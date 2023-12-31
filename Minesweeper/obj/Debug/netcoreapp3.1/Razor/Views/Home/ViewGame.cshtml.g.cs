#pragma checksum "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68c5c404b4e256625122266017cfb5b032661cc2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ViewGame), @"mvc.1.0.view", @"/Views/Home/ViewGame.cshtml")]
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
#line 1 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\_ViewImports.cshtml"
using Minesweeper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\_ViewImports.cshtml"
using Minesweeper.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68c5c404b4e256625122266017cfb5b032661cc2", @"/Views/Home/ViewGame.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6b5e2b17d1473d4172a6fae55492c009436ab92", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ViewGame : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Minesweeper.Models.GameModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table class=""table"">
    <thead>
        <tr>
            <th>ID</th>
            <th>Game</th>
            <th>Status</th>
            <th>Created Date</th>
            <th>Modified Date</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 15 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
     foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 17 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
           Write(Html.DisplayFor(modelItem => item.ID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 18 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 19 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 20 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
           Write(Html.DisplayFor(modelItem => item.CreatedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 21 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
           Write(Html.DisplayFor(modelItem => item.ModifiedDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n");
#nullable restore
#line 23 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
                 if (item.Status == "InProgress") {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
               Write(Html.ActionLink("Continue", "ContinueGame", new { id = item.ID }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" | ");
#nullable restore
#line 25 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
                                    
                } else {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-muted\">Continue</span>\r\n");
            WriteLiteral(" | ");
#nullable restore
#line 28 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
                                    
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <!-- Delete link with confirmation dialog -->\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 1122, "\"", 1176, 1);
#nullable restore
#line 31 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
WriteAttributeValue("", 1129, Url.Action("DeleteGame", new { id = item.ID }), 1129, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"return confirm(\'Are you sure you want to delete this game?\');\">Delete</a>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 34 "C:\OneDrive\BS Software Development\CST-350\CLC\Project\cst350-clc\Minesweeper\Views\Home\ViewGame.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Minesweeper.Models.GameModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
