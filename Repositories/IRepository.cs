using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using Utils;

namespace Repositories
{
    public interface IRepository
    {  
        Task<IList<ICompany>> GetAll(IReader reader);

        Task<ICompany> GetCompany(IReader reader);

        Task<int> Save(IWriter writer, ICompany company);

        Task<int> SaveEntity(IWriter writer, IEntity entity, int companyId);

        Task Update(IWriter writer, ICompany company);

        Task DeleteCompany(IWriter writer, ICompany company);

        Task DeleteEntity(IWriter writer, IEntity entity, ICompany company);

        Task WriteDummyData(IWriter writer);
    }
}
