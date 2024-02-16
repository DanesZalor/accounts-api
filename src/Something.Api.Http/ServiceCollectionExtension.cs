using Something.Core;
using Something.Infra;

namespace Something.Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IAccountsRepository, InMemoryAccountsRepo>(s => { 
            var repo = new InMemoryAccountsRepo();
            repo.Add(new Core.Account("among", "us"));
            return repo;
        });
        return services;
    }
}