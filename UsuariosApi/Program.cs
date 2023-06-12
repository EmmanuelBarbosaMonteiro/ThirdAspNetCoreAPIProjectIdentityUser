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

// Obtendo a string de conex�o
// Getting the connection string
var connString = builder.Configuration["ConnectionsString:UsuarioConnection"];

// Configurando o DbContext do usu�rio
// Configuring the user's DbContext
builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
    });

// Configurando a identidade do usu�rio
// Configuring the user's identity
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

// Adicionando AutoMapper ao projeto
// Adding AutoMapper to the project
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrando a pol�tica de autoriza��o de idade
// Registering the age authorization policy
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando a autentica��o
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

// Configurando a autoriza��o
// Estamos utilizando a funcionalidade de Autoriza��o do .NET Core para restringir o acesso a certas partes da aplica��o baseadas em pol�ticas de autoriza��o.
// Configuring the authorization
// We are using the .NET Core's Authorization feature to restrict access to certain parts of the application based on authorization policies.
builder.Services.AddAuthorization(options =>
{
    // Adicionando uma pol�tica chamada "IdadeMinima". Essa pol�tica � definida pela classe IdadeMinima que constru�mos.
    // Esta pol�tica vai verificar se um usu�rio atende a uma certa idade m�nima (neste caso, 18 anos) antes de permitir o acesso.
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