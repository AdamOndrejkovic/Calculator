using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CalculatorTest
    {
        private readonly ICalculator _c = new Calculator.Calculator();
        
        [TestMethod]
        [DataRow(0,1,1)]
        [DataRow(0,-1,-1)]
        [DataRow(1,-1,0)]
        [DataRow(-1,1,0)]
        [DataRow(0,int.MaxValue, int.MaxValue)]
        [DataRow(0,int.MinValue, int.MinValue)]
        public void AddValidNumber(int initialValue, int x, int expected)
        {
            _c.Add(initialValue);
            Assert.AreEqual(initialValue, _c.Result);
            
            _c.Add(x);
            Assert.AreEqual(expected,_c.Result);
        }
        
        [TestMethod]
        public void AddNumberWithOverflowExceptionToBeThrown()
        {
            _c.Add(int.MaxValue);
            int oldResult = _c.Result;

            var operationException = Assert.ThrowsException<InvalidOperationException>(() => _c.Add(1)
            );
            
            Assert.AreEqual("Overflow while adding.", operationException.Message);
            Assert.AreEqual(oldResult,_c.Result);
        }
        
        [TestMethod]
        public void AddNumberWithUnderflowExceptionToBeThrown()
        {
            _c.Add(int.MinValue);
            int oldResult = _c.Result;

            var operationException = Assert.ThrowsException<InvalidOperationException>(() => _c.Add(-1));
            
            Assert.AreEqual("Underflow while adding.", operationException.Message);
            Assert.AreEqual(oldResult,_c.Result);
        }

        [TestMethod]
        [DataRow(13456,-124)]
        [DataRow(70353,-73252)]
        [DataRow(2432,-3124)]
        [DataRow(12,-5)]
        [DataRow(42,-40)]
        public void SubtractValidNumberUnderZero(int initialValue, int subtractor)
        {
            _c.Add(initialValue);
            int expectedResult = _c.Result - (subtractor / - 1);
            
            _c.Subtract(subtractor);
            
            Assert.AreEqual(expectedResult,_c.Result);
        }
        
        [TestMethod]
        [DataRow(13456,124)]
        [DataRow(70353,73252)]
        [DataRow(2432,3124)]
        [DataRow(12,5)]
        [DataRow(42,40)]
        public void SubtractValidNumberAboveZero(int initialValue, int subtractor)
        {
            _c.Add(initialValue);
            int expectedResult = _c.Result - subtractor ;
            
            _c.Subtract(subtractor);
            
            Assert.AreEqual(expectedResult,_c.Result);
        }
        

        [TestMethod]
        [DataRow(75,2)]
        [DataRow(7,14)]
        [DataRow(24,1)]
        [DataRow(150,100)]
        [DataRow(42,9)]
        public void MultiplyValidNumber(int initialValue, int multiplier)
        {
            _c.Add(initialValue);
            int expectedValue = _c.Result * multiplier;
            
            _c.Multiply(multiplier);

            Assert.AreEqual(expectedValue, _c.Result);
        }

        [TestMethod]
        [DataRow(13456,int.MaxValue)]
        [DataRow(70353,int.MaxValue)]
        [DataRow(2432,int.MaxValue)]
        [DataRow(12,int.MaxValue)]
        [DataRow(42,int.MaxValue)]
        public void MultiplyOutsideTheRangeOverflow(int initialValue, int multiplier)
        {
            _c.Add(initialValue);

            var operationException = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _c.Multiply(multiplier);
            });

            Assert.AreEqual("Overflow occurred while multiplying.",operationException.Message);
        }
        
        [TestMethod]
        [DataRow(-13456,int.MinValue)]
        [DataRow(-70353,int.MinValue)]
        [DataRow(-2432,int.MinValue)]
        [DataRow(-12,int.MinValue)]
        [DataRow(-42,int.MinValue)]
        public void MultiplyOutsideTheRangeUnderflow(int initialValue, int multiplier)
        {
            _c.Add(initialValue);

            var operationException = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _c.Multiply(multiplier);
            });

            Assert.AreEqual("Underflow occurred while multiplying.",operationException.Message);
        }

        [TestMethod]
        [DataRow(2,2)]
        [DataRow(14,7)]
        [DataRow(63,9)]
        [DataRow(150,10)]
        [DataRow(500,5)]
        public void DivideValidNumber(int initialValue, int divider)
        {
            _c.Add(initialValue);
            int expectedValue = _c.Result / divider;
            _c.Divide(divider);
            
            Assert.AreEqual(expectedValue,_c.Result);

        }
        
        [TestMethod]
        [DataRow(2,0)]
        [DataRow(14,0)]
        [DataRow(63,0)]
        [DataRow(150,0)]
        [DataRow(500,0)]
        public void DivideByZero(int initialDivider, int divider)
        {
            _c.Add(initialDivider);
            

            var operationException = Assert.ThrowsException<InvalidOperationException>(() => _c.Divide(divider));
            
            Assert.AreEqual("Can not be divided by zero.", operationException.Message);
        }

        [TestMethod]
        [DataRow(2,2)]
        [DataRow(14,5)]
        [DataRow(63,10)]
        [DataRow(150,34)]
        [DataRow(500,125)]
        public void ModulusValidNumber(int initialValue, int divider)
        {
            _c.Add(initialValue);

            int expectedValue = _c.Result % divider;
            
            _c.Modulus(divider);
            
            Assert.AreEqual(expectedValue,_c.Result);
        }
    }
}