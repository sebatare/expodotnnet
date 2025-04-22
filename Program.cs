using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de SignalR
builder.Services.AddSignalR().AddHubOptions<ChatHub>(options => { options.EnableDetailedErrors = true; });

//CONFIGURACION DE RUTAS Y CONTROLADORES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//CONFIGURACION DE SWAGGER
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//CONGIRACION DE BASE DE DATOS
builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

//CONFIGURTACION DE IDENTITY
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;  // Asegura que los emails sean únicos
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



//CONFIGURACION DE AUTENTICACION JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero // Evitar que la expiración se considere un poco más tarde
        };
    });

// Agregar servicios personalizados (por ejemplo, IUserService)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICanchaService, CanchaService>();
builder.Services.AddScoped<ICanchaRepository, CanchaRepository>();
builder.Services.AddScoped<ISedeService, SedeService>();
builder.Services.AddScoped<ISedeRepository, SedeRepository>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IUserTeamRepository, UserTeamRepository>();

// Configuración para el servicio de correo
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Agregar autorización
builder.Services.AddAuthorization();

var app = builder.Build();

// Configuración Swagger para autenticación con JWT
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseCors("AllowAll");

app.UseAuthentication();  // Asegúrate de que esté antes de UseAuthorization
app.UseAuthorization();

// Mapear controladores y el Hub de SignalR
app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
