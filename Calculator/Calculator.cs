using System;

namespace Calculator
{
    public class Calculator: ICalculator
    {
        public int Result { get; private set; }

        public Calculator()
        {
            Result = 0;
        }
        
        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Add(int x)
        {
            if (x >= 0 && (Result + x) < Result)
            {
                throw new InvalidOperationException("Overflow while adding.");
            }
            if (x < 0 && (Result + x) > Result)
            {
                throw new InvalidOperationException("Underflow while adding.");
            }
            Result += x;
        }

        public void Subtract(int x)
        {
            if (x >= 0)
            {
                Result -= x;
            }

            if (x < 0)
            {
                Result -= (x / -1);
            }
        }

        public void Multiply(int x)
        {
            int a = Result;

            if (a > int.MaxValue / x)
            {
                throw new InvalidOperationException("Overflow occurred while multiplying.");
            } /* `a * x` would overflow */;
            if ((a < int.MinValue / x))
            {
                throw new InvalidOperationException("Underflow occurred while multiplying.");
            }/* `a * x` would underflow */;
            Result *= x;
        }

        public void Divide(int x)
        {
            if (x == 0)
            {
                throw new InvalidOperationException("Can not be divided by zero.");
            }
            Result /= x;
        }

        public void Modulus(int x)
        {
            Result %= x;
        }
    }
}