using DataLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddSingleton<IMediaService, MediaService>();
builder.Services.AddSingleton<IActorService, ActorService>();


var app = builder.Build();

app.MapControllers();

app.Run();
