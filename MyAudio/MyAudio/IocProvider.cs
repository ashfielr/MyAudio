namespace MyAudio
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class to encapsulate startup activites.
    /// </summary>
    public static class IocProvider
    {
        /// <summary>
        /// Gets or sets service provider (container).
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Initialises the service provider / container.
        /// </summary>
        /// <returns>The service provider.</returns>
        public static IServiceProvider Init()
        {
            ServiceProvider serviceProvider;

            serviceProvider = new ServiceCollection()
            .ConfigureServices()
            .ConfigureViewModels()
            .BuildServiceProvider();

            ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }
}
