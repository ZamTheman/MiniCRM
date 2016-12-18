using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;

namespace Utils
{
    public interface IReader
    {
        Task<IList<ICompany>> GetAll();
    }
}
