using MonitoringApp.API.IServices;
using MonitoringApp.UI.InterfaceClasses;
using MonitoringApp.UI.Interfaces;
using MonitoringApp.UI.ServiceUI;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ILoginService, LoginServiceUI>();
builder.Services.AddScoped<IApplicationService, ApplicationServiceUI>();
builder.Services.AddScoped<IApplicationLogService, ApplicationLogServiceUI>();
builder.Services.AddScoped<IAppControl, AppControl>();
builder.Services.AddScoped<INotify, Notify>();


builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("AppControl");
    q.AddJob<AppControl>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AppControl-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule("0 * * ? * *")
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
