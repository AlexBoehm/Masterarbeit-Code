using Microsoft.AspNetCore.Components.Web;
using static BlazorDSL.Html;

namespace BlazorDSL.Components;

public partial class App : WebComponent
{        
    protected override Node Render() =>
        Tags(
            DocTypeHtml(),

            html(
                [lang("en")],
                head(
                    meta([charset("utf-8")]),
                    meta([name("viewport"), content("width=device-width, initial-scale=1.0")]),
                    @base([href("/")]),
                    link([rel("stylesheet"), href("bootstrap/bootstrap.min.css")]),
                    link([rel("stylesheet"), href("app.css")]),
                    link([rel("stylesheet"), href("BlazorDSL.styles.css")]),
                    link([rel("icon"), type("image/png"), href("favicon.png")]),
                    Component<HeadOutlet>(RenderMode.InteractiveServer)
                ),
                body(
                    [],
                    Component<Routes>(RenderMode.InteractiveServer),
                    script([src("_framework/blazor.web.js")])
                )
            )
        );    
}
