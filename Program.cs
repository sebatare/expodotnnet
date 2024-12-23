using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR()
                .AddHubOptions<ChatHub>(options =>
                {
                    options.EnableDetailedErrors = true;
                });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseLazyLoadingProxies() // Habilita Lazy Loading
           .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Configura SQL Server
});



builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Configura la opción para asegurar que los emails sean únicos

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuración de JWT
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        Console.WriteLine("Configuración de JWT completada");
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                Console.WriteLine("Accediendo a OnMessageReceived...");
                var accessToken = context.Request.Query["access_token"];
                Console.WriteLine($"Access token recibido: {accessToken}");
                if (!string.IsNullOrEmpty(accessToken) && context.HttpContext.Request.Path.StartsWithSegments("/chathub"))
                {
                    Console.WriteLine("Token se asigna correctamente");
                    context.Token = accessToken;
                }
                else
                {
                    Console.WriteLine("No se ha recibido token o la ruta es incorrecta");
                }

                return Task.CompletedTask;
            }
        };
    });





builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ChatRepository>();



builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();




builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:8081")  // Asegúrate de que esta URL sea la correcta para tu cliente
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();  // Permitir credenciales (tokens JWT, cookies, etc.)
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
