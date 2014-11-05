using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace MapNotesApp.DataModel
{
    public class DataSource
    {
        private ObservableCollection<MapNote> _mapNotes = new ObservableCollection<MapNote>();
        private const string FILE_NAME = "data.json";

        public async Task<ObservableCollection<MapNote>> GetMapNotes()
        {
            await EnsureDataLoaded();
            return _mapNotes;
        }

        public async void AddMapNote(MapNote mapNote)
        {
            _mapNotes.Add(mapNote);
            await SaveMapNoteDataAsync();
        }

        public async void DeleteMapNote(MapNote mapNote)
        {
            _mapNotes.Remove(mapNote);
            await SaveMapNoteDataAsync();
        }

        private async Task EnsureDataLoaded()
        {
            if (_mapNotes.Any()) return;
            await GetMapNoteDataAsync();
        }

        private async Task GetMapNoteDataAsync()
        {
            if (_mapNotes.Any()) return;
            var serializer = new DataContractJsonSerializer(typeof (ObservableCollection<MapNote>));
            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(FILE_NAME))
                {
                    _mapNotes = (ObservableCollection<MapNote>) serializer.ReadObject(stream);
                }
            }
            catch
            {
                _mapNotes = new ObservableCollection<MapNote>();
            }
        }

        private async Task SaveMapNoteDataAsync()
        {
            var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<MapNote>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, _mapNotes);
            }
        }
    }
}