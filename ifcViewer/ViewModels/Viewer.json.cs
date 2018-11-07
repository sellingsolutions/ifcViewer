using Starcounter;
using System;
using System.Collections.Generic;

using ifcViewer.IFC;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace ifcViewer.ViewModels
{
    partial class Viewer : Json
    {
        static Viewer()
        {

        }
        void Handle(Input.ProductID action)
        {
            var ProductID = action.Value as string;
            IEnumerable<IIfcPropertySingleValue> Props = IFCRepository.GetPropsForProduct(ProductID);

            var Description = "";
            foreach (IIfcPropertySingleValue Prop in Props)
            {
                Description += $"{Prop.Description}\n";
            }

            this.ifcProduct = Description;
        }
    }
}
