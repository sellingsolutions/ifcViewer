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
        public static IEnumerable<IIfcPropertySingleValue> GetPropsForProduct(string ifcProductID)
        {
            const string Path = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\IFC_Models\\SampleHouse.ifc";
            using (var model = IfcStore.Open(Path))
            {
                var objects = model.Instances.Where(o => o.EntityLabel > 0);

                var product = model.Instances.Where<IIfcProduct>(p => p.GlobalId == ifcProductID).FirstOrDefault();
                if (product == null)
                {
                    return new List<IIfcPropertySingleValue>();
                }

                var properties = product.IsDefinedBy
                    .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                    .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                    .OfType<IIfcPropertySingleValue>();

                foreach (var property in properties)
                    Console.WriteLine($"Property: {property.Name}, Value: {property.NominalValue}");

                return properties;
            }
        }
    }
}
