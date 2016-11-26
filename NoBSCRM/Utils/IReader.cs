using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoBSCRM.Models;

namespace NoBSCRM.Utils
{
    public interface IReader
    {
        Task<Company> GetSingleById(int id);

        Task<IList<Company>> GetAll();
    }
}
