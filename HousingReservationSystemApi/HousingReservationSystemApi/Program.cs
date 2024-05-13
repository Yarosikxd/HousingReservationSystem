using HousingReservationSystemApi.Midllewares;
using HousingReservationSystemApplication.Interfaces;
using HousingReservationSystemApplication.Services;
using HousingReservationSystemDataAccess;
using HousingReservationSystemDataAccess.Repositoryes;
using HousingReservationSystemDomain.Abstraction.Auth;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemInfrastucture.JWT;
using HousingReservationSystemInfrastucture.Password;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings
    ().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataBaseDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<IAccommodationRepository, AccommodationRepository>();
builder.Services.AddScoped<IAccommodationService, AccommodationService>();
builder.Services.AddScoped<AccommodationService>();
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
