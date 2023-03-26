using Eventmi.Core;
using Eventmi.Core.Contracts;
using Eventmi.Core.Services;
using Eventmi.Data.Common.Contracts;
using Eventmi.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var test = builder.Configuration["ConnectionStrings:Eventmi"];

builder.Services.AddDbContext<EventmiDbContext>(options =>
        options.UseSqlServer(builder.Configuration["ConnectionStrings:Eventmi"])
    );

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IEventService, EventService>();


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
