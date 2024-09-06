using N5Application.Start;
using N5Infrastructure.config;
using N5Api.StartUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:5173", "https://example.com")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseUrls("https://*:5253");
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DatabaseInitial.ExecuteMigrate(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
