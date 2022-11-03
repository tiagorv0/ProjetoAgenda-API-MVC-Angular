using Agenda.Api.Configuration;
using Agenda.Api.Filters;
using Agenda.Application.Mappers;
using Agenda.Infrastructure.Context;
using Agenda.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ApplicationExceptionFilter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.ResolveDependencies();
builder.Services.AddIdentityAndJwtConfiguration(builder.Configuration);
builder.Services.AddSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", x =>
    {
        x.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JsonStorageOptions>(config =>
{
    config.FilePath = "\\log.json";
});

builder.Services.AddAutoMapper(typeof(AgendaProfile));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
