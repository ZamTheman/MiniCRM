using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using Utils;

namespace Repositories
{
    public class Repository : IRepository
    {
        public async Task<IList<Company>> GetAll(IReader reader)
        {
            return await reader.GetAll();
        }

        public Task<Company> GetCompany(IReader reader)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Save(IWriter writer, ICompany company)
        {
            return await writer.SaveCompany(company);
        }

        public Task Update(IWriter writer, Company company)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteCompany(IWriter writer, ICompany company)
        {
            await writer.DeleteSingleCompanyByIdAsync(company.Id);
        }

        public async Task DeleteEntity(IWriter writer, IEntity entity, ICompany company)
        {
            await writer.DeleteSingleEntityByIdAsync(company.Id, entity);
        }

        public async Task WriteDummyData(IWriter writer)
        {
            await writer.WriteDummyData();
        }
    }
}
