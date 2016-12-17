using System;
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

        public Task<ICompany> GetCompany(IReader reader)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Save(IWriter writer, ICompany company)
        {
            return await writer.SaveCompany(company);
        }

        public async Task<int> SaveEntity(IWriter writer, IEntity entity, int companyId)
        {
            return await writer.SaveEntity(entity, companyId);
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

        public Task Update(IWriter writer, ICompany company)
        {
            throw new NotImplementedException();
        }
    }
}
