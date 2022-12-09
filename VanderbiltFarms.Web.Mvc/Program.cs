using Auth0.AspNetCore.Authentication;
using VanderbiltFarms.DataAccess;
using VanderbiltFarms.DataAccess.Helpers;
using VanderbiltFarms.DataAccess.Interfaces;
using VanderbiltFarms.Service;
using VanderbiltFarms.Service.Interfaces;
using VanderbiltFarms.Web.Mvc.Support;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureSameSiteNoneCookies();

builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>(x =>
    new DatabaseConnection(builder.Configuration.GetConnectionString("VanderbiltFarms")));

builder.Services
    .AddTransient<IHomeownerService, HomeownerService>()
    .AddTransient<IHomeownerRepository, HomeownerRepository>()
    .AddTransient<IPlotService, PlotService>()
    .AddTransient<IPlotRepository, PlotRepository>()
    .AddTransient<IFeeService, FeeService>()
    .AddTransient<IFeeRepository, FeeRepository>()
    .AddTransient<ITransactionService, TransactionService>()
    .AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddHttpClient<IHealthCheckService, HealthCheckService>(client =>
{
    client.BaseAddress = new Uri("https://5rcjffzih8.execute-api.us-east-1.amazonaws.com/");
});

builder.Services.AddTransient<IHealthCheckService, HealthCheckService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Welcome/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Welcome}/{action=Index}/{id?}");

app.Run();