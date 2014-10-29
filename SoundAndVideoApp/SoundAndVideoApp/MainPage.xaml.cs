using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SoundAndVideoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaState _state;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            _state= MediaState.Stopped;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        

        private void PlaySoundButton_OnClick(object sender, RoutedEventArgs e)
        {
            // set source in runtime 
            MyMediaElement.Source = new Uri("ms-appx:///Assets/duck.wav", UriKind.RelativeOrAbsolute);
            MyMediaElement.Play();
        }


        private void PlayVideoButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case MediaState.Stopped:
                    MyMediaElement.Source = new Uri("ms-appx:///Assets/coffee.mp4", UriKind.RelativeOrAbsolute);
                    MyMediaElement.Play();
                    _state = MediaState.Playing;
                    break;
                case MediaState.Playing:
                    MyMediaElement.Pause();
                    _state = MediaState.Paused;
                    break;
                case MediaState.Paused:
                    MyMediaElement.Play();
                    _state = MediaState.Playing;
                    break;
            }
        }

        private void MyMediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            _state = MediaState.Stopped;
        }
    }

    public enum MediaState
    {
        Stopped,
        Playing,
        Paused,
    }
}