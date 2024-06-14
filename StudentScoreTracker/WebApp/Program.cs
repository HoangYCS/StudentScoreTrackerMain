using DataApp.ContextDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RespositoryApp.IRespositories;
using RespositoryApp.Respositories;
using ServiceApp.IServices;
using ServiceApp.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EducationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EducationConection"), option => option.CommandTimeout(60 * 5)));
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var dbContext = sp.GetRequiredService<EducationDbContext>();
    return new SqlConnection(dbContext.Database.GetDbConnection().ConnectionString);
});

builder.Services.AddScoped<IEducationDataRespository, EducationDataRespository>();
builder.Services.AddScoped<IEducationDataService, EducationDataService>();

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
