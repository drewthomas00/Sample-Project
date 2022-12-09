using Microsoft.AspNetCore.Components.Rendering;
using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;
using VanderbiltFarms.Web.BlazorServerMvcPattern.Controllers;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern
{
    // AppView is the default app view. If you created a new Blazor application this class will be names App. We renamed it to AppView 
    public class AppController : ControllerComponentBase<AppView>
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            AppendRenderFragment<HeartbeatController>(builder);
            base.BuildRenderTree(builder);
        }
    }
}
