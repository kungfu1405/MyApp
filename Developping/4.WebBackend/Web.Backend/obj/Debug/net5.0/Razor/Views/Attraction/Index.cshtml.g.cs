#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c4012373c03cf25162779ba630555867a02987a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Attraction_Index), @"mvc.1.0.view", @"/Views/Attraction/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4012373c03cf25162779ba630555867a02987a4", @"/Views/Attraction/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb7ec4032fcf1730a1b90be4f9301726cc4ca107", @"/Views/_ViewImports.cshtml")]
    public class Views_Attraction_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AttractionListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Partials/_alertMessage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddEdit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-warning mr-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
  
    ViewData["Title"] = "Attraction";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card card-custom"">
    <div id=""pnlFilter"" class=""w-100 collapse"">
        <div class=""card-header"">
            <div class=""card-toolbar row"">
                <div class=""col-lg-4 col-sm-6 d-flex flex-fill align-items-center mb-3"">
                    <label class=""mr-3 mb-0 d-none d-md-block"">Attraction</label>
                    <input type=""text"" id=""txtName"" class=""form-control ctrl-filter"" placeholder=""Attraction key"" />
                </div>
                <div class=""col-lg-4 col-sm-6 d-flex flex-fill align-items-center mb-3"">
                    <label class=""mr-3 mb-0 d-none d-md-block"">Destination</label>
                    <select id=""ddlDestination""
                            class=""form-control ctrl-filter"" style=""width: 100%"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c4012373c03cf25162779ba630555867a02987a46673", async() => {
                WriteLiteral("-All-");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </select>\r\n                </div>\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"card-body\">\r\n        <div class=\"table-hover datatable datatable-bordered datatable-head-custom\" id=\"ctbl_attraction\"></div>\r\n    </div>\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c4012373c03cf25162779ba630555867a02987a48466", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 36 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.AlertMessages;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
    var ctbl_attraction;
    $(document).ready(function () {

        $(""#ddlDestination"").select2({
            minimumInputLength: 2,
            placeholder: ""Search Destination"",
            allowClear: true,
            ajax: {
                url: '");
#nullable restore
#line 48 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                 Write(Url.Action("Search", "Destination"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                dataType: 'json',
                delay: 250,
                cache: true,
                data: function (params) {
                    var queryParameters = {
                        keyword: params.term
                    }
                    return queryParameters;
                }
            }
        });
");
                WriteLiteral("\r\n        ctbl_attraction = $(\'#ctbl_attraction\').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: \'remote\',\r\n                source: {\r\n                    read: {\r\n                        url: \'");
#nullable restore
#line 85 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                         Write(Url.Action("GetDatatable"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                        params: {
                            DestinationId: function () { return $('#ddlDestination').val(); },
                            Name: function () { return $('#txtName').val(); }
                        }
                    },
                },
                pageSize: 10,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
                saveState: false,
            },
            layout: {
                scroll: true,
                footer: false,
            },
            sortable: true,
            pagination: true,

            // columns definition
            columns: [
                { field: ""defaultName"", title: ""Attraction name"" }
                , { field: ""destination.defaultName"", title: ""Destination"" }
                , { field: ""routeUri"", title: ""Uri"" }
                , { field: ""address"", title: ""Address"" }
                , {
                    field: """", title: """);
                WriteLiteral("Attraction Type\", template: function (row) {\r\n                        var textString = \"\";\r\n                        debugger\r\n                        if ((row.attractionTypes & ");
#nullable restore
#line 115 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                               Write((int)EnumAttractionType.PlaceToEat);

#line default
#line hidden
#nullable disable
                WriteLiteral(") != 0)\r\n                        { textString += \"Place to eat , \"; }\r\n                        if ((row.attractionTypes & ");
#nullable restore
#line 117 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                               Write((int)EnumAttractionType.PlaceToStay);

#line default
#line hidden
#nullable disable
                WriteLiteral(") != 0) { textString += \"Place to stay , \"; }\r\n                        if ((row.attractionTypes & ");
#nullable restore
#line 118 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                               Write((int)EnumAttractionType.PlaceToVisit);

#line default
#line hidden
#nullable disable
                WriteLiteral(@") != 0) {
                            textString += ""Place to visit , "";
                        }
                        return textString;
                    }
                }
                , {
                    field: ""Tourist"", title: ""Tourist Classes"", template: function (row) {
                        var textReturn = """";
                        if ((row.touristClasses & ");
#nullable restore
#line 127 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                              Write((int)EnumTouristClass.Budget);

#line default
#line hidden
#nullable disable
                WriteLiteral(") !=0 ) { textReturn += \"Budget , \"; }\r\n                        if ((row.touristClasses & ");
#nullable restore
#line 128 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                              Write((int)EnumTouristClass.Luxury);

#line default
#line hidden
#nullable disable
                WriteLiteral(") != 0) { textReturn += \"Luxury , \"; }\r\n                        if ((row.touristClasses & ");
#nullable restore
#line 129 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                              Write((int)EnumTouristClass.MidRange);

#line default
#line hidden
#nullable disable
                WriteLiteral(@") != 0) { textReturn += ""MidRange , ""; }
                        return textReturn;
                    }
                }
                , {
                    field: ""status"", title: ""Status"", template: function (row) {
                        if (row.status == ");
#nullable restore
#line 135 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                      Write((int)EnumPostStatus.Spam);

#line default
#line hidden
#nullable disable
                WriteLiteral(")\r\n                            return \"Spam\"\r\n                        if (row.status == ");
#nullable restore
#line 137 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                      Write((int)EnumPostStatus.Draft);

#line default
#line hidden
#nullable disable
                WriteLiteral(")\r\n                            return \"Draft\"\r\n                        if (row.status == ");
#nullable restore
#line 139 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                      Write((int)EnumPostStatus.Published);

#line default
#line hidden
#nullable disable
                WriteLiteral(")\r\n                            return \"Published\"\r\n                          if (row.status == ");
#nullable restore
#line 141 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                        Write((int)EnumPostStatus.Approving);

#line default
#line hidden
#nullable disable
                WriteLiteral(")\r\n                              return \"Approving\"\r\n                          if (row.status == ");
#nullable restore
#line 143 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                        Write((int)EnumPostStatus.Approved);

#line default
#line hidden
#nullable disable
                WriteLiteral(@")
                              return ""Approved""

                    }
                }

                , {
                    field: ""Actions"", title: """", sortable: false, width: 35, textAlign: ""right"", overflow: 'visible', autoHide: false
                    , template: function (row) {
                        var resultHtml = '\
                        <div class=""dropdown dropdown-inline"">\
                            <a href=""javascript:;"" class=""btn btn-sm btn-clean btn-icon"" data-toggle=""dropdown"" title=""Quick actions"">\
                                <i class=""ki ki-bold-more-ver""></i>\
                            </a>\
                            <div class=""dropdown-menu dropdown-menu-sm dropdown-menu-right"">\
                                <ul class=""navi flex-column navi-hover py-2"">\
                                    <li class=""navi-header font-weight-bolder text-uppercase font-size-xs text-primary pb-2"">\
                                        Choose an action:\
     ");
                WriteLiteral("                               </li>\\\r\n                                        \';\r\n");
                WriteLiteral("\r\n                        // TODO: check Edit permission\r\n                        resultHtml += \'<li class=\"navi-item\">\\\r\n                                        <a class=\"navi-link\" href=\"");
#nullable restore
#line 173 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                                               Write(Url.Action("AddEdit"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/' + row.id + '"" >\
                                            <span class=""navi-icon""><i class=""flaticon-edit-1""></i></span>\
                                            <span class=""navi-text"">Edit record</span>\
                                        </a></li>';

                        // TODO: check Delete permission
                        resultHtml += '<li class=""navi-item"">\
                                        <a class=""navi-link btn-delete"" data-toggle=""confirmation"" href=""#"" data-link=""");
#nullable restore
#line 180 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Attraction\Index.cshtml"
                                                                                                                   Write(Url.Action("Delete"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/' + row.id + '"" >\
                                            <span class=""navi-icon""><i class=""flaticon-delete-1""></i></span>\
                                            <span class=""navi-text"">Delete record</span>\
                                        </a></li>';
                        resultHtml += '</ul></div></div>';
                        return resultHtml;
                    }
                }
            ],
            translate: {
                records: {
                    noRecords: ""No records found"",
                    processing: ""Loading..."",
                }
            }
        });

        $('#btnReload').click(function (e) {
            ctbl_attraction.reload();
        });

        $('.ctrl-filter').change(function (e) {
            ctbl_attraction.reload();
        });

        $(document).on(""click"", "".btn-delete"", function (e) {
            e.preventDefault();
            var thisButton = $(this);

            var fndelete = function () {
  ");
                WriteLiteral(@"              $.ajax({
                    url: thisButton.attr('data-link'),
                    type: ""DELETE"",
                    cache: false,
                    contentType: ""application/json"",
                    beforeSend: function () {
                        thisButton.addClass('spinner spinner-primary spinner-right');
                        addOverlay('#ctbl_attraction', 5000);
                    },
                    success: function (msg) {
                        removeOverlay('#ctbl_attraction');
                        Swal.fire(
                            'Deleted!',
                            'Your record has been deleted.',
                            'success'
                        )
                        if ($('#pnlQuickView').hasClass('offcanvas-on')) {
                            KTUtil.getById('btn-quickview-toggle').click();
                        }
                        ctbl_attraction.reload();
                    },
                    error: fun");
                WriteLiteral(@"ction (xhr) {
                        removeOverlay('#ctbl_attraction');
                        if ($('#pnlQuickView').hasClass('offcanvas-on')) {
                            KTUtil.getById('btn-quickview-toggle').click();
                        }
                        KTUtil.find(thisButton, '.spinner').removeClass('spinner spinner-primary spinner-right');

                        Swal.fire(
                            'Delete failed!',
                            xhr.responseText,
                            'danger'
                        );
                    }
                });
            }

            if ($(thisButton).attr('data-toggle') == 'confirmation') {
                ConfirmDelete(fndelete);
            } else {
                fndelete();
            }

        });
    });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Toolbar", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c4012373c03cf25162779ba630555867a02987a423333", async() => {
                    WriteLiteral(@"
        <span class=""svg-icon svg-icon-sm mr-0"">
            <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                    <rect x=""0"" y=""0"" width=""24"" height=""24"" />
                    <circle fill=""#000000"" opacity=""0.3"" cx=""12"" cy=""12"" r=""10"" />
                    <path d=""M11,11 L11,7 C11,6.44771525 11.4477153,6 12,6 C12.5522847,6 13,6.44771525 13,7 L13,11 L17,11 C17.5522847,11 18,11.4477153 18,12 C18,12.5522847 17.5522847,13 17,13 L13,13 L13,17 C13,17.5522847 12.5522847,18 12,18 C11.4477153,18 11,17.5522847 11,17 L11,13 L7,13 C6.44771525,13 6,12.5522847 6,12 C6,11.4477153 6.44771525,11 7,11 L11,11 Z"" fill=""#000000"" />
                </g>
            </svg>
        </span>
        <span class=""d-none d-sm-inline"">Add New</span>
    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <a class=""btn btn-sm btn-primary mr-1"" data-toggle=""collapse"" href=""#pnlFilter"" role=""button"" aria-expanded=""false"" title=""Show/hide filters"">
        <span class=""svg-icon svg-icon-sm mr-0"">
            <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                    <rect x=""0"" y=""0"" width=""24"" height=""24"" />
                    <path d=""M5,4 L19,4 C19.2761424,4 19.5,4.22385763 19.5,4.5 C19.5,4.60818511 19.4649111,4.71345191 19.4,4.8 L14,12 L14,20.190983 C14,20.4671254 13.7761424,20.690983 13.5,20.690983 C13.4223775,20.690983 13.3458209,20.6729105 13.2763932,20.6381966 L10,19 L10,12 L4.6,4.8 C4.43431458,4.5790861 4.4790861,4.26568542 4.7,4.1 C4.78654809,4.03508894 4.89181489,4 5,4 Z"" fill=""#000000"" />
                </g>
            </svg>
        </span>
        <span class=""d-none d-sm-inline"">Filter</span>
 ");
                WriteLiteral(@"   </a>
    <a id=""btnReload"" href=""#"" class=""btn btn-sm btn-success mr-1"">
        <span class=""svg-icon svg-icon-sm mr-0"">
            <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                    <rect x=""0"" y=""0"" width=""24"" height=""24"" />
                    <path d=""M8.43296491,7.17429118 L9.40782327,7.85689436 C9.49616631,7.91875282 9.56214077,8.00751728 9.5959027,8.10994332 C9.68235021,8.37220548 9.53982427,8.65489052 9.27756211,8.74133803 L5.89079566,9.85769242 C5.84469033,9.87288977 5.79661753,9.8812917 5.74809064,9.88263369 C5.4720538,9.8902674 5.24209339,9.67268366 5.23445968,9.39664682 L5.13610134,5.83998177 C5.13313425,5.73269078 5.16477113,5.62729274 5.22633424,5.53937151 C5.384723,5.31316892 5.69649589,5.25819495 5.92269848,5.4165837 L6.72910242,5.98123382 C8.16546398,4.72182424 10.0239806,4 12,4 C16.41827");
                WriteLiteral(@"8,4 20,7.581722 20,12 C20,16.418278 16.418278,20 12,20 C7.581722,20 4,16.418278 4,12 L6,12 C6,15.3137085 8.6862915,18 12,18 C15.3137085,18 18,15.3137085 18,12 C18,8.6862915 15.3137085,6 12,6 C10.6885336,6 9.44767246,6.42282109 8.43296491,7.17429118 Z"" fill=""#000000"" fill-rule=""nonzero"" />
                </g>
            </svg>
        </span>
        <span class=""d-none d-sm-inline"">Reload</span>
    </a>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AttractionListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591