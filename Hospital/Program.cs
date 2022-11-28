using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Serilog.Events;
using System.Reflection;
using Infrastructure;
using Hospital;
using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;


	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	var migrationAssemblyName = Assembly.GetExecutingAssembly().FullName;
	builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString, m => m.MigrationsAssembly(migrationAssemblyName)));
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	//AUTOFAC CONFIGURATION
	builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
	builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
	{
		containerBuilder.RegisterModule(new WebModule());
		containerBuilder.RegisterModule(new InfrastructureModule(connectionString, migrationAssemblyName));
	});

	//Serilog
	builder.Host.UseSerilog(( ctx, lc ) => lc
							 .MinimumLevel.Debug()
							 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
							 .Enrich.FromLogContext()
							 .ReadFrom.Configuration(builder.Configuration));
try
{

	builder.Services
	.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddUserManager<ApplicationUserManager>()
	.AddRoleManager<ApplicationRoleManager>()
	.AddSignInManager<ApplicationSignInManager>()
	.AddDefaultTokenProviders();


	builder.Services.AddAuthentication()
	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
	{
		options.LoginPath = new PathString("/Account/Login");
		options.AccessDeniedPath = new PathString("/Account/Login");
		options.LogoutPath = new PathString("/Account/Logout");
		options.Cookie.Name = "FirstDemoPortal.Identity";
		options.SlidingExpiration = true;
		options.ExpireTimeSpan = TimeSpan.FromHours(1);
	});

	builder.Services.Configure<IdentityOptions>(options =>
	{
		// Password settings.
		options.Password.RequireDigit = true;
		options.Password.RequireLowercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequiredLength = 6;
		options.Password.RequiredUniqueChars = 0;

		// Lockout settings.
		options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
		options.Lockout.MaxFailedAccessAttempts = 5;
		options.Lockout.AllowedForNewUsers = true;

		// User settings.
		options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
		options.User.RequireUniqueEmail = true;
	});

	builder.Services.AddAuthorization(options =>
	{
		options.AddPolicy("CourseManagementPolicy", policy =>
		{
			policy.RequireAuthenticatedUser();
			policy.RequireRole("Admin");
			policy.RequireRole("Teacher");
		});

		options.AddPolicy("CourseViewPolicy", policy =>
		{
			policy.RequireAuthenticatedUser();
			policy.RequireClaim("ViewCourse", "true");
		});
		options.AddPolicy("CourseCreatePolicy", policy =>
		{
			policy.RequireAuthenticatedUser();
			policy.RequireClaim("CreateCourse", "true");
		});

		//options.AddPolicy("CourseViewRequirementPolicy", policy =>
		//{
		//	policy.RequireAuthenticatedUser();
		//	policy.Requirements.Add(new CourseViewRequirement());
		//});

	});

	builder.Services.AddControllersWithViews();


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
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthentication();
	app.UseAuthorization();

	//app.UseEndpoints(endpoints =>
	//{
	app.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
	////});

	app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");



	app.Run();

}
catch (Exception ex)
{
	Log.Fatal(ex, "Application Start-up Failed");
	Console.WriteLine(ex);
}
finally
{
	Log.CloseAndFlush();
}
