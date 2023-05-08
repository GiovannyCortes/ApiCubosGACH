using ApiCubosGACH.Data;
using ApiCubosGACH.Helpers;
using ApiCubosGACH.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// IMPORTS PARA EL TOKEN ====================================================================================
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("SqlAzure");

    builder.Services.AddTransient<RepositoryCubos>();
    builder.Services.AddDbContext<CubosContext>(options => 
        options.UseSqlServer(connectionString)
    );

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo {
            Title = "CUBOS API GACH",
            Description = "API examen",
            Version = "v1",

        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "API CUBOS GACH V1");
    options.RoutePrefix = "";
});


if (app.Environment.IsDevelopment()) {}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
