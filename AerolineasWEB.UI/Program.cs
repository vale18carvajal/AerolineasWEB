using AerolineasWEB.UI;

var builder = WebApplication.CreateBuilder(args);
//URL del API
var AerolineasWebAPIBaseUrl = builder.Configuration.GetValue<string>("AerolineasWebAPI:BaseUrl");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("AerolineasWebAPI", client =>
{
    //client.BaseAddress = new Uri("https://localhost:7235/");
    client.BaseAddress = new Uri(AerolineasWebAPIBaseUrl);
});
builder.Services.AddScoped<ServicioAPI>();

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
