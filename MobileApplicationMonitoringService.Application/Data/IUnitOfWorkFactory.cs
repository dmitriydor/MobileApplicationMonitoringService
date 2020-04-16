namespace MobileApplicationMonitoringService.Application.Data
{
    public interface IUnitOfWorkFactory
    {
        UnitOfWork CreateUnitOfWork();
    }
}