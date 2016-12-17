using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;

namespace Utils
{
    public interface IReader
    {
        Task<ICompany> GetSingleById(int id);

        Task<IList<ICompany>> GetAll();
    }
}
