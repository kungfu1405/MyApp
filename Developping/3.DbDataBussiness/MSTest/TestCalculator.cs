using DbData.Bll;
using DbData.Entitties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    [TestClass]
    public class TestCalculator
    {
        private readonly Calculator _calculator;
        private readonly CityBll _cityBll;
        public TestCalculator()
        {
            _calculator = new Calculator();
            _cityBll = new CityBll();
        }

        [TestMethod]
        public void Add()
        {
            //try
            //{
                int result = _calculator.Add(3, 5);
                Assert.AreEqual(10, result);
            //}
            //catch(Exception ex)
            //{
            //    var error = ex.Message;
            //}            
        }
        [TestMethod]
        public void getCity()
        {
            var city = _cityBll.Get(1);
            Assert.AreEqual("hanoi", city.Result.Name);
        }

    }
}
