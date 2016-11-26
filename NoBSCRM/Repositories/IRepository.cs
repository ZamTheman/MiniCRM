using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoBSCRM.Models;
using NoBSCRM.Utils;

namespace NoBSCRM.Repositories
{
    public interface IRepository
    {  
        Task<IList<Company>> GetAll(IReader reader);

        Task<Company> GetCompany(IReader reader);

        Task Save(IWriter writer);

        Task Update(IWriter writer, Company company);

        Task Delete(IWriter writer, Company company);

        Task WriteDummyData(IWriter writer);
    }
}
