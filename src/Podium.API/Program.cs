using Microsoft.EntityFrameworkCore;
using Podium.Application.MappingProfiles;
using Podium.Application.Services;
using Podium.Infrastructure.Respositories;
using Podium.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PodiumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<UsersServices>();
builder.Services.AddScoped<SessionServices>();

builder.Services.AddScoped<AuthServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
