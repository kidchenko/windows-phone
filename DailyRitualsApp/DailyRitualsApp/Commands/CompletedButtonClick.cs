using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DailyRitualsApp.DataModel;

namespace DailyRitualsApp.Commands
{
    public class CompletedButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            App.Data.CompleteRitualToday((Ritual) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
