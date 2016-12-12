using NUnit.Framework;
using Moq;

namespace ViewModelsTestProject
{
    [TestFixture]
    public abstract class TestBase<T>
    {
        protected MockRepository MockRepository { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            CreateMockRepository();
            CreteMocks();
            SetupConstructorRequiredMocks();
            SetupMocksAfterConstructions();
        }

        protected virtual void SetupMocksAfterConstructions()
        {
        }

        protected virtual void SetupConstructorRequiredMocks()
        {
        }

        protected virtual void CreteMocks()
        {
        }

        private void CreateMockRepository()
        {
           MockRepository = new MockRepository(MockBehavior.Strict);
        }

        protected Mock<TMock> CreateMock<TMock>() where TMock : class

        {
            return MockRepository.Create<TMock>();
        }

        protected virtual void CreateMocks()
        {
            
        }
        
        

    }
}
