using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MapNotesApp.DataModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MapNotesApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMapNote : Page
    {
        private bool _isViewing;
        private MapNote _mapNote;
        public AddMapNote()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Geopoint point;
            // MUST ENABLE LOCATION CAPABILITY!!!
            if (e.Parameter == null)
            {
                _isViewing = false;
                var locator = new Geolocator { DesiredAccuracyInMeters = 50 };
                var position = await locator.GetGeopositionAsync();
                point = position.Coordinate.Point;
            }
            else
            {
                _isViewing = true;
                _mapNote = (MapNote) e.Parameter;
                TitleTextBox.Text = _mapNote.Title;
                NoteTextBox.Text = _mapNote.Note;
                AddButton.Content = "Delete";

                var position = new BasicGeoposition
                {
                    Latitude = _mapNote.Latitude,
                    Longitude = _mapNote.Longitude
                };
                point = new Geopoint(position);
            }
            await MyMapControl.TrySetViewAsync(point, 16D);
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isViewing)
            {
                var dialog = new MessageDialog("Are you sure?", "Delete Map note");
                dialog.Commands.Add(new UICommand("Delete", CommandInvokedHandler));
                dialog.Commands.Add(new UICommand("Cancel", CommandInvokedHandler));
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;
                await dialog.ShowAsync();
            }
            else
            {
                var mapNote = new MapNote
                {
                    Created = DateTime.Now,
                    Latitude = MyMapControl.Center.Position.Latitude,
                    Longitude = MyMapControl.Center.Position.Longitude,
                    Note = NoteTextBox.Text,
                    Title = TitleTextBox.Text
                };
                App.Data.AddMapNote(mapNote);
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MainPage));
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label.Equals("Delete"))
            {
                App.Data.DeleteMapNote(_mapNote);
                Frame.Navigate(typeof (MainPage));
            }
        }
    }
}
