namespace Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public static class MockConfigurationExtensions
{
    public static IServiceCollection AddConfigurationValues(
        this IServiceCollection services,
        Dictionary<string, string?> configValues
    )
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(configValues).Build();

        return services.AddSingleton<IConfiguration>(configuration);
    }
}
