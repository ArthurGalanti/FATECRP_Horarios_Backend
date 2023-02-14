using SeleniumBot.Data;
using SeleniumBot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDataContext>();
builder.Services.AddScoped<ScrapperService>();
builder.Services.AddSingleton<CustomizeService>();


builder.Services.AddCors(policeBuilder =>
    policeBuilder.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));

builder.WebHost.UseUrls("http://localhost:4000", "http://odin:4000", "http://192.168.15.108:4000");

var app = builder.Build();
app.UseCors();
app.MapControllers();
app.Run();



