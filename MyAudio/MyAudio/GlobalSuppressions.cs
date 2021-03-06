// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not required", Scope = "namespace", Target = "~N:MyAudio.Interfaces")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not required", Scope = "namespace", Target = "~N:MyAudio.Models")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not required", Scope = "namespace", Target = "~N:MyAudio")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not required", Scope = "namespace", Target = "~N:MyAudio.Utilities")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Not required", Scope = "namespace", Target = "~N:MyAudio.Services")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Valid as it is suppression file")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Is for a singleton instance.", Scope = "member", Target = "~F:MyAudio.Services.MyAudioDatabase.Instance")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Removed to keep code tidy.")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Not my style for parameters")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Converters.MillisecondsToTimeFormatStr.ConvertBack(System.Object,System.Type,System.Object,System.Globalization.CultureInfo)~System.Object")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Converters.MillisecondsToTimeFormatStr.Convert(System.Object,System.Type,System.Object,System.Globalization.CultureInfo)~System.Object")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.MainShell.#ctor")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Converters.AudioFileCountToInfoString.ConvertBack(System.Object,System.Type,System.Object,System.Globalization.CultureInfo)~System.Object")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Converters.AudioFileCountToInfoString.Convert(System.Object,System.Type,System.Object,System.Globalization.CultureInfo)~System.Object")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Not needed.", Scope = "type", Target = "~T:MyAudio.MainShell")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Views.AddPlaylistPage.#ctor")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Not needed.", Scope = "type", Target = "~T:MyAudio.Views.AddPlaylistPage")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.ViewModels.AddPlaylistPageViewModel.PlaylistAudioFile.#ctor(MyAudio.Models.AudioFile,System.Boolean)")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Services.MyAudioDatabase.SavePlaylistAsync(MyAudio.Interfaces.IPlaylist)~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "type", Target = "~T:MyAudio.Interfaces.IAudioFilePlaylist")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~P:MyAudio.Models.AudioFilePlaylist.AudioFileID")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~P:MyAudio.Models.AudioFilePlaylist.PlaylistID")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.Services.MyAudioDatabase.SaveAudioFilePlaylistAsync(MyAudio.Interfaces.IAudioFilePlaylist)~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Property name is self-explanatory", Scope = "member", Target = "~P:MyAudio.ViewModels.PlaylistDetailsPageViewModel.PlaylistTitle")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Property name is self-explanatory", Scope = "member", Target = "~P:MyAudio.ViewModels.PlaylistDetailsPageViewModel.PlaylistAudioFiles")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Not needed.", Scope = "member", Target = "~M:MyAudio.ViewModels.PlaylistDetailsPageViewModel.Initialise~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "No documentation needed.", Scope = "member", Target = "~P:MyAudio.Models.AudioFilePlaylist.ID")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1515:Single-line comment should be preceded by blank line", Justification = "This is an exception.", Scope = "member", Target = "~M:MyAudio.ViewModels.AudioFilesPageViewModel.UploadAudioFile~System.Threading.Tasks.Task{Xamarin.Essentials.FileResult}")]
