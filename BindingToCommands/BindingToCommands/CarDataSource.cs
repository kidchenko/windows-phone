using System.Collections.ObjectModel;
using System.Linq;

namespace BindingToCommands
{
    public class CarDataSource
    {
        private static readonly ObservableCollection<Car> Cars = new ObservableCollection<Car>();

        public static ObservableCollection<Car> GetCars()
        {
            if (Cars.Any()) return Cars;
            Cars.Add(new Car { Id = 1, Make = "Olds", Model = "Cutlas"});
            Cars.Add(new Car { Id = 2, Make = "Geo", Model = "Prism" });
            Cars.Add(new Car { Id = 3, Make = "Ford", Model = "Pinto" });
            return Cars;
        }
    }
}