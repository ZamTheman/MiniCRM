using System.Threading.Tasks;
using ModelLayer;

namespace Utils
{
    public interface IWriter
    {
        Task<int> SaveEntity(IEntity entity, int companyId);

        Task DeleteSingleCompanyByIdAsync(int id);

        Task DeleteSingleEntityByIdAsync(int id, IEntity entity);

        Task<int> SaveCompany(ICompany company);
    }
}
