using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SerializingData;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SerializingData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
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

        private const string XML_FILE_NAME = "data.xml";
        private const string JSON_FILE_NAME = "data.json";


        private async void WriteXmlButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cars = BuildCars();
            await WriteXmlAsync(cars);
        }

        private async void WriteJsonButton_OnClick(object sender, RoutedEventArgs e)
        {
            var cars = BuildCars();
            await WriteJsonAsync(cars);
        }

        private async void ReadXmlButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await ReadAsync(XML_FILE_NAME);
            ResultTextBlock.Text = result;
        }

        private async void ReadJsonButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await ReadAsync(JSON_FILE_NAME);
            ResultTextBlock.Text = result;
        }

        private IList<Car> BuildCars()
        {
            var cars = new List<Car>
            {
                new Car {Id = 1, Make = "Oldmobile", Model = "Cutlas Supreme", Year = 1995},
                new Car {Id = 2, Make = "Geo", Model = "Prism", Year = 1991},
                new Car {Id = 3, Make = "Ford", Model = "Escape", Year = 2005}
            };
            return cars;
        }

        private async Task WriteXmlAsync(IList<Car> cars)
        {
            var serializer = new DataContractSerializer(typeof (IList<Car>));
            using (
                var strem = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(XML_FILE_NAME,
                    CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(strem, cars);
            }
            ResultTextBlock.Text = "Write Succeeded";
        }

        private async Task WriteJsonAsync(IList<Car> cars)
        {
            var serializer = new DataContractJsonSerializer(typeof(IList<Car>));
            using (
                var strem = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(XML_FILE_NAME,
                    CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(strem, cars);
            }
            ResultTextBlock.Text = "Write Succeeded";
        }



        private async Task<string> ReadAsync(string file)
        {
            string content;
            var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(file);
            using (var reader = new StreamReader(stream))
            {
                content = await reader.ReadToEndAsync();
            }
            return content;
        }

        
    }
}
