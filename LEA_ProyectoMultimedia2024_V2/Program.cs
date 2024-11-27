
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Services.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProducto, ProductoRepository>();
builder.Services.AddScoped<ICategorias, CategoriasRepository>();
builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<IDescuento, DescuentosRepository>();
builder.Services.AddScoped<IDetalleOrden, DetalleOrdenRepository>();
builder.Services.AddScoped<IDireccionEnvios, DireccionEnviosRepository>();
builder.Services.AddScoped<IMetodoDePago, MetodoDePagoRepository>();
builder.Services.AddScoped<IOrden, OrdenRepository>();
builder.Services.AddScoped<IReseñaProducto,ReseñaProductoRepository>();
builder.Services.AddScoped<IOrdenRepository, FacturaRepository>();


builder.Services.AddDbContext<GimnasioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<GimnasioContext>(
    options =>
    {
        //options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
        options.UseSqlServer(builder.Configuration.GetConnectionString("BD"));

    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Home/Recividor"; // Ruta de la página de inicio de sesión
    options.LogoutPath = "/Home/Logout"; // Ruta de la página de cierre de sesión
    options.AccessDeniedPath = "/Home/Rechazo";
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Pomprador", policy => policy.RequireRole("Cliente", "Vendedor", "Mantenedor", "Admin"));
    options.AddPolicy("Vendedor", policy => policy.RequireRole("Vendedor", "Admin"));
    options.AddPolicy("Mantenedor", policy => policy.RequireRole("Mantenedor", "Admin"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));

});

// Add services to the container.
builder.Services.AddControllersWithViews();



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
