using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoBSCRM.Models;
using NoBSCRM.Utils;

namespace NoBSCRM.Repositories
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

        public Task Save(IWriter writer)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(IWriter writer, Company company)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(IWriter writer, Company company)
        {
            throw new System.NotImplementedException();
        }

        public async Task WriteDummyData(IWriter writer)
        {
            await writer.WriteDummyData();
        }
    }
}
