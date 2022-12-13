using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hospital.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Infrastructure;
using Hospital;
using Infrastructure.DbContexts;
using Infrastructure.Entities;

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	var migrationAssemblyName = Assembly.GetExecutingAssembly().FullName;
	builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString, m => m.MigrationsAssembly(migrationAssemblyName)));
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
	builder.Services
	.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

	//Password Configuration
	builder.Services.Configure<IdentityOptions>(options =>
	{
		options.Password.RequiredUniqueChars = 0;
		options.Password.RequireDigit = false;
		options.Password.RequireUppercase = false;
		options.Password.RequiredLength = 4;
		options.Password.RequireNonAlphanumeric = false;

	});

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
}
finally
{
	Log.CloseAndFlush();
}
