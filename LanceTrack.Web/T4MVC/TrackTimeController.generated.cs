// <auto-generated />
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
namespace T4MVC
{
    public class TrackTimeController
    {

        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string MyTimeTemplate = "MyTimeTemplate";
                public readonly string Refs = "Refs";
                public readonly string TimeCellTemplate = "TimeCellTemplate";
                public readonly string TrackTimeTemplate = "TrackTimeTemplate";
                public readonly string UsersTimeTemplate = "UsersTimeTemplate";
            }
            public readonly string MyTimeTemplate = "~/Features/TrackTime/MyTimeTemplate.cshtml";
            public readonly string Refs = "~/Features/TrackTime/Refs.cshtml";
            public readonly string TimeCellTemplate = "~/Features/TrackTime/TimeCellTemplate.cshtml";
            public readonly string TrackTimeTemplate = "~/Features/TrackTime/TrackTimeTemplate.cshtml";
            public readonly string UsersTimeTemplate = "~/Features/TrackTime/UsersTimeTemplate.cshtml";
            static readonly _ModelsClass s_Models = new _ModelsClass();
            public _ModelsClass Models { get { return s_Models; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _ModelsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                }
            }
            static readonly _scriptsClass s_scripts = new _scriptsClass();
            public _scriptsClass scripts { get { return s_scripts; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _scriptsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                }
            }
            static readonly _stylesClass s_styles = new _stylesClass();
            public _stylesClass styles { get { return s_styles; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _stylesClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                }
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
