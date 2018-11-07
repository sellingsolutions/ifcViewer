using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using ifcViewer.ViewModels;

namespace ifcViewer.API
{
    public class DefaultRoute
    {
        public static void Register()
        {
            Handle.GET("/ifcViewer/partial", () =>
            {
                Session.Ensure();
                return Self.GET("/ifcViewer");
            });

            Handle.GET("/ifcViewer", () =>
            {
                Session.Ensure();
                return Db.Scope(() =>
                {
                    return new Viewer { };
                });
            });

            
        }
    }
}
