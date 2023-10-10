using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SignalR;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<DataContext>();
// For Identity
builder.Services.AddIdentity<Usuarios, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

// Adding Authentication
string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
if (string.IsNullOrEmpty(JWT_SECRET))
    JWT_SECRET = configuration["JWT:Secret"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidAudience = configuration["JWT:ValidAudience"],
        //ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    // Reset token valid for 2 hours
    options.TokenLifespan = TimeSpan.FromHours(2);
});
// Add cors builder
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:5173").AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Web API - V1",
            Version = "v1"
        }
     );

    //var filePath = Path.Combine(AppContext.BaseDirectory, "WebAPI.xml");
    //c.IncludeXmlComments(filePath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    }});
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

/********************************************************************************************************/
/** Add Dependencies                                                                                   **/
/********************************************************************************************************/
builder.Services.AddTransient<IDAL_Categoria, DAL_Categoria>();
builder.Services.AddTransient<IB_Categoria, B_Categoria>();

builder.Services.AddTransient<IDAL_Usuario, DAL_Usuario>();
builder.Services.AddTransient<IB_Usuario, B_Usuario>();

builder.Services.AddTransient<IDAL_Casteo, DAL_Casteo>();
builder.Services.AddTransient<IDAL_FuncionesExtras, DAL_FuncionesExtras>();

builder.Services.AddTransient<IDAL_Ingrediente, DAL_Ingrediente>();
builder.Services.AddTransient<IB_Ingrediente, B_Ingrediente>();

builder.Services.AddTransient<IDAL_Mesa, DAL_Mesa>();
builder.Services.AddTransient<IB_Mesa, B_Mesa>();

builder.Services.AddTransient<IDAL_Pedido, DAL_Pedido>();
builder.Services.AddTransient<IB_Pedido, B_Pedido>();

builder.Services.AddTransient<IDAL_Producto, DAL_Producto>();
builder.Services.AddTransient<IB_Producto, B_Producto>();

builder.Services.AddTransient<IDAL_ClientePreferencial, DAL_ClientePreferencial>();
builder.Services.AddTransient<IB_ClientePreferencial, B_ClientePreferencial>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Add SignalR to the request pipeline
app.MapHub<ChatHub>("/chatHub");

app.MapControllers();
app.Run();
