using System.Threading.Tasks;
using ModelLayer;

namespace Utils
{
    public interface IWriter
    {
        Task WriteDummyData();

        Task SaveSingle();

        Task DeleteSingleCompanyByIdAsync(int id);

        Task DeleteSingleEntityByIdAsync(int id, IEntity entity);

        Task UpdateSingleById(int id);

        Task SaveCompany(ICompany company);
    }
}
