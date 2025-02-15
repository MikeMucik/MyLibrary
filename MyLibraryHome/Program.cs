using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Infrastructure;
using MyLibraryMVC.Application;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyLibraryMVC.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<MyLibraryMVC.Infrastructure.Context>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IEmailSender, FakeEmailSender>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Context>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options
	=> options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<MyLibraryMVC.Infrastructure.Context>()
	.AddDefaultTokenProviders();


//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
	;

//builder.Services.AddControllers()
//	.AddJsonOptions(options =>
//	{
//		options.JsonSerializerOptions.PropertyNamingPolicy = null; // Zapobiega konwersji do camelCase
//	});

builder.Services.AddAplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();
//app.MapControllers();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
