using Agenda.MVC.Areas.Admin.Data;
using Agenda.MVC.Constants;
using Agenda.MVC.Data;
using Agenda.MVC.Mapper;
using Agenda.MVC.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

AuthService(builder.Services);
ResolveDependencies(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");

});

app.Run();

void ResolveDependencies(IServiceCollection services)
{
    services.AddAutoMapper(typeof(ViewModelToPost));

    services.AddScoped<HeaderTokenHandler>();
    var nullTask = Task.FromResult<Exception>(null);

    services.AddHttpContextAccessor();

    services.AddRefitClient<IApiLoginService>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(Store.ApiUrl));

    services.AddRefitClient<IApiAgendaService>(new RefitSettings { ExceptionFactory = httpResponse => nullTask })
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(Store.ApiUrl))
        .AddHttpMessageHandler<HeaderTokenHandler>();

    services.AddRefitClient<IApiAgendaAdminService>(new RefitSettings { ExceptionFactory = httpResponse => nullTask })
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(Store.ApiUrl))
        .AddHttpMessageHandler<HeaderTokenHandler>();

    services.AddRefitClient<IApiUserService>(new RefitSettings {ExceptionFactory = httpResponse => nullTask})
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(Store.ApiUrl))
        .AddHttpMessageHandler<HeaderTokenHandler>();

    services.AddRefitClient<IApiLogService>(new RefitSettings { ExceptionFactory = httpResponse => nullTask })
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(Store.ApiUrl))
        .AddHttpMessageHandler<HeaderTokenHandler>();
}

void AuthService(IServiceCollection services)
{
    services.AddAuthorization()
            .AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/Home";
                options.SlidingExpiration = true;
            });
}

