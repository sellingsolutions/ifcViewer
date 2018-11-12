using Starcounter;
using System;
using System.Collections.Generic;

using ifcViewer.IFC;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using ifcViewer.Database;

namespace ifcViewer.ViewModels
{
    partial class Viewer : Json, IBound<ifcViewer.Database.Selection>
    {
        private IFCRepository IfcRepo { get; set; }
        public Viewer()
        {
            IfcRepo = new IFCRepository();
        }
        static Viewer()
        {
            DefaultTemplate.Props.ElementType.InstanceType = typeof(PropertyPanel);
        }

        void Handle(Input.ProductID action)
        {
            var ProductID = action.Value as string;
            var Product = IfcRepo.GetProduct(ProductID);
            var Hierarchy = IfcRepo.GetHierarchy(Product);
            
            var ProductType = Product.GetType().ToString().Replace("Xbim.Ifc2x3.", "");
            var Properties = IfcRepo.GetPropsForProduct(Product);


            var NewSelection = new Selection()
            {
                Type = ProductType
            };

            var _Properties = new List<ProductProperty>();

            foreach (IIfcPropertySingleValue Prop in Properties)
            {
                var ProductProp = new ProductProperty()
                {
                    Property = Prop.Name,
                    Value = Prop.NominalValue.ToString()
                };

                _Properties.Add(ProductProp);
            }
            NewSelection.Props = _Properties;

            this.Data = NewSelection;
        }
    }
}
