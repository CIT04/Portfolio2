using DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using WebServiceToken.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSingleton<Hashing>();

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

var secret = builder.Configuration.GetSection("Auth:Secret").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = false,
            ValidateAudience =false,
            ClockSkew = TimeSpan.Zero,


    };
    }
    
    );

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

