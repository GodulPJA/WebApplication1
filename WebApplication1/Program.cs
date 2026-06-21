using WebApplication1.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<_2019sbdContext>();
builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();



app.UseAuthorization();
app.MapControllers();

app.Run();