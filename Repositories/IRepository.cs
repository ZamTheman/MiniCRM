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

        Task Save(IWriter writer);

        Task Update(IWriter writer, Company company);

        Task DeleteCompany(IWriter writer, Company company);

        Task DeleteEntity(IWriter writer, IEntity entity, Company company);

        Task WriteDummyData(IWriter writer);
    }
}
