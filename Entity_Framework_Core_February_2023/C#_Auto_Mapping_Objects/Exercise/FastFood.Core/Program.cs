using FastFood.Core.MappingConfiguration;
using FastFood.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<FastFoodContext>(options =>
//                options.UseSqlServer(connectionString));

var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
conStrBuilder.DataSource = builder.Configuration["ConnectionStrings:FastFood:Server"];
conStrBuilder.UserID = builder.Configuration["ConnectionStrings:FastFood:UserId"];
conStrBuilder.Password = builder.Configuration["ConnectionStrings:FastFood:Password"];

builder.Services.AddDbContext<FastFoodContext>(options => options.UseSqlServer(conStrBuilder.ConnectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<FastFoodProfile>();
});

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
