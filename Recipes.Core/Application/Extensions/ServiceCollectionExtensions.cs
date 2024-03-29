using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Core.Application.Auth;
using Recipes.Core.Application.Contracts;
using Recipes.Core.Application.Models;
using Recipes.Core.Infrastructure.Ingredients;

namespace Recipes.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<UserSettings>(configuration.GetRequiredSection("User"));
        serviceCollection.Configure<JwtSettings>(configuration.GetRequiredSection("Jwt"));
        
        serviceCollection.AddSingleton<IAccessTokenGenerator, JwtGenerator>();
        
        serviceCollection.AddSingleton<IIngredientsService, CachedIngredientsService>();

        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => x.FullName!.StartsWith(nameof(Recipes)))
            .ToArray();
        
        serviceCollection.AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies));
        serviceCollection.AddAutoMapper(assemblies);
        serviceCollection.AddValidatorsFromAssemblies(assemblies);
        
        return serviceCollection;
    }
}