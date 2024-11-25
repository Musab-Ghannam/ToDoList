

using Microsoft.Extensions.Configuration;
using ToDoList.Option;
using ToDoList.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<FileOption>
        (builder.Configuration.GetSection("FileOption"));
builder.Services.AddHostedService<CreateFileWorker>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
