namespace MyAudio
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using MyAudio.ViewModels;
    using Xamarin.Essentials;

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
            services.AddSingleton<IMyAudioDataAccess, FirestoreDbAccess>();
            services.AddSingleton<IFileService, FirebaseStorageService>();
            services.AddSingleton<IAudioPlayerService, AudioPlayerService>();
            services.AddSingleton<IFilePicker, FilePickerWrapper>();
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
            services.AddTransient<SignUpPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<MainShellViewModel>();
            services.AddTransient<AudioFilesPageViewModel>();
            services.AddTransient<PlaylistsPageViewModel>();
            services.AddTransient<AddPlaylistPageViewModel>();
            services.AddTransient<PlaylistDetailsPageViewModel>();
            services.AddTransient<AudioFilesListViewModel>();
            services.AddSingleton<CurrentPlayingAudioFileViewModel>(); // ICurrentPlayingAudioFileViewModel, 
            return services;
        }
    }
}
