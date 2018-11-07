using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace ifcViewer.IFC
{
    public class IFCRepository
    {
        private const string Path = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\IFC_Models\\SampleHouse.ifc";
        private IfcStore Model { get; set; }

        public IFCRepository()
        {
            Model = IfcStore.Open(Path);
        }
        public IIfcObject GetProduct(string ifcProductID)
        {
            int ProductID = Convert.ToInt32(ifcProductID);

            var Product = Model.Instances.Where(o => o.EntityLabel == ProductID).FirstOrDefault() as IIfcObject;
            return Product;
        }
        public IEnumerable<IIfcPropertySingleValue> GetPropsForProduct(IIfcObject Product)
        {
            if (Product == null)
            {
                return new List<IIfcPropertySingleValue>();
            }

            var Properties = Product.IsDefinedBy
                .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                .OfType<IIfcPropertySingleValue>();

            return Properties;
        }
    }
}
