using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using Utils;

namespace Repositories
{
    public class Repository : IRepository
    {
        public async Task<IList<ICompany>> GetAll(IReader reader)
        {
            return await reader.GetAll();
        }

        public async Task<int> Save(IWriter writer, ICompany company)
        {
            return await writer.SaveCompany(company);
        }

        public async Task<int> SaveEntity(IWriter writer, IEntity entity, int companyId)
        {
            return await writer.SaveEntity(entity, companyId);
        }

        public async Task DeleteCompany(IWriter writer, ICompany company)
        {
            await writer.DeleteSingleCompanyByIdAsync(company.Id);
        }

        public async Task DeleteEntity(IWriter writer, IEntity entity, ICompany company)
        {
            await writer.DeleteSingleEntityByIdAsync(company.Id, entity);
        }
    }
}
