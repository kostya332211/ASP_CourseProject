#pragma checksum "D:\ChessProject\ChessProject\Chess.Web\Views\Statistic\TopPlayers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f753ab8fca6e41fc783f70f47fb8b8da8bfc301c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Statistic_TopPlayers), @"mvc.1.0.view", @"/Views/Statistic/TopPlayers.cshtml")]
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
#line 1 "D:\ChessProject\ChessProject\Chess.Web\Views\_ViewImports.cshtml"
using Chess.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ChessProject\ChessProject\Chess.Web\Views\_ViewImports.cshtml"
using Chess.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f753ab8fca6e41fc783f70f47fb8b8da8bfc301c", @"/Views/Statistic/TopPlayers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10039505a518bacd68619d3f2dcfb0f0067793dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Statistic_TopPlayers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<nav>
    <div class=""nav nav-tabs"" id=""nav-tab"" role=""tablist"">
        <a class=""nav-item nav-link active"" data-toggle=""tab"" href=""#nav-bullet"" role=""tab"" aria-controls=""nav-home"" aria-selected=""true"">Bullet</a>
        <a class=""nav-item nav-link"" data-toggle=""tab"" href=""#nav-blitz"" role=""tab"" aria-controls=""nav-profile"" aria-selected=""false"">Blitz</a>
        <a class=""nav-item nav-link"" data-toggle=""tab"" href=""#nav-rapid"" role=""tab"" aria-controls=""nav-contact"" aria-selected=""false"">Rapid</a>
        <a class=""nav-item nav-link""  data-toggle=""tab"" href=""#nav-pawns"" role=""tab"" aria-controls=""nav-home"" aria-selected=""false"">Only pawns</a>
        <a class=""nav-item nav-link"" data-toggle=""tab"" href=""#nav-knights"" role=""tab"" aria-controls=""nav-profile"" aria-selected=""false"">Only knights</a>
        <a class=""nav-item nav-link"" data-toggle=""tab"" href=""#nav-queens"" role=""tab"" aria-controls=""nav-contact"" aria-selected=""false"">Only queens</a>
    </div>
</nav>

<div class=""tab-content"" id=""nav-tabConte");
            WriteLiteral(@"nt"">
<div class=""tab-pane fade show active"" id=""nav-bullet"" role=""tabpanel"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Rating</th>
        </tr>
        </thead>
        <tbody id=""topBulletBody"">
       
        </tbody>
    </table>
</div>

<div class=""tab-pane fade"" id=""nav-blitz"" role=""tabpanel"" aria-labelledby=""nav-profile-tab"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Rating</th>
        </tr>
        </thead>
        <tbody id=""topBlitzBody"">
        
        </tbody>
    </table>
</div>

<div class=""tab-pane fade"" id=""nav-ra");
            WriteLiteral(@"pid"" role=""tabpanel"" aria-labelledby=""nav-contact-tab"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Rating</th>
        </tr>
        </thead>
        <tbody id=""topRapidBody"">
       
        </tbody>
    </table>
</div>

<div class=""tab-pane fade"" id=""nav-pawns"" role=""tabpanel"" aria-labelledby=""nav-home-tab"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Rating</th>
        </tr>
        </thead>
        <tbody id=""topPawnsBody"">
       
        </tbody>
    </table>
</div>


<div class=""tab-pane fade"" id=""nav-knights"" role=""tabpanel"" ar");
            WriteLiteral(@"ia-labelledby=""nav-profile-tab"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Nickname</th>
        </tr>
        </thead>
        <tbody id=""topKnightsBody"">
        
        </tbody>
    </table>
</div>

<div class=""tab-pane fade"" id=""nav-queens"" role=""tabpanel"" aria-labelledby=""nav-contact-tab"">
    <table class=""table"">
        <thead class=""thead-light"">
        <tr>
            <th scope=""col"">#</th>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col"">Nickname</th>
            <th scope=""col"">Rating</th>
        </tr>
        </thead>
        <tbody id=""topQueensBody"">
       
        </tbody>
    </table>
</div>

</div>

<script>
    $.get(""/api/statistic/topPlayersBlitz"").then(result =");
            WriteLiteral(@"> {
        var markup = '';
        for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.blitzRating}</th>`;
            markup += '</tr>';
        }
        document.getElementById('topBlitzBody').innerHTML = markup;
    });

    $.get(""/api/statistic/topPlayersBullet"").then(result => {
        var markup = '';
        for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.bulletRating}</th>`;
            ");
            WriteLiteral(@"markup += '</tr>';
        }
        document.getElementById('topBulletBody').innerHTML = markup;
    });

    $.get(""/api/statistic/topPlayersRapid"").then(result => {
        var markup = '';
        for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.rapidRating}</th>`;
            markup += '</tr>';
        }
        document.getElementById('topRapidBody').innerHTML = markup;
    });

    $.get(""/api/statistic/topPlayersPawnsMode"").then(result => {
        console.log(result);
        var markup = '';
        for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;");
            WriteLiteral(@"
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.onlyPawnsRating}</th>`;
            markup += '</tr>';
        }
        document.getElementById('topPawnsBody').innerHTML = markup;
    });

    $.get(""/api/statistic/topPlayersKnightsMode"").then(result => {
        var markup = '';
        for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.onlyKnightsRating}</th>`;
            markup += '</tr>';
        }
        document.getElementById('topKnightsBody').innerHTML = markup;
    });

    $.get(""/api/statistic/topPlayersQueensMode"").then(result => {
        var markup = '';
      ");
            WriteLiteral(@"  for (var i = 0; i < result.length; i++) {
            markup += '<tr>';
            markup += `<th scope=""row"">${i + 1}</th>`;
            markup += `<th>${result[i].firstName}</th>`;
            markup += `<th>${result[i].lastName}</th>`;
            markup += `<th>${result[i].nickname}</th>`;
            markup += `<th>${result[i].playerDetails.onlyQueensRating}</th>`;
            markup += '</tr>';
        }
        document.getElementById('topQueensBody').innerHTML = markup;
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
