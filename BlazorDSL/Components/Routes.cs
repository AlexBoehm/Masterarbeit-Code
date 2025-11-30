using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using static BlazorDSL.Html;

namespace BlazorDSL.Components;

public partial class Routes : WebComponent
{
    protected override Node Render() =>
        Component<Router>(
            [
                parameter("AppAssembly", typeof(Program).Assembly),
                templateParameter<Microsoft.AspNetCore.Components.RouteData>("Found", routeData =>
                    Tags(
                        Component<RouteView>([
                            parameter("RouteData", routeData),
                            parameter("DefaultLayout", typeof(Layout.MainLayout))
                        ]),

                        Component<FocusOnNavigate>([
                            parameter("RouteData", routeData),
                            parameter("Selector", "h1")
                        ])
                    )
                )
            ]
        );
}