using Test.Web.Extensions;
using Test.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilter));
});

//builder.Services.AddHttpClient();

//builder.Services.AddHttpClient("EmployeeHttpClient", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration.GetSection("ExternalServiceSettings").GetValue<String>("EmployeeEndpointUrl").Replace("{key}", builder.Configuration.GetSection("ExternalServiceSettings").GetValue<String>("EmployeeAPIKey")));
//});

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IEmployeeLogic, EmployeeLogic>();

builder.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
