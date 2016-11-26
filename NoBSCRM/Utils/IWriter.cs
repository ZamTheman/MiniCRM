using System.Threading.Tasks;

namespace NoBSCRM.Utils
{
    public interface IWriter
    {
        Task WriteDummyData();

        Task SaveSingle();

        Task DeleteSingleById(int id);

        Task UpdateSingleById(int id);
    }
}
