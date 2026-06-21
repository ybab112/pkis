using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using MyWebApp.Data;
using MyWebApp.Components;
using MyWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
});

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<ShoeService>();
builder.Services.AddSingleton<MyWebApp.Services.CartService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication(); 
app.UseAuthorization();  
app.UseAntiforgery();    

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();


app.MapPost("/Account/Logout", async (HttpContext context, SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    
    string? returnUrl = context.Request.Form["ReturnUrl"].ToString();
    return Results.LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try { await DbInitializer.InitializeAsync(services); }
    catch (Exception ex) { Console.WriteLine(ex.Message); }

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>(); 
    
    string[] roles = { "Administrator", "Manager", "Customer" };
    foreach (var role in roles) {
        if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
    }

    var adminEmail = "admin@shop.ru";
    if (await userManager.FindByEmailAsync(adminEmail) == null) {
        var newAdmin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var result = await userManager.CreateAsync(newAdmin, "Admin123!");
        if (result.Succeeded) await userManager.AddToRoleAsync(newAdmin, "Administrator");
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    MyWebApp.Data.SeedData.Initialize(services);
}

app.Run();