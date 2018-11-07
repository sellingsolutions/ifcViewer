using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ifcViewer.Database
{
    public class Selection
    {
        public string Type { get; set; }
        public IEnumerable<ProductProperty> Props { get; set; }
    }
}
