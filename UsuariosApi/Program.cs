using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Obtendo a string de conexão
// Getting the connection string
var connString = builder.Configuration["ConnectionsString:UsuarioConnection"];

// Configurando o DbContext do usuário
// Configuring the user's DbContext
builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
    });

// Configurando a identidade do usuário
// Configuring the user's identity
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

// Adicionando AutoMapper ao projeto
// Adding AutoMapper to the project
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrando a política de autorização de idade
// Registering the age authorization policy
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando a autenticação
// Configuring the authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Configurando a autorização
// Estamos utilizando a funcionalidade de Autorização do .NET Core para restringir o acesso a certas partes da aplicação baseadas em políticas de autorização.
// Configuring the authorization
// We are using the .NET Core's Authorization feature to restrict access to certain parts of the application based on authorization policies.
builder.Services.AddAuthorization(options =>
{
    // Adicionando uma política chamada "IdadeMinima". Essa política é definida pela classe IdadeMinima que construímos.
    // Esta política vai verificar se um usuário atende a uma certa idade mínima (neste caso, 18 anos) antes de permitir o acesso.
    // Adding a policy named "IdadeMinima". This policy is defined by the IdadeMinima class we've built.
    // This policy will check if a user meets a certain minimum age (in this case, 18 years old) before allowing access.
    options.AddPolicy("IdadeMinima", policy =>
         policy.AddRequirements(new IdadeMinima(18))
    );
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();