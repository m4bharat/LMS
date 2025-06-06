using LotteryAPI.DbInfra.Model;
using System.Text.Json.Serialization;

namespace UserIdentity.Service.Entities
{
    public class User : TrackableModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsConfirmedPhoneNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsConfirmedEmailId { get; set; }
        public string? PanNo { get; set; }
        public string? AdharNo { get; set; }
        public int? Pincode { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public bool IsAccesptedTermAndCondition { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
