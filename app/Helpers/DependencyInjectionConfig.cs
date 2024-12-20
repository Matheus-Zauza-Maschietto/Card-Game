using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Middlewares;
using app.Models;
using app.Repositories;
using app.Repositories.Interfaces;
using app.Services;

namespace app.Helper;

    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjections(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<JsonWebTokensService>();
            builder.Services.AddScoped<UserService>();  
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<LanguageService>();
            builder.Services.AddScoped<DeckCardService>();
            builder.Services.AddScoped<DeckService>();
            builder.Services.AddScoped<CardService>();
            builder.Services.AddScoped<KafkaService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<IDeckRepository, DeckRepository>();
            builder.Services.AddScoped<ICardRepository, CardRepository>();
            builder.Services.AddScoped<ICardApiRepository, CardApiRepository>();
            builder.Services.AddScoped<IDeckCardRepository, DeckCardRepository>();
            builder.Services.AddSingleton<IRedisRepository, RedisRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<LogsMiddleware>();
        }   
    }
