#pragma checksum "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "97585287eff3d1cdc4150628ee82bf7dac83b998"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DForm_AddEdit), @"mvc.1.0.view", @"/Views/DForm/AddEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"97585287eff3d1cdc4150628ee82bf7dac83b998", @"/Views/DForm/AddEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fb7ec4032fcf1730a1b90be4f9301726cc4ca107", @"/Views/_ViewImports.cshtml")]
    public class Views_DForm_AddEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Web.Backend.Models.DynamicForm.DFormViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AddEdit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Partials/_alertMessage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "DForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary mr-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
  
    ViewData["Title"] = Model.Table?.DisplayName;
    Model.ReturnUrl = string.Format("{0}{1}", Context.Request.Path, Context.Request.QueryString);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\" id=\"pnlDForm\">\r\n    <div class=\"col-lg-5\">\r\n        <div class=\"card card-custom\">\r\n            <div class=\"card-header\">\r\n                <div class=\"card-title\">\r\n                    <h3 class=\"card-label\">\r\n                        ");
#nullable restore
#line 13 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
                   Write(Model.Table.DisplayName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <small>");
#nullable restore
#line 14 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
                          Write(Model.ActionMode.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral(" record</small>\r\n                    </h3>\r\n                </div>\r\n            </div>\r\n            <div class=\"card-body\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "97585287eff3d1cdc4150628ee82bf7dac83b9987416", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 19 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

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
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "97585287eff3d1cdc4150628ee82bf7dac83b9989054", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 24 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
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
            DefineSection("Toolbar", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "97585287eff3d1cdc4150628ee82bf7dac83b99810744", async() => {
                    WriteLiteral("\r\n        <i class=\"la la-undo\"></i> Return\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-fid", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
                                                    WriteLiteral(Model.TableId);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fid"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fid", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fid"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 30 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
     if (Model.ActionMode == FormActionMode.Edit)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <a class=\"btn btn-danger btn-delete mr-2\" data-command=\"delete\">\r\n            <i class=\"flaticon-delete-1\"></i> Delete\r\n        </a>\r\n");
#nullable restore
#line 35 "E:\Microtec\Project\travel-hub\svn\4.WebBackend\Web.Backend\Views\DForm\AddEdit.cshtml"
    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    <a class=""btn btn-warning btn-reset mr-2"">
        <i class=""la la-undo-alt""></i> Reset
    </a>
    <div class=""btn-group"">
        <a class=""btn btn-primary btn-submit""><i class=""la la-save""></i> Save</a>
        <button type=""button"" class=""btn btn-primary dropdown-toggle dropdown-toggle-split"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
            <span class=""sr-only"">Save Options</span>
        </button>
        <div class=""dropdown-menu"">
            <a class=""dropdown-item btn-submit"" href=""#""><i class=""la la-save""></i> Save</a>
            <a class=""dropdown-item btn-submit"" href=""#"" data-command=""continue""><i class=""la la-save""></i> Save & Continue</a>
        </div>
    </div>
");
            }
            );
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "97585287eff3d1cdc4150628ee82bf7dac83b99814936", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {
            $('.btn-reset').click(function (e) {
                e.preventDefault();
                $('#pnlDForm form').trigger(""reset"");
                $('#pnlDForm .select2').trigger(""change""); 
            });

            $('.btn-submit').click(function (e) {
                e.preventDefault();      
                SubmitForm($(this), '#pnlDForm');
            });
            $('.btn-delete').click(function (e) {
                e.preventDefault();
                var btDelete = $(this);
                ConfirmDelete(function () {
                    SubmitForm(btDelete, '#pnlDForm');
                });
            });
        });

    </script>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Backend.Models.DynamicForm.DFormViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591