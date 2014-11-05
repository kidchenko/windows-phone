using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotesApp.DataModel
{
    public class MapNote
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
