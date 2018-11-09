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

       
        public string GetHierarchy (IIfcObjectDefinition o)
        {
            var Hierarchy = PrintHierarchy(o, 0);
            return Hierarchy;
        }
        private string PrintHierarchy(IIfcObjectDefinition o, int level)
        {
            var Hierarchy = string.Format("{0}{1} [{2}]", GetIndent(level), o.Name, o.GetType().Name);

            //only spatial elements can contain building elements
            var spatialElement = o as IIfcSpatialStructureElement;
            if (spatialElement != null)
            {
                //using IfcRelContainedInSpatialElement to get contained elements
                var containedElements = spatialElement.ContainsElements.SelectMany(rel => rel.RelatedElements);
                foreach (var element in containedElements)
                    Hierarchy += string.Format("{0}    ->{1} [{2}]", GetIndent(level), element.Name, element.GetType().Name) + "\n";
            }

            //using IfcRelAggregares to get spatial decomposition of spatial structure elements
            foreach (var item in o.IsDecomposedBy.SelectMany(r => r.RelatedObjects))
                Hierarchy += PrintHierarchy(item, level + 1) + "\n";

            return Hierarchy;
        }

        private static string GetIndent(int level)
        {
            var indent = "";
            for (int i = 0; i < level; i++)
                indent += "  ";
            return indent;
        }
    }
}
