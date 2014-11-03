using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DailyRitualsApp.DataModel
{
    public class DataSource
    {
        private ObservableCollection<Ritual> _rituals;
        private const string FILE_NAME = "data.json";

        public DataSource()
        {
            _rituals = new ObservableCollection<Ritual>();
        }

        public async void AddRitual(Ritual ritual)
        {
            _rituals.Add(ritual);
            await SaveRitualDataAsync();
        }

        public async Task<ObservableCollection<Ritual>> GetRituals()
        {
            await EnsureDataLoaded();
            return _rituals;
        }

        public async void CompleteRitualToday(Ritual ritual)
        {
            var index = _rituals.IndexOf(ritual);
            _rituals[index].AddDate();
            await SaveRitualDataAsync();
        }

        private async Task EnsureDataLoaded()
        {
            if (_rituals.Any()) return;
            await GetRitualDataAsync();
        }

        

        private async Task GetRitualDataAsync()
        {
            if (_rituals.Any()) return;
            var serializer = new DataContractJsonSerializer(typeof (ObservableCollection<Ritual>));
            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(FILE_NAME))
                {
                    _rituals = (ObservableCollection<Ritual>) serializer.ReadObject(stream);
                }
            }
            catch
            {
                _rituals = new ObservableCollection<Ritual>();
            }
        }

        private async Task SaveRitualDataAsync()
        {
            var serializer = new DataContractJsonSerializer(typeof (ObservableCollection<Ritual>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, _rituals);
                
            }
        }
    }
}
