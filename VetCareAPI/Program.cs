using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCareAPI.Data;
using VetCareAPI.Repositories;
using VetCareAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseMySql(
        builder.Configuration.GetConnectionString("db"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("db"))
    ));

// builder.Services.Configure<ApiBehaviorOptions>(options =>
// {
//     options.InvalidModelStateResponseFactory = ctx =>
//         new UnprocessableEntityObjectResult(ctx.ModelState);
// });

builder.Services.AddScoped<ClinicRepository>();
builder.Services.AddScoped<AppUserRepository>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<VisitRepository>();
builder.Services.AddScoped<VisitService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<ClinicService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
