﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC
{
    public static LanceTrack.Web.Features.Account.AccountController Account = new LanceTrack.Web.Features.Account.T4MVC_AccountController();
    public static LanceTrack.Web.Features.Home.HomeController Home = new LanceTrack.Web.Features.Home.T4MVC_HomeController();
    public static LanceTrack.Web.Features.TrackTime.TrackTimeController TrackTime = new LanceTrack.Web.Features.TrackTime.T4MVC_TrackTimeController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Libs {
        private const string URLPATH = "~/Libs";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class angular {
            private const string URLPATH = "~/Libs/angular";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string angular_ui_router_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/angular-ui-router.min.js") ? Url("angular-ui-router.min.js") : Url("angular-ui-router.js");
            public static readonly string angular_ui_router_min_js = Url("angular-ui-router.min.js");
            public static readonly string angular_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/angular.min.js") ? Url("angular.min.js") : Url("angular.js");
            public static readonly string angular_min_js = Url("angular.min.js");
            public static readonly string angular_min_js_map = Url("angular.min.js.map");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class bootstrap {
            private const string URLPATH = "~/Libs/bootstrap";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class css {
                private const string URLPATH = "~/Libs/bootstrap/css";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string bootstrap_theme_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap-theme.min.css") ? Url("bootstrap-theme.min.css") : Url("bootstrap-theme.css");
                     
                public static readonly string bootstrap_theme_css_map = Url("bootstrap-theme.css.map");
                public static readonly string bootstrap_theme_min_css = Url("bootstrap-theme.min.css");
                public static readonly string bootstrap_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.css") ? Url("bootstrap.min.css") : Url("bootstrap.css");
                     
                public static readonly string bootstrap_css_map = Url("bootstrap.css.map");
                public static readonly string bootstrap_min_css = Url("bootstrap.min.css");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class fonts {
                private const string URLPATH = "~/Libs/bootstrap/fonts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string glyphicons_halflings_regular_eot = Url("glyphicons-halflings-regular.eot");
                public static readonly string glyphicons_halflings_regular_svg = Url("glyphicons-halflings-regular.svg");
                public static readonly string glyphicons_halflings_regular_ttf = Url("glyphicons-halflings-regular.ttf");
                public static readonly string glyphicons_halflings_regular_woff = Url("glyphicons-halflings-regular.woff");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class js {
                private const string URLPATH = "~/Libs/bootstrap/js";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string bootstrap_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/bootstrap.min.js") ? Url("bootstrap.min.js") : Url("bootstrap.js");
                public static readonly string bootstrap_min_js = Url("bootstrap.min.js");
                public static readonly string npm_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/npm.min.js") ? Url("npm.min.js") : Url("npm.js");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class jquery {
            private const string URLPATH = "~/Libs/jquery";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string jquery_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.min.js") ? Url("jquery.min.js") : Url("jquery.js");
            public static readonly string jquery_min_js = Url("jquery.min.js");
            public static readonly string jquery_min_map = Url("jquery.min.map");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Momentjs {
            private const string URLPATH = "~/Libs/Momentjs";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string locales_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/locales.min.js") ? Url("locales.min.js") : Url("locales.js");
            public static readonly string locales_min_js = Url("locales.min.js");
            public static readonly string moment_with_locales_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/moment-with-locales.min.js") ? Url("moment-with-locales.min.js") : Url("moment-with-locales.js");
            public static readonly string moment_with_locales_min_js = Url("moment-with-locales.min.js");
            public static readonly string moment_min_js = Url("moment.min.js");
        }
    
        public static readonly string normalize_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/normalize.min.css") ? Url("normalize.min.css") : Url("normalize.css");
             
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class typings {
            private const string URLPATH = "~/Libs/typings";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string angular_ui_router_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/angular-ui-router.d.min.js") ? Url("angular-ui-router.d.min.js") : Url("angular-ui-router.d.js");
            public static readonly string angular_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/angular.d.min.js") ? Url("angular.d.min.js") : Url("angular.d.js");
            public static readonly string jquery_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.d.min.js") ? Url("jquery.d.min.js") : Url("jquery.d.js");
            public static readonly string moment_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/moment.d.min.js") ? Url("moment.d.min.js") : Url("moment.d.js");
            public static readonly string underscore_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/underscore.d.min.js") ? Url("underscore.d.min.js") : Url("underscore.d.js");
            public static readonly string urls_d_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/urls.d.min.js") ? Url("urls.d.min.js") : Url("urls.d.js");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class underscore {
            private const string URLPATH = "~/Libs/underscore";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string underscore_min_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/underscore-min.min.js") ? Url("underscore-min.min.js") : Url("underscore-min.js");
            public static readonly string underscore_min_map = Url("underscore-min.map");
            public static readonly string underscore_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/underscore.min.js") ? Url("underscore.min.js") : Url("underscore.js");
        }
    
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Features {
        private const string URLPATH = "~/Features";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Account {
            private const string URLPATH = "~/Features/Account";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Models {
                private const string URLPATH = "~/Features/Account/Models";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class styles {
                private const string URLPATH = "~/Features/Account/styles";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string login_less = Url("login.less");
                public static readonly string login_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/login.min.css") ? Url("login.min.css") : Url("login.css");
                     
                public static readonly string login_css_map = Url("login.css.map");
                public static readonly string login_min_css = Url("login.min.css");
            }
        
            public static readonly string UI_resx = Url("UI.resx");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Views {
                private const string URLPATH = "~/Features/Account/Views";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Home {
            private const string URLPATH = "~/Features/Home";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Views {
                private const string URLPATH = "~/Features/Home/Views";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Shared {
            private const string URLPATH = "~/Features/Shared";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class scripts {
                private const string URLPATH = "~/Features/Shared/scripts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string app_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/app.min.js") ? Url("app.min.js") : Url("app.js");
                public static readonly string dateService_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/dateService.min.js") ? Url("dateService.min.js") : Url("dateService.js");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class styles {
                private const string URLPATH = "~/Features/Shared/styles";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string app_less = Url("app.less");
                public static readonly string app_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/app.min.css") ? Url("app.min.css") : Url("app.css");
                     
                public static readonly string app_css_map = Url("app.css.map");
                public static readonly string app_min_css = Url("app.min.css");
            }
        
            public static readonly string ValidationMessages_resx = Url("ValidationMessages.resx");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class TrackTime {
            private const string URLPATH = "~/Features/TrackTime";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class scripts {
                private const string URLPATH = "~/Features/TrackTime/scripts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string module_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/module.min.js") ? Url("module.min.js") : Url("module.js");
                public static readonly string trackTimeController_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/trackTimeController.min.js") ? Url("trackTimeController.min.js") : Url("trackTimeController.js");
                public static readonly string trackTimeService_ts = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/trackTimeService.min.js") ? Url("trackTimeService.min.js") : Url("trackTimeService.js");
            }
        
        }
    
        public static readonly string web_config = Url("web.config");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009


