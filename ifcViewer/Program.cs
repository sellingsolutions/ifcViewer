using System;
using Starcounter;

using ifcViewer.API;
using ifcViewer.IFC;

namespace ifcViewer
{
    class Program
    {
        static void Main()
        {
            //var wexbimFileDestinationPath = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\wwwroot\\wexbim_files\\SampleHouse.wexbim";
            //WEXBIMFactory.CreateWEXFileFromIFCModel("SampleHouse.ifc", wexbimFileDestinationPath);

            string ModelName = "A2-400-V-0100000.ifc";
            string Path = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\IFC_Models\\" + ModelName;

            var ifcRepo = new IFCRepository(Path);
            ifcRepo.GetHierarchy(null);

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            DefaultRoute.Register();
        }
    }
}