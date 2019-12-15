using System.Threading.Tasks;
using app.web.Infrastructure.Contracts;

namespace app.web.Data.Contracts
{
    public interface IDefaultDbContextInitializer : ITransientService
    {
        bool EnsureCreated();
        void Migrate();
        Task Seed();
    }
}