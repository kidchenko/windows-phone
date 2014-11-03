using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DailyRitualsApp.Commands;

namespace DailyRitualsApp.DataModel
{
    public class Ritual : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<DateTime> Dates { get; set; } = new ObservableCollection<DateTime>();

        [IgnoreDataMember]
        public ICommand CompletedCommand { get; set; } = new CompletedButtonClick();

        public void AddDate()
        {
            Dates.Add(DateTime.Today);
            OnPropertyChanged("Dates");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
