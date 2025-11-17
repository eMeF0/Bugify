using Bugify.API.Data;
using Bugify.API.Mappings;
using Bugify.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BugifyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BugifyConnection"));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(x => x.ErrorMessage).ToArray());
        var response = new
        {
            message = "Validation failed",
            errors = errors
        };
        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddScoped<ITaskRepository, SQLTaskRepository>();
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
