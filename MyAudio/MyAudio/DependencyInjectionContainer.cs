namespace MyAudio
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using MyAudio.ViewModels;

    /// <summary>
    /// Class for dependency injection container.
    /// </summary>
    public static class DependencyInjectionContainer
    {
        /// <summary>
        /// Configures all app services.
        /// </summary>
        /// <param name="services">The services container.</param>
        /// <returns>The services container with app services configured.</returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IMyAudioDataAccess, MyAudioDatabase>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IAudioPlayerService, AudioPlayerService>();
            return services;
        }

        /// <summary>
        /// Configures all of the mocks.
        /// </summary>
        /// <param name="services">The services container.</param>
        /// <returns>The services container with mocks configured.</returns>
        public static IServiceCollection ConfigureMockServices(this IServiceCollection services)
        {
            // add your mocks.
            return services;
        }

        /// <summary>
        /// Configures all of the view models.
        /// </summary>
        /// <param name="services">The services container.</param>
        /// <returns>The services container with VMs configured.</returns>
        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<AudioFilesPageViewModel>();
            services.AddTransient<PlaylistsPageViewModel>();
            return services;
        }
    }
}
