using System;
using System.Collections.Generic;
using System.Text;

namespace L03_V01_HomeWork
{
    class PythagoreanTable
    {
        private readonly int numberFrom;
        private readonly int numberTo;
        private readonly BaseMultiplicationTable multiplicationTable;
        
        public int NumberFrom => numberFrom;
        
        public int NumberTo => numberTo;

        internal BaseMultiplicationTable MultiplicationTable => multiplicationTable;

        public PythagoreanTable(int numberFrom, int numberTo)
        {
            if (numberFrom < 1)
            {
                throw new InvalidOperationException($"Parameter {nameof(numberFrom)} is not a natural number");
            }
            else if (numberTo < 1)
            {
                throw new InvalidOperationException($"Parameter {nameof(numberTo)} is not a natural number");
            }
            else if (numberTo < numberFrom)
            {
                throw new InvalidOperationException($"Parameter {nameof(numberTo)} is smaller than {nameof(numberFrom)}");
            }

            this.numberFrom = numberFrom;
            this.numberTo = numberTo;

            int dimensionLength = numberTo - numberFrom + 1;
            int[] multiplier = new int[dimensionLength];
            for (int i = 0; i < dimensionLength; i++)
            {
                multiplier[i] = numberFrom + i;
            }

            multiplicationTable = new BaseMultiplicationTable(multiplier, multiplier);
        }

        public PythagoreanTable() : this(1, 10) { }

        public string ToString(int lengthCell)
        {
            return multiplicationTable.ToString(lengthCell);
        }

        public override string ToString()
        {
            int lengthCell = (numberTo * numberTo).ToString().Length + 1;
            return this.ToString(lengthCell);
        }
    }
}
