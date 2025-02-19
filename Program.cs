using BaseApi.Configuaration;
using BaseApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using BaseApi.Repository;
using BaseApi.Service;
using BaseApi.Service;

using Microsoft.Extensions.FileProviders;
using BaseApi.Configuaration;
using BaseApi.Extensions;
using BaseApi.Repository;
using BaseApi.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nh?p token vào ?ây:",
        Name = "Authorization",
        In = ParameterLocation.Header,


        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
});
var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);
GlobalSettings.IncludeConfig(appSettings.Get<AppSettings>());


builder.Services.ConfigureDbContext(GlobalSettings.AppSettings.Database.DatabaseConfig);
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Repository
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOtpService, OtpService>();


// Add services to the container.
builder.Services.AddAppScopedService();




builder.Services.AddCors(x => x.AddPolicy("CorsPolicy", p =>
{
    p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.UseSwagger();
app.UseSwaggerUI();
/*}*/

var resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/");
if (!Directory.Exists(resourcesPath))
{
    Directory.CreateDirectory(resourcesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(resourcesPath),
    RequestPath = "/Resources"

});
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(resourcesPath),
//    RequestPath = "/Resources/Template"

//});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();



app.Run();
