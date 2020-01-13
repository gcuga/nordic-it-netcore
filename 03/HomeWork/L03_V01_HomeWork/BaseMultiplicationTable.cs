using System;
using System.Text;

namespace L03_V01_HomeWork
{
    class BaseMultiplicationTable
    {
        private readonly int[] rowMultiplier;
        private readonly int[] columnMultiplier;
        private readonly int[,] table;
        private readonly int maxAbsResultMultiplication;

        public int[] RowMultiplier => (int[])rowMultiplier.Clone();
        public int[] ColumnMultiplier => (int[])columnMultiplier.Clone();
        public int[,] Table => (int[,])table.Clone();

        public BaseMultiplicationTable(int[] rowMultiplier, int[] columnMultiplier)
        {
            this.rowMultiplier = (int[])(rowMultiplier ?? throw new ArgumentNullException(nameof(rowMultiplier))).Clone();
            this.columnMultiplier = (int[])(columnMultiplier ?? throw new ArgumentNullException(nameof(columnMultiplier))).Clone();
            if (this.rowMultiplier.Length < 1 || this.columnMultiplier.Length < 1)
            {
                throw new InvalidOperationException("The multiplier array contains no elements");
            }

            checked
            {
                maxAbsResultMultiplication = MaxAbsValue(this.rowMultiplier) * MaxAbsValue(this.columnMultiplier);
            }

            table = new int[this.rowMultiplier.Length, this.columnMultiplier.Length];
            for (int i = 0; i < this.rowMultiplier.Length; i++)
            {
                for (int j = 0; j < this.columnMultiplier.Length; j++)
                {
                    table[i, j] = this.rowMultiplier[i] * this.columnMultiplier[j];
                }
            }
        }

        public string ToString(int lengthCell)
        {
            StringBuilder sb = new StringBuilder("".PadLeft(lengthCell));
            // строка множителей по столбцам
            for (int j = 0; j < this.columnMultiplier.Length; j++)
            {
                sb.Append(this.columnMultiplier[j].ToString().PadLeft(lengthCell));
            }

            for (int i = 0; i < this.rowMultiplier.Length; i++)
            {
                sb.AppendFormat("\n{0}", this.rowMultiplier[i].ToString().PadLeft(lengthCell));
                for (int j = 0; j < this.columnMultiplier.Length; j++)
                {
                    sb.Append(table[i, j].ToString().PadLeft(lengthCell));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.ToString(maxAbsResultMultiplication.ToString().Length + 2);
        }

        private static int MaxAbsValue(int[] array)
        {
            int maxValue = int.MinValue;
            int arrayElementValue;
            for (int i = 0; i < array.Length; i++)
            {
                arrayElementValue = Math.Abs(array[i]);
                if (arrayElementValue > maxValue)
                {
                    maxValue = arrayElementValue;
                }
            }

            return maxValue;
        }
    }
}
