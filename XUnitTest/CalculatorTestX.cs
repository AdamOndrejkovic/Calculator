using System;
using Calculator;
using Xunit;

namespace XUnitTest
{
    public class CalculatorTestX
    {
        private readonly ICalculator _c = new Calculator.Calculator();
        
        [Theory]
        [InlineData(0,1,1)]
        [InlineData(0,-1,-1)]
        [InlineData(1,-1,0)]
        [InlineData(-1,1,0)]
        [InlineData(0,int.MaxValue, int.MaxValue)]
        [InlineData(0,int.MinValue, int.MinValue)]
        public void AddValidNumber(int initialValue, int x, int expected)
        {
            _c.Add(initialValue);
            Assert.Equal(initialValue, _c.Result);
            
            _c.Add(x);
            Assert.Equal(expected,_c.Result);
        }
        
        [Fact]
        public void AddNumberWithOverflowExceptionToBeThrown()
        {
            _c.Add(int.MaxValue);
            int oldResult = _c.Result;

            var operationException = Assert.Throws<InvalidOperationException>(() => _c.Add(1)
            );
            
            Assert.Equal("Overflow while adding.", operationException.Message);
            Assert.Equal(oldResult,_c.Result);
        }
        
        [Fact]
        public void AddNumberWithUnderflowExceptionToBeThrown()
        {
            _c.Add(int.MinValue);
            int oldResult = _c.Result;

            var operationException = Assert.Throws<InvalidOperationException>(() => _c.Add(-1));
            
            Assert.Equal("Underflow while adding.", operationException.Message);
            Assert.Equal(oldResult,_c.Result);
        }

        [Theory]
        [InlineData(13456,-124)]
        [InlineData(70353,-73252)]
        [InlineData(2432,-3124)]
        [InlineData(12,-5)]
        [InlineData(42,-40)]
        public void SubtractValidNumberUnderZero(int initialValue, int subtractor)
        {
            _c.Add(initialValue);
            int expectedResult = _c.Result - (subtractor / - 1);
            
            _c.Subtract(subtractor);
            
            Assert.Equal(expectedResult,_c.Result);
        }
        
        [Theory]
        [InlineData(13456,124)]
        [InlineData(70353,73252)]
        [InlineData(2432,3124)]
        [InlineData(12,5)]
        [InlineData(42,40)]
        public void SubtractValidNumberAboveZero(int initialValue, int subtractor)
        {
            _c.Add(initialValue);
            int expectedResult = _c.Result - subtractor ;
            
            _c.Subtract(subtractor);
            
            Assert.Equal(expectedResult,_c.Result);
        }
        

        [Theory]
        [InlineData(75,2)]
        [InlineData(7,14)]
        [InlineData(24,1)]
        [InlineData(150,100)]
        [InlineData(42,9)]
        public void MultiplyValidNumber(int initialValue, int multiplier)
        {
            _c.Add(initialValue);
            int expectedValue = _c.Result * multiplier;
            
            _c.Multiply(multiplier);

            Assert.Equal(expectedValue, _c.Result);
        }

        [Theory]
        [InlineData(13456,int.MaxValue)]
        [InlineData(70353,int.MaxValue)]
        [InlineData(2432,int.MaxValue)]
        [InlineData(12,int.MaxValue)]
        [InlineData(42,int.MaxValue)]
        public void MultiplyOutsideTheRangeOverflow(int initialValue, int multiplier)
        {
            _c.Add(initialValue);

            var operationException = Assert.Throws<InvalidOperationException>(() =>
            {
                _c.Multiply(multiplier);
            });

            Assert.Equal("Overflow occurred while multiplying.",operationException.Message);
        }
        
        [Theory]
        [InlineData(-13456,int.MinValue)]
        [InlineData(-70353,int.MinValue)]
        [InlineData(-2432,int.MinValue)]
        [InlineData(-12,int.MinValue)]
        [InlineData(-42,int.MinValue)]
        public void MultiplyOutsideTheRangeUnderflow(int initialValue, int multiplier)
        {
            _c.Add(initialValue);

            var operationException = Assert.Throws<InvalidOperationException>(() =>
            {
                _c.Multiply(multiplier);
            });

            Assert.Equal("Underflow occurred while multiplying.",operationException.Message);
        }

        [Theory]
        [InlineData(2,2)]
        [InlineData(14,7)]
        [InlineData(63,9)]
        [InlineData(150,10)]
        [InlineData(500,5)]
        public void DivideValidNumber(int initialValue, int divider)
        {
            _c.Add(initialValue);
            int expectedValue = _c.Result / divider;
            _c.Divide(divider);
            
            Assert.Equal(expectedValue,_c.Result);

        }
        
        [Theory]
        [InlineData(2,0)]
        [InlineData(14,0)]
        [InlineData(63,0)]
        [InlineData(150,0)]
        [InlineData(500,0)]
        public void DivideByZero(int initialDivider, int divider)
        {
            _c.Add(initialDivider);
            

            var operationException = Assert.Throws<InvalidOperationException>(() => _c.Divide(divider));
            
            Assert.Equal("Can not be divided by zero.", operationException.Message);
        }

        [Theory]
        [InlineData(2,2)]
        [InlineData(14,5)]
        [InlineData(63,10)]
        [InlineData(150,34)]
        [InlineData(500,125)]
        public void ModulusValidNumber(int initialValue, int divider)
        {
            _c.Add(initialValue);

            int expectedValue = _c.Result % divider;
            
            _c.Modulus(divider);
            
            Assert.Equal(expectedValue,_c.Result);
        }
    }
}