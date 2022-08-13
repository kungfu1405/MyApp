#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75411c9aae76bf37a697f27df68e4018eedfc564"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Language_Index), @"mvc.1.0.view", @"/Views/Language/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75411c9aae76bf37a697f27df68e4018eedfc564", @"/Views/Language/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb7ec4032fcf1730a1b90be4f9301726cc4ca107", @"/Views/_ViewImports.cshtml")]
    public class Views_Language_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""card card-custom"">
    <div id=""pnlFilter"" class=""w-100 collapse"">
        <div class=""card-header"">
            <div class=""card-toolbar row"">
                <div class=""col-lg-4 col-sm-6 d-flex flex-fill align-items-center mb-3"">
                    <label class=""mr-3 mb-0 d-none d-md-block"">Table</label>
                    <input type=""text"" id=""txtTableName"" class=""form-control ctrl-filter"" placeholder=""Table Name"" />
                </div>
                <div class=""col-lg-4 col-sm-6 d-flex flex-fill align-items-center mb-3"">
                    <label class=""mr-3 mb-0 d-none d-md-block"">Enabled Only</label>
                    <span class=""switch switch-outline switch-icon switch-brand"">
                        <label>
                            <input type=""checkbox"" id=""chkViewEnabled"" checked=""checked"" />
                            <input type=""hidden"" id=""chkViewEnabled"" value=""true"" />
                            <span></span>
                        </label>
    ");
            WriteLiteral(@"                </span>
                </div>
            </div>
        </div>
    </div>
    <div class=""card-body"">
        <div class=""table-hover datatable datatable-bordered datatable-head-custom"" id=""ctbl_language""></div>
    </div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
    var container_language;
    $(document).ready(function () {
        container_language = $('#ctbl_language').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '");
#nullable restore
#line 38 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml"
                         Write(Url.Action("GetDatatable", "Language"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'
                    },
                },
                pageSize: 20,
                serverPaging: false,
                serverFiltering: true,
                serverSorting: false,
                saveState: false,
            },
            layout: {
                scroll: false,
                footer: false,
            },
            sortable: false,
            pagination: false,

            // columns definition
            columns: [
                { field: ""langCode"", title: ""#"", width: 30, overflow: 'visible', autoHide: false }
                , { field: ""iconUrl"", title: ""flag"", width: 40, overflow: 'visible', autoHide: false }
                , { field: ""name"", title: ""Name"", overflow: 'visible', autoHide: false }
                , { field: ""native"", title: ""Native"" }
                , {
                    field: ""isDefault"", title: ""Default"", width: 70, textAlign: ""center""
                    , template: function (row) {
                        if (row[""isDefaul");
                WriteLiteral(@"t""])
                            return '<span class=""badge badge-primary"">YES</span>';
                        return """";
                    }
                }
                , {
                    field: ""isActive"", title: ""Active"", width: 70, textAlign: ""center""
                    , template: function (row) {
                        var returnText = row[""isActive""] ? ""YES"" : ""NO"";
                        var resultHTML = """";
                        if (returnText == ""YES"")
                            resultHTML = '<span class=""badge badge-primary"">' + returnText + '</span>';
                        else
                            resultHTML = '<span class=""badge badge-light"">' + returnText + '</span>';
                        return resultHTML;
                    }
                }
                , {
                    field: ""Actions"", title: """", width: 35, textAlign: ""right"", overflow: 'visible', autoHide: false
                    , template: function (row) {
              ");
                WriteLiteral(@"          var resultHtml = '';

                        // TODO: Check Edit permission
                        var resultHtml = '\
                        <div class=""dropdown dropdown-inline"">\
                            <a href=""javascript:;"" class=""btn btn-sm btn-clean btn-icon"" data-toggle=""dropdown"" title=""Quick actions"">\
                                <i class=""ki ki-bold-more-ver""></i>\
                            </a>\
                            <div class=""dropdown-menu dropdown-menu-sm dropdown-menu-right"">\
                                <ul class=""navi flex-column navi-hover py-2"">\
                                    <li class=""navi-header font-weight-bolder text-uppercase font-size-xs text-primary pb-2"">\
                                        Choose an action:\
                                    </li>';

                        // TODO: Check Config language permission
                        resultHtml += '<li class=""navi-item"">\
                            <a href=""");
#nullable restore
#line 99 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml"
                                 Write(Url.Action("Index", "LanguageData"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/' + row.langCode + '"" \
                                    class=""navi-link "" title=""Config Language"">\
                                <span class=""navi-icon""><i class=""flaticon2-gear text-primary""></i></span>\
                                <span class=""navi-text"">Configuration</span>\
                            </a></li>';

                        // TODO: check Edit permission
                        resultHtml += '<li class=""navi-item"">\
                            <a href=""#"" data-title=""Edit Language ' + row.name + '"" \
                                    data-link=""");
#nullable restore
#line 108 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml"
                                           Write(Url.Action("AddEdit", "Language"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/' + row.langCode + '"" \
                                    data-kt-table=""ctbl_language"" \
                                    class=""navi-link btn-quickedit"" title=""Edit Language"">\
                                <span class=""navi-icon""><i class=""flaticon-edit-1""></i></span>\
                                <span class=""navi-text"">Edit record</span>\
                            </a></li>';

                        // TODO: check Delete permission
                        resultHtml += '<li class=""navi-item"">\
                                        <a class=""navi-link btn-delete"" data-toggle=""confirmation"" href=""#"" data-link=""");
#nullable restore
#line 117 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml"
                                                                                                                   Write(Url.Action("Delete"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/' + row.langCode + '"" >\
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

        $(document).on(""click"", "".btn-delete"", function (e) {
            e.preventDefault();
            var thisButton = $(this);

            var fndelete = function () {
                $.ajax({
                    url: thisButton.attr('data-link'),
                    type: ""DELETE"",
                    cache: false,
                    contentType: ""application");
                WriteLiteral(@"/json"",
                    beforeSend: function () {
                        thisButton.addClass('spinner spinner-primary spinner-right');
                        addOverlay('#ctbl_language', 5000);
                    },
                    success: function (msg) {
                        removeOverlay('#ctbl_language');
                        Swal.fire(
                            'Deleted!',
                            'Your record has been deleted.',
                            'success'
                        )
                        if ($('#pnlQuickView').hasClass('offcanvas-on')) {
                            KTUtil.getById('btn-quickview-toggle').click();
                        }
                        container_language.reload();
                    },
                    error: function (xhr) {
                        removeOverlay('#ctbl_language');
                        if ($('#pnlQuickView').hasClass('offcanvas-on')) {
                            KTUtil.getById('btn-q");
                WriteLiteral(@"uickview-toggle').click();
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
                WriteLiteral("\r\n    <a href=\"#\" \r\n       data-title=\"Add New Language\" \r\n       data-link=\"");
#nullable restore
#line 189 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\Language\Index.cshtml"
              Write(Url.Action("AddEdit", "Language"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""" 
       data-kt-table=""ctbl_language"" 
       class=""btn btn-sm btn-warning mr-1 btn-quickedit"" >
        <span class=""svg-icon svg-icon-sm mr-0"">
            <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                    <rect x=""0"" y=""0"" width=""24"" height=""24"" />
                    <circle fill=""#000000"" opacity=""0.3"" cx=""12"" cy=""12"" r=""10"" />
                    <path d=""M11,11 L11,7 C11,6.44771525 11.4477153,6 12,6 C12.5522847,6 13,6.44771525 13,7 L13,11 L17,11 C17.5522847,11 18,11.4477153 18,12 C18,12.5522847 17.5522847,13 17,13 L13,13 L13,17 C13,17.5522847 12.5522847,18 12,18 C11.4477153,18 11,17.5522847 11,17 L11,13 L7,13 C6.44771525,13 6,12.5522847 6,12 C6,11.4477153 6.44771525,11 7,11 L11,11 Z"" fill=""#000000"" />
                </g>
            </svg>
        </span>
        <span class=""d-none d-sm-inli");
                WriteLiteral(@"ne"">Add New</span>
    </a>
    <a id=""btnReload"" href=""#"" class=""btn btn-sm btn-success mr-1"">
        <span class=""svg-icon svg-icon-sm mr-0"">
            <svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                <g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
                    <rect x=""0"" y=""0"" width=""24"" height=""24"" />
                    <path d=""M8.43296491,7.17429118 L9.40782327,7.85689436 C9.49616631,7.91875282 9.56214077,8.00751728 9.5959027,8.10994332 C9.68235021,8.37220548 9.53982427,8.65489052 9.27756211,8.74133803 L5.89079566,9.85769242 C5.84469033,9.87288977 5.79661753,9.8812917 5.74809064,9.88263369 C5.4720538,9.8902674 5.24209339,9.67268366 5.23445968,9.39664682 L5.13610134,5.83998177 C5.13313425,5.73269078 5.16477113,5.62729274 5.22633424,5.53937151 C5.384723,5.31316892 5.69649589,5.25819495 5.92269848,5.4165837 L6.72910242,5.98123382 C8.16546398,4.72182424 10.023");
                WriteLiteral(@"9806,4 12,4 C16.418278,4 20,7.581722 20,12 C20,16.418278 16.418278,20 12,20 C7.581722,20 4,16.418278 4,12 L6,12 C6,15.3137085 8.6862915,18 12,18 C15.3137085,18 18,15.3137085 18,12 C18,8.6862915 15.3137085,6 12,6 C10.6885336,6 9.44767246,6.42282109 8.43296491,7.17429118 Z"" fill=""#000000"" fill-rule=""nonzero"" />
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591