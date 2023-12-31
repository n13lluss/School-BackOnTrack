using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Services;
using BackOnTrack.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISleepService, SleepService>();
builder.Services.AddScoped<ISleepRepository, SleepRepository>();

builder.Services.AddScoped<IToDOService, ToDoService>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

builder.Services.AddScoped<IStressService, StressService>();
builder.Services.AddScoped<IStressRepository, StressRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
