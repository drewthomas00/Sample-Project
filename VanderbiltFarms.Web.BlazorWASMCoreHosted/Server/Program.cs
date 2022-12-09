using Microsoft.AspNetCore.ResponseCompression;
using VanderbiltFarms.DataAccess;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Service;
using VanderbiltFarms.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}"
        };
    });

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>(x =>
    new DatabaseConnection(builder.Configuration.GetConnectionString("VanderbiltFarms")));

builder.Services.AddTransient<IHomeownerService, HomeownerService>();
builder.Services.AddTransient<IHomeownerRepository, HomeownerRepository>();

builder.Services.AddTransient<IPlotService, PlotService>();
builder.Services.AddTransient<IPlotRepository, PlotRepository>();

builder.Services.AddTransient<IFeeService, FeeService>();
builder.Services.AddTransient<IFeeRepository, FeeRepository>();

builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddHttpClient<IHealthCheckService, HealthCheckService>(client =>
{
    client.BaseAddress = new Uri("https://5rcjffzih8.execute-api.us-east-1.amazonaws.com/");
});

builder.Services.AddTransient<IHealthCheckService, HealthCheckService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
