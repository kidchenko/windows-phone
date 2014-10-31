using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BindingToCommands
{
    public class Car : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        private string _checkInDateTime;

        public string CheckedInDateTime
        {
            get { return _checkInDateTime; }
            set
            {
                _checkInDateTime = value;
                OnPropertyChanged();
            } 
        }

        public ICommand CheckInCommand { get; set; } = new CheckInButtonClick();

        public void CheckInCar()
        {
            CheckedInDateTime = DateTime.Now.ToString("t");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CheckInButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((Car)parameter).CheckInCar();
        }

        public event EventHandler CanExecuteChanged;
    }
}
