using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoBSCRM.Annotations;
using NoBSCRM.Models;
using System.Collections.ObjectModel;
using NoBSCRM.Utils;

namespace NoBSCRM.Models
{
    [CompilerGenerated]
    public class StubICompany : ICompany
    {
        private readonly StubContainer<StubICompany> _stubs = new StubContainer<StubICompany>();

        int global::NoBSCRM.Models.ICompany.Id
        {
            get
            {
                return _stubs.GetMethodStub<Id_Get_Delegate>("get_Id").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<Id_Set_Delegate>("set_Id").Invoke(value);
            }
        }

        public delegate int Id_Get_Delegate();

        public StubICompany Id_Get(Id_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void Id_Set_Delegate(int value);

        public StubICompany Id_Set(Id_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.Models
{
    [CompilerGenerated]
    public class StubIEntity : IEntity
    {
        private readonly StubContainer<StubIEntity> _stubs = new StubContainer<StubIEntity>();

        int global::NoBSCRM.Models.IEntity.Id
        {
            get
            {
                return _stubs.GetMethodStub<Id_Get_Delegate>("get_Id").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<Id_Set_Delegate>("set_Id").Invoke(value);
            }
        }

        public delegate int Id_Get_Delegate();

        public StubIEntity Id_Get(Id_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void Id_Set_Delegate(int value);

        public StubIEntity Id_Set(Id_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubICompanyListViewModel : ICompanyListViewModel
    {
        private readonly StubContainer<StubICompanyListViewModel> _stubs = new StubContainer<StubICompanyListViewModel>();
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubICompanyViewModel : ICompanyViewModel
    {
        private readonly StubContainer<StubICompanyViewModel> _stubs = new StubContainer<StubICompanyViewModel>();
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubIEntityViewModel : IEntityViewModel
    {
        private readonly StubContainer<StubIEntityViewModel> _stubs = new StubContainer<StubIEntityViewModel>();

        global::NoBSCRM.Models.IEntity global::NoBSCRM.ViewModels.IEntityViewModel.ActiveEntity
        {
            get
            {
                return _stubs.GetMethodStub<ActiveEntity_Get_Delegate>("get_ActiveEntity").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<ActiveEntity_Set_Delegate>("set_ActiveEntity").Invoke(value);
            }
        }

        public delegate global::NoBSCRM.Models.IEntity ActiveEntity_Get_Delegate();

        public StubIEntityViewModel ActiveEntity_Get(ActiveEntity_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void ActiveEntity_Set_Delegate(global::NoBSCRM.Models.IEntity value);

        public StubIEntityViewModel ActiveEntity_Set(ActiveEntity_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubIEntityViewModelFactory : IEntityViewModelFactory
    {
        private readonly StubContainer<StubIEntityViewModelFactory> _stubs = new StubContainer<StubIEntityViewModelFactory>();

        global::NoBSCRM.ViewModels.IEntityViewModel global::NoBSCRM.ViewModels.IEntityViewModelFactory.GetEntityViewModel(string type, global::NoBSCRM.Models.IEntity entity)
        {
            return _stubs.GetMethodStub<GetEntityViewModel_String_IEntity_Delegate>("GetEntityViewModel").Invoke(type, entity);
        }

        public delegate global::NoBSCRM.ViewModels.IEntityViewModel GetEntityViewModel_String_IEntity_Delegate(string type, global::NoBSCRM.Models.IEntity entity);

        public StubIEntityViewModelFactory GetEntityViewModel(GetEntityViewModel_String_IEntity_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubIStartPageViewModel : IStartPageViewModel
    {
        private readonly StubContainer<StubIStartPageViewModel> _stubs = new StubContainer<StubIStartPageViewModel>();
    }
}

namespace NoBSCRM.ViewModels
{
    [CompilerGenerated]
    public class StubIViewModel : IViewModel
    {
        private readonly StubContainer<StubIViewModel> _stubs = new StubContainer<StubIViewModel>();
    }
}

namespace NoBSCRM.Repositories
{
    [CompilerGenerated]
    public class StubIRepository : IRepository
    {
        private readonly StubContainer<StubIRepository> _stubs = new StubContainer<StubIRepository>();

        global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::NoBSCRM.Models.Company>> global::NoBSCRM.Repositories.IRepository.GetAll(global::NoBSCRM.Utils.IReader reader)
        {
            return _stubs.GetMethodStub<GetAll_IReader_Delegate>("GetAll").Invoke(reader);
        }

        public delegate global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::NoBSCRM.Models.Company>> GetAll_IReader_Delegate(global::NoBSCRM.Utils.IReader reader);

        public StubIRepository GetAll(GetAll_IReader_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::NoBSCRM.Models.Company> global::NoBSCRM.Repositories.IRepository.GetCompany(global::NoBSCRM.Utils.IReader reader)
        {
            return _stubs.GetMethodStub<GetCompany_IReader_Delegate>("GetCompany").Invoke(reader);
        }

        public delegate global::System.Threading.Tasks.Task<global::NoBSCRM.Models.Company> GetCompany_IReader_Delegate(global::NoBSCRM.Utils.IReader reader);

        public StubIRepository GetCompany(GetCompany_IReader_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Repositories.IRepository.Save(global::NoBSCRM.Utils.IWriter writer)
        {
            return _stubs.GetMethodStub<Save_IWriter_Delegate>("Save").Invoke(writer);
        }

        public delegate global::System.Threading.Tasks.Task Save_IWriter_Delegate(global::NoBSCRM.Utils.IWriter writer);

        public StubIRepository Save(Save_IWriter_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Repositories.IRepository.Update(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.Company company)
        {
            return _stubs.GetMethodStub<Update_IWriter_Company_Delegate>("Update").Invoke(writer, company);
        }

        public delegate global::System.Threading.Tasks.Task Update_IWriter_Company_Delegate(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.Company company);

        public StubIRepository Update(Update_IWriter_Company_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Repositories.IRepository.DeleteCompany(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.Company company)
        {
            return _stubs.GetMethodStub<DeleteCompany_IWriter_Company_Delegate>("DeleteCompany").Invoke(writer, company);
        }

        public delegate global::System.Threading.Tasks.Task DeleteCompany_IWriter_Company_Delegate(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.Company company);

        public StubIRepository DeleteCompany(DeleteCompany_IWriter_Company_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Repositories.IRepository.DeleteEntity(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.IEntity entity, global::NoBSCRM.Models.Company company)
        {
            return _stubs.GetMethodStub<DeleteEntity_IWriter_IEntity_Company_Delegate>("DeleteEntity").Invoke(writer, entity, company);
        }

        public delegate global::System.Threading.Tasks.Task DeleteEntity_IWriter_IEntity_Company_Delegate(global::NoBSCRM.Utils.IWriter writer, global::NoBSCRM.Models.IEntity entity, global::NoBSCRM.Models.Company company);

        public StubIRepository DeleteEntity(DeleteEntity_IWriter_IEntity_Company_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Repositories.IRepository.WriteDummyData(global::NoBSCRM.Utils.IWriter writer)
        {
            return _stubs.GetMethodStub<WriteDummyData_IWriter_Delegate>("WriteDummyData").Invoke(writer);
        }

        public delegate global::System.Threading.Tasks.Task WriteDummyData_IWriter_Delegate(global::NoBSCRM.Utils.IWriter writer);

        public StubIRepository WriteDummyData(WriteDummyData_IWriter_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.Utils
{
    [CompilerGenerated]
    public class StubIReader : IReader
    {
        private readonly StubContainer<StubIReader> _stubs = new StubContainer<StubIReader>();

        global::System.Threading.Tasks.Task<global::NoBSCRM.Models.Company> global::NoBSCRM.Utils.IReader.GetSingleById(int id)
        {
            return _stubs.GetMethodStub<GetSingleById_Int32_Delegate>("GetSingleById").Invoke(id);
        }

        public delegate global::System.Threading.Tasks.Task<global::NoBSCRM.Models.Company> GetSingleById_Int32_Delegate(int id);

        public StubIReader GetSingleById(GetSingleById_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::NoBSCRM.Models.Company>> global::NoBSCRM.Utils.IReader.GetAll()
        {
            return _stubs.GetMethodStub<GetAll_Delegate>("GetAll").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::NoBSCRM.Models.Company>> GetAll_Delegate();

        public StubIReader GetAll(GetAll_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace NoBSCRM.Utils
{
    [CompilerGenerated]
    public class StubIWriter : IWriter
    {
        private readonly StubContainer<StubIWriter> _stubs = new StubContainer<StubIWriter>();

        global::System.Threading.Tasks.Task global::NoBSCRM.Utils.IWriter.WriteDummyData()
        {
            return _stubs.GetMethodStub<WriteDummyData_Delegate>("WriteDummyData").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task WriteDummyData_Delegate();

        public StubIWriter WriteDummyData(WriteDummyData_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Utils.IWriter.SaveSingle()
        {
            return _stubs.GetMethodStub<SaveSingle_Delegate>("SaveSingle").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task SaveSingle_Delegate();

        public StubIWriter SaveSingle(SaveSingle_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Utils.IWriter.DeleteSingleCompanyByIdAsync(int id)
        {
            return _stubs.GetMethodStub<DeleteSingleCompanyByIdAsync_Int32_Delegate>("DeleteSingleCompanyByIdAsync").Invoke(id);
        }

        public delegate global::System.Threading.Tasks.Task DeleteSingleCompanyByIdAsync_Int32_Delegate(int id);

        public StubIWriter DeleteSingleCompanyByIdAsync(DeleteSingleCompanyByIdAsync_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Utils.IWriter.DeleteSingleEntityByIdAsync(int id, global::NoBSCRM.Models.IEntity entity)
        {
            return _stubs.GetMethodStub<DeleteSingleEntityByIdAsync_Int32_IEntity_Delegate>("DeleteSingleEntityByIdAsync").Invoke(id, entity);
        }

        public delegate global::System.Threading.Tasks.Task DeleteSingleEntityByIdAsync_Int32_IEntity_Delegate(int id, global::NoBSCRM.Models.IEntity entity);

        public StubIWriter DeleteSingleEntityByIdAsync(DeleteSingleEntityByIdAsync_Int32_IEntity_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task global::NoBSCRM.Utils.IWriter.UpdateSingleById(int id)
        {
            return _stubs.GetMethodStub<UpdateSingleById_Int32_Delegate>("UpdateSingleById").Invoke(id);
        }

        public delegate global::System.Threading.Tasks.Task UpdateSingleById_Int32_Delegate(int id);

        public StubIWriter UpdateSingleById(UpdateSingleById_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}