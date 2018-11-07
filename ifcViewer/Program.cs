using System;
using Starcounter;

using ifcViewer.API;


namespace ifcViewer
{
    class Program
    {
        static void Main()
        {
            //var wexbimFileDestinationPath = "C:\\Users\\zno\\source\\repos\\ifcViewer\\ifcViewer\\wwwroot\\wexbim_files\\SampleHouse.wexbim";
            //WEXBIMFactory.CreateWEXFileFromIFCModel("SampleHouse.ifc", wexbimFileDestinationPath);

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            DefaultRoute.Register();
        }
    }
}