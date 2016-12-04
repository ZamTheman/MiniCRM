using System.Threading.Tasks;
using NoBSCRM.Models;

namespace NoBSCRM.Utils
{
    public interface IWriter
    {
        Task WriteDummyData();

        Task SaveSingle();

        Task DeleteSingleCompanyByIdAsync(int id);

        Task DeleteSingleEntityByIdAsync(int id, IEntity entity);

        Task UpdateSingleById(int id);
    }
}
