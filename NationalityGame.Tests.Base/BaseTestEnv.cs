using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace NationalityGame.Tests.Base
{
    public abstract class BaseTestEnv<TSut>
    {
        protected readonly IFixture Fixture;

        private bool _isBuilt;

        protected BaseTestEnv()
        {
            Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        public TSut Setup()
        {
            SetupDependencies();

            _isBuilt = true;

            return Fixture.Create<TSut>();
        }

        protected abstract void SetupDependencies();

        protected Mock<T> AddDependency<T>() where T : class
        {
            return Fixture.Freeze<Mock<T>>();
        }

        protected void AddConcreteDependency<T>(T instance)
        {
            Fixture.Register(() => instance);
        }

        public Mock<T> Dependency<T>() where T : class
        {
            CheckIsBuilt();

            return Fixture.Create<Mock<T>>();
        }

        private void CheckIsBuilt()
        {
            if (!_isBuilt)
            {
                throw new Exception("Test environment must be built by calling Build() method first");
            }
        }
    }
}