using System.Threading.Tasks;

namespace app.web.Infrastructure.Contracts
{
    public interface ILogisticsCenterService : ITransientService
    {
        Task TryCreateLogisticsCenter();
    }
}