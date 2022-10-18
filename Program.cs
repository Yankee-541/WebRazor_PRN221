using WebRazor.Models;
using WebRazor.Hubs;
var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSession(otp => otp.IdleTimeout = TimeSpan.FromMinutes(5));
builder.Services.AddDbContext<PRN221DBContext>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapRazorPages();
app.MapHub<HubServer>("/hub");
app.Run();