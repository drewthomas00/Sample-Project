using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging.Abstractions;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Service;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.DataAccess;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
    });

builder.Services.AddScoped<IUiBus>(provider => new MvcBus(NullLogger<MvcBus>.Instance));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AppController>();

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
