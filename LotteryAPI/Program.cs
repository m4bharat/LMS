using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;
using LotteryAPI.LotteryBusiness.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding DBconnetion
string conStr = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<ContestDbContext>(options => options.UseSqlServer(conStr));

// Register Repository
builder.Services.AddScoped<IContestDetailRepo, ContestDetailRepo>();
builder.Services.AddScoped<ILotteryNumbersRepo, LotteryNumbersRepo>();
builder.Services.AddScoped<IContestResultRepo, ContestResultRepo>();

// Register Services
builder.Services.AddScoped<IContestDetailService, ContestDetailService>();
builder.Services.AddScoped<ILotteryNumbersService, LotteryNumbersService>();
builder.Services.AddScoped<IContestResultService, ContestResultService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
