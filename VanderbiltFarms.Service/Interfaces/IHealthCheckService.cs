namespace VanderbiltFarms.Service.Interfaces
{
    public interface IHealthCheckService
    {
        Task<bool> HealthCheck();
    }
}