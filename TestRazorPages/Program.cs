using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestRazorPages.Data;
using TestRazorPages.Models;
using TestRazorPages.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TestRazorPagesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestRazorPagesContext") ?? throw new InvalidOperationException("Connection string 'TestRazorPagesContext' not found.")));

var app = builder.Build();

using (var score = app.Services.CreateScope())
{
    var services = score.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
