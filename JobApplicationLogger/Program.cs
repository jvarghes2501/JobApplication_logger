using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add service into IoC container
builder.Services.AddSingleton<ICompanyService, CompanyService>(); //singleton lifetime
builder.Services.AddSingleton<ILocationService, LocationService>(); //singleton lifetime

var app = builder.Build();

if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers(); 
app.Run();
