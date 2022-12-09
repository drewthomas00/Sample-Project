namespace VanderbiltFarms.Web.BlazorServerMvcPattern.BlazorMvcHelpers
{
    public interface IUiBus
    {
        void Register(IListener listener);
        void UnRegister(IListener listener);
        IListener<T>[] GetListeners<T>() where T : IUiBusEvent;
        void Notify<T>(T notification) where T : IUiBusEvent;
        void UnRegisterAll();
    }}