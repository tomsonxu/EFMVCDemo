using EFMVCDemo.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMVCDemo.IntegrationTest
{
    [TestClass]
    public class TestGlobal
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            System.Data.Entity.Database.SetInitializer(new DbInitializer());
        }
    }
}
