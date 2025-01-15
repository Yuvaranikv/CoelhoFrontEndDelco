using Microsoft.AspNetCore.Authentication.Cookies;
using TURN.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Register ClientService with connection string
builder.Services.AddScoped<ClientService>(sp =>
    new ClientService(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});




// Configure JSON serialization options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve exact property names
});

// Add Authentication and Authorization services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to Login page if unauthenticated
        options.LogoutPath = "/Account/Logout"; // Optional logout path
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();

app.Run();
