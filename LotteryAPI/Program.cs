using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;
using LotteryAPI.LotteryBusiness.Service;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Service.Authorization;
using UserIdentity.Service.Helpers;
using UserIdentity.Service.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(AutoMapperProfile).Assembly);
// configure strongly typed settings objects
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Adding DBconnetion
string conStr = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<ContestDbContext>(options => options.UseSqlServer(conStr));

// Register Repository
builder.Services.AddScoped<IContestDetailRepo, ContestDetailRepo>();
builder.Services.AddScoped<ILotteryNumbersRepo, LotteryNumbersRepo>();
builder.Services.AddScoped<IContestResultRepo, ContestResultRepo>();

// Register Services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
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
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
