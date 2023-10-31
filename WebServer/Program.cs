using DataLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddSingleton<IMediaService, MediaService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IActorService, ActorService>();
builder.Services.AddSingleton<ILanguageService, LanguageService>();

var service = new UserService();
var userToCreate = new DataLayer.Objects.User { Username = "Test" };

service.CreateUser(userToCreate.Username);

var app = builder.Build();

app.MapControllers();

app.Run();

