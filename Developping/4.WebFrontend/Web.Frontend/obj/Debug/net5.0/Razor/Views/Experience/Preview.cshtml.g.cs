#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7dd91d450e3aa23803f527aa6ab6fbb3c9dede5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Experience_Preview), @"mvc.1.0.view", @"/Views/Experience/Preview.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7dd91d450e3aa23803f527aa6ab6fbb3c9dede5f", @"/Views/Experience/Preview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a4d097f9d68cef47ff13841e00a8ff32f6a019b", @"/Views/_ViewImports.cshtml")]
    public class Views_Experience_Preview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ExperienceDetailModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/user-default.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("rounded"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/icon-no-image.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
  
    ViewData["Title"] = Model.Experience.ExperienceLanguage.Title;
    var countSection = 0;
    var countSectionScroll = 0;
    var mainSection = Model.Experience.ExperienceSessions.First();
    //var regex = new Regex(@"(?<=#)\w*");
    var regex = new Regex(@"#\w*");

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container previewStory m-auto mt-48\">\r\n    <div class=\"offset-xl-1 col-xl-10\">\r\n        <div class=\"text-center w-100\">\r\n            <a class=\"btn btn-toppick active\">Save All</a>\r\n            <a class=\"btn btn-toppick mx-3 deactive\"");
            BeginWriteAttribute("href", " href=\"", 559, "\"", 633, 1);
#nullable restore
#line 14 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 566, string.Format("/Experience/Edit/{0}", Model.Experience.RouteUri), 566, 67, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n        </div>\r\n\r\n        <div class=\"home-txt pt-36\">\r\n            <p class=\"text-center mb-12 font-size-14 font-weight-light\">");
#nullable restore
#line 18 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                                   Write(Model.Experience.PublishDate.ToString("MMM dd, yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" . 5 Minute Read . Wrote by ");
#nullable restore
#line 18 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                                                                                                                      Write(Model.User.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h1 class=\"mb-24\">");
#nullable restore
#line 19 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                          Write(Model.Experience.ExperienceLanguage.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <div class=\"text-center\">\r\n                ");
#nullable restore
#line 21 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
           Write(Html.Raw(regex.Replace(mainSection.Detail, "<a href=\"/Pillars/$&\" class=\"hashtag\">$&</a>")));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>

        <div class=""preview-rating row d-flex"">
            <div class=""col-4 text-center"">
                <label class=""color-yel"">87</label>
                <span>Critical Critics</span>
            </div>
            <div class=""col-4 text-center"">
                <label>100</label>
                <span>Audience Rating</span>
            </div>
            <div class=""col-4 text-center"">
                <label>651K</label>
                <span>Questions, Tip & Tricks</span>
            </div>
        </div>
    </div>

");
#nullable restore
#line 41 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
     foreach (var session in Model.Experience.ExperienceSessions)
    {
        var firstImg = session.Images.FirstOrDefault();
        countSection++;
        if (countSection == 1) continue;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div");
            BeginWriteAttribute("class", " class=\"", 1912, "\"", 1964, 4);
            WriteAttributeValue("", 1920, "w-100", 1920, 5, true);
            WriteAttributeValue(" ", 1925, "preview-detail", 1926, 15, true);
            WriteAttributeValue(" ", 1940, "section-", 1941, 9, true);
#nullable restore
#line 46 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 1949, countSection, 1949, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"preview-detail-thumb\"");
            BeginWriteAttribute("id", " id=\"", 2013, "\"", 2037, 2);
            WriteAttributeValue("", 2018, "thumb-", 2018, 6, true);
#nullable restore
#line 47 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 2024, session.Id, 2024, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <div class=\"swiper-wrapper\">\r\n");
#nullable restore
#line 49 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                     if (session.Images.Count() > 0)
                    {
                        foreach (var img in session.Images)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <img class=\"swiper-slide\" onerror=\"imgError(this)\"");
            BeginWriteAttribute("alt", " alt=\"", 2330, "\"", 2352, 1);
#nullable restore
#line 53 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 2336, session.Title, 2336, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 2353, "\"", 2420, 1);
#nullable restore
#line 53 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 2359, String.Format("{0}{1}", UrlList.FileServer, img.ImagerUrl), 2359, 61, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 54 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                        }
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <img src=\"/img/icon-no-image.svg\" />\r\n");
#nullable restore
#line 59 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div class=\"swiper-pagination swiper-pagination-image-experience\"></div>\r\n            </div>\r\n            <div class=\"preview-detail-content\">\r\n                <h2>");
#nullable restore
#line 64 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                Write(session.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 65 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                 if (session.Attraction != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p hidden><a class=\"color-yel\"");
            BeginWriteAttribute("href", " href=\"", 2956, "\"", 3027, 1);
#nullable restore
#line 67 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 2963, string.Format("/Attraction/{0}", session.Attraction.RouteUri), 2963, 64, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 67 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                                                                                       Write(session.Attraction.DefaultName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></p>\r\n");
#nullable restore
#line 68 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"preview-detail-destination\">\r\n                    ");
#nullable restore
#line 70 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
               Write(Html.Raw(regex.Replace(session.Detail, "<a href=\"/Pillars/$&\" class=\"hashtag\">$&</a>")));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"preview-detail-author text-right mb-48\">\r\n                    <p class=\"font-size-14\">\r\n                        <a class=\"preview-detail-author-img\"");
            BeginWriteAttribute("href", " href=\"", 3463, "\"", 3525, 1);
#nullable restore
#line 74 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 3470, string.Format("/Account/Profile/{0}", Model.User.Id), 3470, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 75 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                             if (!string.IsNullOrEmpty(Model.User.Picture))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <img onerror=\"imgError(this)\"");
            BeginWriteAttribute("src", " src=\"", 3698, "\"", 3770, 1);
#nullable restore
#line 77 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 3704, String.Format("{0}{1}", UrlList.FileServer, Model.User.Picture), 3704, 66, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 78 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7dd91d450e3aa23803f527aa6ab6fbb3c9dede5f16365", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 82 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </a>\r\n                        ");
#nullable restore
#line 84 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                   Write(session.CreateDate.ToString("MMM dd, yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" . By\r\n                        <a class=\"preview-detail-author-name\"");
            BeginWriteAttribute("href", " href=\"", 4139, "\"", 4201, 1);
#nullable restore
#line 85 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 4146, string.Format("/Account/Profile/{0}", Model.User.Id), 4146, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            ");
#nullable restore
#line 86 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                        Write(Model.User.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </a>
                    </p>
                </div>
            </div>

            <div class=""preview-detail-social"">
                <a class=""btn btn-toppick"">Save</a>
                <a class=""btn btn-toppick btn-preview-detail-share""><ion-icon name=""share-social-outline""></ion-icon></a>
            </div>
        </div>
");
#nullable restore
#line 97 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
</div>
<div class=""container relatedStory m-auto p-0"">
    <div class=""home-txt"">
        <h3 class=""mb-48"">Related Stories You May Like, Curated for You</h3>
    </div>
    <div class=""container p-0"">
        <div class=""row related-list position-relative p-0"">
");
#nullable restore
#line 106 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
             foreach (var itm in Model.ListExperienceRelated.Take(3).ToList())
            {
                if (itm.Id.Equals(Model.Experience.Id))
                {
                    continue;
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-xl-4 col-lg-6 col-md-6 col-sm-12 mb-48\">\r\n                    <div class=\"home-item\">\r\n                        <div class=\"w-100\">\r\n                            <div class=\"thumb m-auto\">\r\n                                <a");
            BeginWriteAttribute("class", " class=\"", 5374, "\"", 5382, 0);
            EndWriteAttribute();
            BeginWriteAttribute("href", " href=\"", 5383, "\"", 5437, 1);
#nullable restore
#line 116 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 5390, string.Format("/Experience/{0}", itm.RouteUri), 5390, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 117 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                     if (!string.IsNullOrEmpty(itm.ThumbnailUrl))
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <img class=\"rounded\" onerror=\"imgError(this)\"");
            BeginWriteAttribute("src", " src=\"", 5648, "\"", 5716, 1);
#nullable restore
#line 119 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 5654, string.Format("{0}{1}", UrlList.FileServer, itm.ThumbnailUrl), 5654, 62, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 120 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7dd91d450e3aa23803f527aa6ab6fbb3c9dede5f22064", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 124 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </a>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"content\">\r\n                            <p>#N of N Category</p>\r\n                            <h3><a");
            BeginWriteAttribute("href", " href=\"", 6216, "\"", 6270, 1);
#nullable restore
#line 130 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 6223, string.Format("/Experience/{0}", itm.RouteUri), 6223, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 130 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                                                     Write(itm.ExperienceLanguage.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                            <p>10.8km from Center Point . 15h </p>\r\n                            <p>N Stops By . N Saves . ");
#nullable restore
#line 132 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                 Write(string.Format("{0} Comments", itm.TotalComments));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                            <p>#Adventure #Cities&Culture #Vietnam</p>
                        </div>
                        <span class=""fea-rating"">
                            <label class=""like""><i></i> 128</label>
                            <label class=""comment""><i></i> 100</label>
                        </span>
                    </div>
                </div>
");
#nullable restore
#line 141 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 143 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
         if (Model.ListExperienceRelated.Count > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"pagination-list related-paging text-center mt-5\" id=\"paging\"></div>\r\n");
#nullable restore
#line 146 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n<div class=\"simple-pagination pagination-list previewStory-pagination\" hidden>\r\n    <ul>\r\n");
#nullable restore
#line 152 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
         foreach (var session in Model.Experience.ExperienceSessions)
        {
            countSectionScroll++;
            if (countSectionScroll == 1) continue;

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a data-section=\"");
#nullable restore
#line 156 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                             Write(countSectionScroll);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" onclick=\"scrollToSection(this)\"");
            BeginWriteAttribute("class", " class=\"", 7437, "\"", 7499, 2);
#nullable restore
#line 156 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
WriteAttributeValue("", 7445, (countSectionScroll - 1) == 1 ? "current" : "", 7445, 49, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 7494, "my-2", 7495, 5, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 156 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
                                                                                                                                                   Write(countSectionScroll - 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 157 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <li><a class=""mt-16""><ion-icon name=""flame-outline""></ion-icon></a></li>
        <li><a><ion-icon name=""earth-outline""></ion-icon></a></li>
    </ul>
</div>

<script>
    $(document).ready(function () {
        var swiper = new Swiper('.preview-detail-thumb', {
            slidesPerView: 1,
            pagination: {
                el: '.swiper-pagination',
                type: 'bullets',
            },
        });

        $("".previewStory-pagination a"").on(""click"", function () {
            $("".previewStory-pagination a"").removeClass(""current"");
            $(this).addClass(""current"");
        });

        $('.related-paging').pagination({
            items: ");
#nullable restore
#line 179 "E:\Microtec\Project\travel-hub\svn\4.WebFrontend\Web.Frontend\Views\Experience\Preview.cshtml"
               Write(Model.ListExperienceRelated.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@", // Total number of items that will be used to calculate the pages.
            itemsOnPage: 3, // Number of items displayed on each page.
            displayedPages: 3,// How many page numbers should be visible while navigating.
            currentPage: 1,
            cssStyle: '',
            prevText: '<ion-icon name=""caret-back-outline""></ion-icon>',
            nextText: '<ion-icon name=""caret-forward-outline""></ion-icon>',
            onInit: function () {
                // fire first page loading
            },
            onPageClick: function (page, evt) {
                // some code
            }
        });
    });

    function scrollToSection(obj) {
        var section = $(obj).attr(""data-section"");
        var topScroll = $("".section-"" + section).offset().top - 50;
        $(""html, body"").stop().animate({ scrollTop: topScroll }, 500, 'swing');
    }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ExperienceDetailModel> Html { get; private set; }
    }
}
#pragma warning restore 1591