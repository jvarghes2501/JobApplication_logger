using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add service into IoC container
builder.Services.AddScoped<ICompanyService, CompanyService>(); //singleton lifetime

builder.Services.AddDbContext<companyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();



if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers(); 
app.Run();
