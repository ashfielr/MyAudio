namespace MyAudio.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Services;

    /// <summary>
    /// Abstract class to provide base functionality for view models.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Event to notify of property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialises the view model.
        /// </summary>
        /// <returns>Returns task of initialisation.</returns>
        public abstract Task Initialise();

        /// <summary>
        /// Handles property change events.
        /// </summary>
        /// <param name="propertyName">The name of the property which has changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
