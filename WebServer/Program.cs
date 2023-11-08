using DataLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddTransient<IMediaService, MediaService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IActorService, ActorService>();
builder.Services.AddSingleton<ILanguageService, LanguageService>();
builder.Services.AddSingleton<IRatingService, RatingService>();
builder.Services.AddSingleton<IBookmarkService, BookmarkService>();
builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IHistoryService, HistoryService>();
builder.Services.AddSingleton<ILocalRatingService, LocalRatingService>();
builder.Services.AddSingleton<ITeamService, TeamService>(); 
builder.Services.AddSingleton<ISearchHistoryService, SearchHistoryService>();


var app = builder.Build();

app.MapControllers();

app.Run();

