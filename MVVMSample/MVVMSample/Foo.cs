using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVMSample
{
    public class Foo : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        // CallerMemberName Permite obter o nome do método ou propriedade que está chamando o método.
        // see http://msdn.microsoft.com/pt-br/library/system.runtime.compilerservices.callermembernameattribute%28v=vs.110%29.aspx
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            // null propagation c# 6.0 see http://davefancher.com/2014/08/14/c-6-0-null-propagation-operator/
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
