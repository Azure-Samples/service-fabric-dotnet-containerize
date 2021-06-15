namespace FabrikamFiber.Extranet.Web.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using FabrikamFiber.Extranet.Web.Helpers;
    using Xunit;

    [TestFixture]
    public class GuardTest
    {
        [Xunit.Fact]
        public void ItShouldThrowExceptionIfArgumentIsNull()
        {
            try
            {
                Guard.ThrowIfNull(null, "value");
            }
            catch (ArgumentNullException)
            { }
        }

        [Test]
        public void ItShouldNotThrowExceptionIfArgumentIsNotNull()
        {
            Guard.ThrowIfNull("this is not null", "value");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItShouldThrowExceptionIfArgumentIsNullOrEmpty()
        {
            Guard.ThrowIfNullOrEmpty(string.Empty, "value");
        }

        [Test]
        public void ItShouldNotThrowExceptionIfArgumentIsNotNullOrEmpty()
        {
            Guard.ThrowIfNullOrEmpty("not null or empty", "value");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItShouldThrowExceptionIfArgumentIsNotInRange()
        {
            Guard.ThrowIfNotInRange(1, 2, 3, "value");
        }

        [Test]
        public void ItShouldNotThrowExceptionIfArgumentIsNotLesserThanTheMin()
        {
            Guard.ThrowIfNotInRange(2, 1, 3, "value");
        }
    }
}
