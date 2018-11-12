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
        private const string ModelName = "A2-400-V-0100000.ifc";
        private const string Path = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\IFC_Models\\" + ModelName;

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

       
        public List<IIfcObjectDefinition> GetHierarchy (IIfcObjectDefinition Object)
        {
            var Hierarchy = PrintHierarchy(Object, 0);
            return Hierarchy;
        }
        private List<IIfcObjectDefinition> PrintHierarchy(IIfcObjectDefinition Object, int level)
        {
            var Hierarchy = new List<IIfcObjectDefinition>();

            var Spaces = Model.Instances.Where<IIfcTypeObject>(s=>s.EntityLabel > 0);

            IIfcElement Element = Object as IIfcElement;

            // If we're dealing with an element: Wall, Door, Window..
            // Then we'll have a direct connection to the Spatial Structure that it's contained in
            if (Element != null)
            {
                var Relation = Element.ContainedInStructure.FirstOrDefault() as IIfcRelContainedInSpatialStructure;
                var RelatingStructure = Relation.RelatingStructure;
                Hierarchy.Add(RelatingStructure);

                Console.WriteLine();
            }



            //only spatial elements can contain building elements
            IIfcSpatialStructureElement SpatialElement = Object as IIfcSpatialStructureElement;
            if (SpatialElement != null)
            {
                //using IfcRelContainedInSpatialElement to get contained elements
                var containedElements = SpatialElement.ContainsElements.SelectMany(rel => rel.RelatedElements);
                foreach (var element in containedElements)
                {

                }

            }

            //using IfcRelAggregares to get spatial decomposition of spatial structure elements
            foreach (var item in Object.IsDecomposedBy.SelectMany(r => r.RelatedObjects))
            {

            }

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
