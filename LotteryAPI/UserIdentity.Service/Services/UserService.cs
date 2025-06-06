using AutoMapper;
using LotteryAPI.DbInfra.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using UserIdentity.Service.Authorization;
using UserIdentity.Service.Entities;
using UserIdentity.Service.Helpers;
using UserIdentity.Service.Models.Users;
using BCryptNet = BCrypt.Net.BCrypt;

namespace UserIdentity.Service.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private ContestDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            ContestDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.EmailId == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            for (int i = 0; i < 4500; i++)
            {
                var ph = 1234567890 + i;
                model.EmailId = "testuser" + i + "@lottery.com";
                model.Password = "testuser" + i;
                model.FirstName = "Test";
                model.LastName = "User" + i;
                model.PhoneNumber = ph.ToString();

                // validate
                if (_context.Users.Any(x => x.EmailId == model.EmailId))
                    throw new AppException("EmailId '" + model.EmailId + "' is already registered");
                if (_context.Users.Any(x => x.PhoneNumber == model.PhoneNumber))
                    throw new AppException("Phone Number '" + model.PhoneNumber + "' is already registered");

                // map model to new user object
                var user = _mapper.Map<User>(model);

                // hash password
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

                // save user
                _context.Users.Add(user);
                _context.SaveChanges();

            }
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.EmailId && _context.Users.Any(x => x.EmailId == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}