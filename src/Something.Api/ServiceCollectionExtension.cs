using Something.Core;
using Something.Infra;

namespace Something.Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IAccountsRepository, InMemoryAccountsRepo>(s => { 
            var repo = new InMemoryAccountsRepo();
            repo.Add(new Core.Account("user", "pass"));
            repo.Add(new Core.Account("bob", "ong"));
            return repo;
        });
        return services;
    }
}