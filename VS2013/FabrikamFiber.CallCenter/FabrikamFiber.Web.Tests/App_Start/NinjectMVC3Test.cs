namespace FabrikamFiber.Web.Tests.App_Start
{
    using System;
    using FabrikamFiber.Web.App_Start;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;
    using Ninject.Web.Mvc;
    using FabrikamFiber.Web.Data;

    /// <summary>
    /// Summary description for NinjectMVC3Test
    /// </summary>
    [TestClass]
    public class NinjectMVC3Test
    {
        [TestMethod]
        public void ItShouldHaveASingletonReferenceToCurrentInstance()
        {
            Assert.AreEqual(TestableNinjectMVC3.SingletonInstance, TestableNinjectMVC3.SingletonInstance);
        }

        [TestMethod]
        public void ItShouldCreateAKernelForTheApplication()
        {
            var bootstrapperMock = new Mock<IBootstrapper>();
            var ninject = new TestableNinjectMVC3(bootstrapperMock.Object);

            var kernel = ninject.CreateKernel();

            Assert.IsInstanceOfType(kernel, typeof(StandardKernel));
        }

        [TestMethod]
        public void ItShouldCallBoostrapperInitializeWithCreateKernelAsParameterWhenStartingInstance()
        {
            var bootstrapperMock = new Mock<IBootstrapper>();
            var ninject = new TestableNinjectMVC3(bootstrapperMock.Object);

            bootstrapperMock.Setup(b => b.Initialize(It.Is<Func<IKernel>>(f => f == ninject.CreateKernel)))
                            .Verifiable();
           
            ninject.ExecuteInnerStart();

            bootstrapperMock.VerifyAll();
        }

        [TestMethod]
        public void ItShouldCallBoostrapperShutdownWhenStoppingInstance()
        {
            var bootstrapperMock = new Mock<IBootstrapper>();
            var ninject = new TestableNinjectMVC3(bootstrapperMock.Object);

            bootstrapperMock.Setup(b => b.ShutDown()).Verifiable();

            ninject.ExecuteInnerShutDown();

            bootstrapperMock.VerifyAll();
        }


        [TestMethod]
        public void ItShouldRegisterTheProperTypeMapping()
        {    
            var bootstrapperMock = new Mock<IBootstrapper>();
            var ninject = new TestableNinjectMVC3(bootstrapperMock.Object);
            
            var kernel = new StandardKernel();
            ninject.RegisterServices(kernel);

            Assert.AreEqual(typeof(CustomerRepository), kernel.Get<ICustomerRepository>().GetType());
            Assert.AreEqual(typeof(EmployeeRepository), kernel.Get<IEmployeeRepository>().GetType());
            Assert.AreEqual(typeof(ServiceTicketRepository), kernel.Get<IServiceTicketRepository>().GetType());
            Assert.AreEqual(typeof(ServiceLogEntryRepository), kernel.Get<IServiceLogEntryRepository>().GetType());
            Assert.AreEqual(typeof(AlertRepository), kernel.Get<IAlertRepository>().GetType());
            Assert.AreEqual(typeof(MessageRepository), kernel.Get<IMessageRepository>().GetType());
            Assert.AreEqual(typeof(ScheduleItemRepository), kernel.Get<IScheduleItemRepository>().GetType());
        }


        internal class TestableNinjectMVC3 : NinjectMVC3
        {
            public TestableNinjectMVC3(IBootstrapper bootstrapper)
                : base(bootstrapper)
            {
            }

            public static NinjectMVC3 SingletonInstance
            {
                get { return NinjectMVC3.Instance; }
            }

            public void ExecuteInnerStart()
            {
                this.InnerStart();
            }

            public void ExecuteInnerShutDown()
            {
                this.InnerShutDown();
            }
        }
    }
}
