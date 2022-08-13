#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e009b90c6268fb76aa0544b3acd90cc1e577b481"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pillar_Hashtag), @"mvc.1.0.view", @"/Views/Pillar/Hashtag.cshtml")]
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
#line 1 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using Web.Frontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using Web.Frontend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using DbData.Protos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using DbData.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using Mic.Core.Website;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using Mic.UserDb.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using Mic.Core.DataTypes;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using DbData.Entities.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\_ViewImports.cshtml"
using System.Text.RegularExpressions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e009b90c6268fb76aa0544b3acd90cc1e577b481", @"/Views/Pillar/Hashtag.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a4d097f9d68cef47ff13841e00a8ff32f6a019b", @"/Views/_ViewImports.cshtml")]
    public class Views_Pillar_Hashtag : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HashtagModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmSearch"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("position-relative"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
  
    ViewData["Title"] = Model.Hashtag;
    var banner = "adventure&exploration.png";
    switch (Model.TypeSearch)
    {
        case EnumTypeSearch.Story:
            banner = "adventure&exploration.png";
            break;
        case EnumTypeSearch.Collection:
            banner = "cities&culture.png";
            break;
        case EnumTypeSearch.PlaceToVisit:
            banner = "nature&preservation.png";
            break;
        case EnumTypeSearch.PlaceToStay:
            banner = "wellness&retreats.png";
            break;
        case EnumTypeSearch.PlaceToEat:
            banner = "wine&dine.png";
            break;
        default:
            banner = "adventure&exploration.png";
            break;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-xxl m-auto container-pillar\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            <div class=\"col-12\">\r\n                <div class=\"pillar_banner position-relative\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 1014, "\"", 1041, 2);
            WriteAttributeValue("", 1020, "/img/Pillar/", 1020, 12, true);
#nullable restore
#line 33 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 1032, banner, 1032, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />                    \r\n                    <div class=\"position-absolute text-center bottom-0 row w-100\">\r\n                        <div class=\"col-xl-8 mx-auto form-group\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e009b90c6268fb76aa0544b3acd90cc1e577b4817171", async() => {
                WriteLiteral(@"
                                <label class=""placeholder""><ion-icon name=""search-outline""></ion-icon> Search anything & explore the world out there!</label>
                                <input class=""form-control w-100 position-absolute top-0"" id=""inpSearch""");
                BeginWriteAttribute("value", " value=\"", 1615, "\"", 1639, 1);
#nullable restore
#line 38 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 1623, Model.Keyword, 1623, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" autocomplete=\"off\" name=\"q\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 1680, "\"", 1694, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                                <input type=""submit""
                                       style=""position: absolute; left: -9999px; width: 1px; height: 1px;""
                                       tabindex=""-1"" />
                                <input type=""hidden"" name=""type"" id=""txtTypeSearch""");
                BeginWriteAttribute("value", " value=\"", 2002, "\"", 2044, 1);
#nullable restore
#line 42 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 2010, EnumTypeSearch.Story.ToString(), 2010, 34, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                <input type=\"hidden\" name=\"page\" id=\"txtPage\" value=\"1\" />\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 36 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
AddHtmlAttributeValue("", 1275, string.Format("/Pillars/{0}", Model.Hashtag), 1275, 47, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"container\">\r\n        <div class=\"home-txt\">\r\n            <h3>Top Picks of #");
#nullable restore
#line 54 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                          Write(Model.Hashtag);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
            <p class=""text-center"">by Our Awesome Community!</p>
        </div>
        <div class=""home-tab-top row mb-xl-5"">
            <div class=""col-12"">
                <div class=""row"">
                    <div class=""offset-xl-1 col-xl-10 col-md-12 col-sm-12 text-center"">
");
#nullable restore
#line 61 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                         foreach (EnumTypeSearch type in (EnumTypeSearch[])Enum.GetValues(typeof(EnumTypeSearch)))
                        {
                            if (Model.TypeSearch.Equals(type))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <a class=\"btn mx-2 my-2 btn-toppick active\">");
#nullable restore
#line 65 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                                                        Write(dEnum.GetDescription(type));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 66 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <a class=\"btn mx-2 my-2 btn-toppick btn-type-search\" data-type=\"");
#nullable restore
#line 69 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                                                                            Write(type.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\">");
#nullable restore
#line 69 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                                                                                                Write(dEnum.GetDescription(type));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 70 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                            }
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"container\">\r\n        <div class=\"row pillar-list\">\r\n");
#nullable restore
#line 80 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
             foreach (var itm in Model.ListExperience)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""col-xl-6 col-lg-6 col-md-6 col-sm-12 mb-4"">
                    <div class=""home-item home-item-horizontal row mb-0"">
                        <div class=""col-6 py-0 px-2"">
                            <div class=""thumb"">
                                <a");
            BeginWriteAttribute("class", " class=\"", 3896, "\"", 3904, 0);
            EndWriteAttribute();
            BeginWriteAttribute("href", " href=\"", 3905, "\"", 3959, 1);
#nullable restore
#line 86 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 3912, string.Format("/Experience/{0}", itm.RouteUri), 3912, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <img class=\"rounded\" onerror=\"imgError(this)\"");
            BeginWriteAttribute("src", " src=\"", 4044, "\"", 4112, 1);
#nullable restore
#line 87 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 4050, string.Format("{0}{1}", UrlList.FileServer, itm.ThumbnailUrl), 4050, 62, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                </a>
                            </div>
                        </div>

                        <div class=""content col-6"">
                            <a class=""mb-2 d-block font-size-14"">STORY</a>
                            <h3><a");
            BeginWriteAttribute("href", " href=\"", 4389, "\"", 4443, 1);
#nullable restore
#line 94 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
WriteAttributeValue("", 4396, string.Format("/Experience/{0}", itm.RouteUri), 4396, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 94 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                                                                     Write(itm.ExperienceLanguage.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                            <p>#N of N Category</p>\r\n                            <p>N Have been there</p>\r\n                            <p>N Saves . ");
#nullable restore
#line 97 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                    Write(string.Format("{0} Comments", itm.TotalComments));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>
                        <span class=""fea-rating"">
                            <label>92</label>
                            <label>95</label>
                        </span>
                    </div>
                </div>
");
#nullable restore
#line 105 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 107 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
         if(Model.TotalRecords > 0){

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"pagination-list pillar-paging text-center mt-5\" id=\"paging\"></div>\r\n");
#nullable restore
#line 109 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
        }        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<style>\r\n    .heade-menu-top .nav_pillar span {\r\n        display: block;\r\n        color: #303030;\r\n        word-break: break-all;\r\n    }\r\n");
#nullable restore
#line 118 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
     if (!string.IsNullOrEmpty(Model.Keyword))
    {
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        .pillar_banner div input:valid {\r\n            background-color: #fff;\r\n        }\r\n        ");
#nullable restore
#line 124 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
               
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    \r\n</style>\r\n<script type=\"text/javascript\">\r\n    $(document).ready(function () {\r\n        $(\".nav_story\").removeClass(\"active\");\r\n        $(\".heade-menu-top .nav_pillar span\").html(\"#");
#nullable restore
#line 131 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                                                 Write(Model.Hashtag);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n\r\n        $(\'.pillar-paging\').pagination({\r\n            items: ");
#nullable restore
#line 134 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
               Write(Model.TotalRecords);

#line default
#line hidden
#nullable disable
            WriteLiteral(", // Total number of items that will be used to calculate the pages.\r\n            itemsOnPage: ");
#nullable restore
#line 135 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                     Write(Model.Items);

#line default
#line hidden
#nullable disable
            WriteLiteral(", // Number of items displayed on each page.\r\n            displayedPages: 3,// How many page numbers should be visible while navigating.\r\n            currentPage: ");
#nullable restore
#line 137 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Pillar\Hashtag.cshtml"
                     Write(Model.Page);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
            cssStyle: '',
            prevText: '<ion-icon name=""caret-back-outline""></ion-icon>',
            nextText: '<ion-icon name=""caret-forward-outline""></ion-icon>',
            onInit: function () {
                // fire first page loading
            },
            onPageClick: function (page, evt) {
                $(""#txtPage"").val(page);
                $(""#frmSearch"").submit();
            }
        });

        $("".btn-type-search"").on(""click"", function () {
            var typeS = $(this).attr(""data-type"");
            $(""#txtTypeSearch"").val(typeS);
            $(""#frmSearch"").submit();
        });

        $(""#inpSearch"").on(""keyup"", function () {
            if ($(this).val().length > 0)
                $(this).css(""background-color"", ""#fff"");
            else
                $(this).css(""background-color"", ""transparent"");
        });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HashtagModel> Html { get; private set; }
    }
}
#pragma warning restore 1591