using VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers;

namespace VanderbiltFarms.Web.BlazorServerMvcPattern.Events.Shared
{
    public class ApplicationHeartbeatEvent : IUiBusEvent
    {
        public DateTime Time { get; }

        public ApplicationHeartbeatEvent(DateTime time)
        {
            Time = time;
        }
    }
}
