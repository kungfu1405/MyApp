#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9bad03985f54e4713fd62e1497512a53b188198"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Destination_Detail), @"mvc.1.0.view", @"/Views/Destination/Detail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Web.Backend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Web.Backend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Mic.Core.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using DbData.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Mic.UserDb.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Mic.Core.DataTypes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\_ViewImports.cshtml"
using Mic.Core.Website;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9bad03985f54e4713fd62e1497512a53b188198", @"/Views/Destination/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb7ec4032fcf1730a1b90be4f9301726cc4ca107", @"/Views/_ViewImports.cshtml")]
    public class Views_Destination_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EDestination>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">Name</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 6 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.DefaultName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">Country</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 13 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.CountryName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">State</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 19 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.StateName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">City</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 26 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.CityName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">Latitude</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 32 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.Latitude);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <span class=\"text-dark-75 font-weight-bolder mr-2\">Longitude</span>\r\n    <span class=\"text-dark-50 text-hover-primary\">\r\n        <span>");
#nullable restore
#line 39 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
         Write(Model.Longitude);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    </span>\r\n</div>\r\n\r\n\r\n<div class=\"separator separator-dashed my-3\"></div>\r\n<div class=\"d-flex justify-content-between align-items-center my-3\">\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 1677, "\"", 1731, 1);
#nullable restore
#line 46 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
WriteAttributeValue("", 1684, Url.Action("AddEdit", new { id = Model.Id }), 1684, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">\r\n        <i class=\"la la-edit\"></i>Edit\r\n    </a>\r\n    <a href=\"#\" data-link=\"");
#nullable restore
#line 49 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Destination\Detail.cshtml"
                       Write(Url.Action("Delete", new { id = Model.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n       data-toggle=\"confirmation\" class=\"btn btn-danger btn-delete\">\r\n        <i class=\"flaticon-delete-1\"></i> Delete\r\n    </a>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EDestination> Html { get; private set; }
    }
}
#pragma warning restore 1591