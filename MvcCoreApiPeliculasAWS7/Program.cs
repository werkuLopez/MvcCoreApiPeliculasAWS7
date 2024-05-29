using Amazon.S3;
using MvcCoreApiPeliculasAWS7.Helpers;
using MvcCoreApiPeliculasAWS7.Models;
using MvcCoreApiPeliculasAWS7.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

string jsonSecrets =
    await HelperSecretManager.GetSecretAsync();

keysModel keys =
    JsonConvert.DeserializeObject<keysModel>(jsonSecrets);

builder.Services.AddSingleton<keysModel>(x => keys);

// Add services to the container.
builder.Services.AddTransient<ServiceApiPeliculas>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceStorageAWS>();
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
