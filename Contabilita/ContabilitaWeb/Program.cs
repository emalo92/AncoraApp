using Infrastructure.Dal;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif



//var connectionString = builder.Configuration.GetConnectionString("Contabilita");
var connectionString = builder.Configuration.GetSection("ConnectionString:Contabilita").Value;

var dalStrartup = new Infrastructure.Startup(builder.Configuration);
dalStrartup.ConfigureServices(builder.Services, connectionString);
builder.Services.AddDbContext<ContabilitaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IContabilitaDal>(s =>
                new ContabilitaDal(s.GetRequiredService<ContabilitaDbContext>(), s.GetRequiredService<ILogger<ContabilitaDal>>()));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging(option =>
{
    option.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
    option.AddNLog("NLog.config");
});

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
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFoundPage";
        await next();
    }
});

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
