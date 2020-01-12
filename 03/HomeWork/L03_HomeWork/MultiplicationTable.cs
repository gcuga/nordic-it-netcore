using System;
using System.Collections.Generic;
using System.Text;

// вопрос 1: как сделать содержимое массива readonly?
// вопрос 2: при использовании public static T[,] TableCopy<T>(T[,] fromTable)
//           не произойдет ли boxing содержимого массива в Integer и как следствие вместо примитивов
//           не окажутся ли ссылки по которым можно изменить содержимое внутреннего массива извне?
// вопрос 3: как отрефракторить сохранив оптимизированный расчет для непрерывной квадратной таблицы?
// вопрос 4: что нужно чтобы класс был по настоящему иммутабельным

namespace L03_HomeWork
{
    /// <summary>
    /// Иммутабельный класс таблицы умножения (Пифагора)
    /// Пример внутреннего массива для чисел с 5 до 7
    /// 7-5 = 2 -> + 2 = 4 - длина измерения массива индексы 0-3
    ///      [,0] [,1] [,2] [,3]
    /// [0,]  0    5    6    7     - множители
    /// [1,]  5    25   30   35
    /// [2,]  6    30   36   42
    /// [3,]  7    35   42   49
    /// </summary>
    class MultiplicationTable
    {
        private readonly RangeFormat rangeFormat;
        private readonly int numberFrom;
        private readonly int numberTo;
        private readonly int[,] table;
        private int maxMultiplication;

        public int NumberFrom { get => numberFrom; }
        public int NumberTo { get => numberTo; }
        public int[,] Table { get => TableCopy(table); }

        /// <summary>
        /// Конструктор создает и заполняет внутренний массив, дальнейшие модификации массива не допускаются
        /// </summary>
        /// <param name="rangeFormat">Параметр определяет каким образом интерпретировать number2,
        /// тип параметра enum RangeFormat, если Range то number2 - верхняя граница диапазона, 
        /// если Quantity то number2 - количество множителей.</param>
        /// <param name="number1">Начало диапазона множителей, включительно</param>
        /// <param name="number2">В зависимости от rangeFormat либо число множителей, либо верхняя граница диапазона</param>
        /// 
        public MultiplicationTable(RangeFormat rangeFormat, int number1, int number2)
        {
            this.rangeFormat = rangeFormat;
            this.numberFrom = number1;
            if (rangeFormat == RangeFormat.Quantity)
            {
                // пример: начало диапазона - 5, количество множителей - 3
                // верхняя граница диапазона 5+3-1 = 7, множители 5-6-7
                this.numberTo = this.numberFrom + number2 - 1;
            } else
            {
                this.numberTo = number2;
            }

            int dimensionLength = this.numberTo - this.numberFrom + 2;
            table = new int[dimensionLength, dimensionLength];

            // заполняем нулевой столбец и строку множителями
            for (int i = 1; i < dimensionLength; i++)
            {
                table[i, 0] = table[0, i] = this.numberFrom + i - 1;
            }

            // заполняем таблицу умножения, i - по колонкам, j - по строкам
            for (int i = 1; i < dimensionLength; i++)
            {
                table[i, i] = table[0, i] * table[i, 0];
                for (int j = i+1; j < dimensionLength; j++)
                {
                    table[j, i] = table[i, j] = table[j - 1, i] + table[0, i];
                }
            }
            maxMultiplication = table[dimensionLength - 1, dimensionLength - 1];
        }

        /// <summary>
        /// Конструктор без параметров создает классическую таблицу умножения 10х10
        /// </summary>
        public MultiplicationTable() : this(rangeFormat : RangeFormat.Range, number1 : 1, number2 : 10)
        {
        }

        /// <summary>
        /// Альтернативный вариант создания таблицы умножения с использованием одномерных массивов множителей
        /// таблица в данном случае может быть не квадратная
        /// </summary>
        /// <param name="rowMultiplier">Множители для строк таблицы умножения</param>
        /// <param name="columnMultiplier">Множители для колонок таблицы умножения</param>
        public MultiplicationTable(int[] rowMultiplier, int[] columnMultiplier)
        {
            this.rangeFormat = RangeFormat.Arrays;
            // диапазон неприменим для этого варианта
            this.numberFrom = -1;
            this.numberTo = -1;
            table = new int[rowMultiplier.Length + 1, columnMultiplier.Length + 1];

            for (int i = 0; i < rowMultiplier.Length; i++)
            {
                table[i + 1, 0] = rowMultiplier[i];
            }

            for (int i = 0; i < columnMultiplier.Length; i++)
            {
                table[0, i + 1] = columnMultiplier[i];
            }

            maxMultiplication = int.MinValue;
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    table[i, j] = table[i, 0] * table[0, j];
                    if (table[i, j] > maxMultiplication)
                    {
                        maxMultiplication = table[i, j];
                    }
                }
            }
        }

        public static T[,] TableCopy<T>(T[,] fromTable)
        {
            T[,] toTable = new T[fromTable.GetLength(0), fromTable.GetLength(1)];
            for (int i = 0; i < fromTable.GetLength(0); i++)
            {
                for (int j = 0; j < fromTable.GetLength(1); j++)
                {
                    toTable[i, j] = fromTable[i, j];
                }
            }
            return toTable;
        }

        public void PrintTable()
        {
            int maxLength = maxMultiplication.ToString().Length;

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i,j].ToString().PadLeft(maxLength + 1));
                }
                Console.WriteLine("");
            }
        }


    }
}
