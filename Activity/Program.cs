using Activity.Repository;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<TodoRestRepository>(client =>
{
  client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});
IServiceCollection serviceCollection = builder.Services.AddTransient<ITodoRestRepository, TodoRestRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
