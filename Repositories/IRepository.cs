using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using Utils;

namespace Repositories
{
    public interface IRepository
    {  
        Task<IList<Company>> GetAll(IReader reader);

        Task<Company> GetCompany(IReader reader);

        Task<int> Save(IWriter writer, ICompany company);

        Task Update(IWriter writer, Company company);

        Task DeleteCompany(IWriter writer, ICompany company);

        Task DeleteEntity(IWriter writer, IEntity entity, ICompany company);

        Task WriteDummyData(IWriter writer);
    }
}
