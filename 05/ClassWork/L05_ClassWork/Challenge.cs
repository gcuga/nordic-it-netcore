using System;
using System.Collections.Generic;
using System.Text;

namespace L05_ClassWork
{
    class Challenge
    {
        public static void C1()
        {
            Console.Write("Длительность договора аренды:");
            int leaseTerm = int.Parse(Console.ReadLine());

            if (leaseTerm < 1 || leaseTerm > 30)
            {
                Console.WriteLine("Вы ввели неверное значение");
                System.Environment.Exit(-1);
            }

            string ending;
            if (leaseTerm % 100 >= 10 && leaseTerm % 100 <= 20)
            {
                ending = "лет";
            }
            else if (leaseTerm % 10 == 1)
            {
                ending = "год";
            }
            else if (leaseTerm % 10 < 5)
            {
                ending = "года";
            }
            else
            {
                ending = "лет";
            }

            Console.WriteLine("Срок действия договора аренды: {0} {1}", leaseTerm, ending);
        }

        public static void C2()
        {
            Console.Write("Длительность договора аренды:");
            int leaseTerm = int.Parse(Console.ReadLine());
            string ending;

            if (leaseTerm < 1 || leaseTerm > 30)
            {
                Console.WriteLine("Вы ввели неверное значение");
                System.Environment.Exit(-1);
            }

            switch (leaseTerm)
            {
                case 1:
                case 21:
                    ending = "год";
                    break;
                case 2:
                case 3:
                case 4:
                case 22:
                case 23:
                case 24:
                    ending = "года";
                    break;
                default:
                    ending = "лет";
                    break;
            }

            Console.WriteLine("Срок действия договора аренды: {0} {1}", leaseTerm, ending);
        }

        public static void C3()
        {
            int ?a = null;
            int ?b = null;
            int ?c = null;
            try
            {
                a = int.Parse(Console.ReadLine());
                b = int.Parse(Console.ReadLine());
                c = a / b;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Неправильный формат");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Попытка деления на ноль");
            }
            catch (Exception e)
            {
                Console.WriteLine("Неизвестная ошибка");
                throw;
            }
            finally
            {
                // выполняется всегда
                Console.WriteLine("Закрыть все что необходимо завершить/закрыть");
            }
            Console.WriteLine("a = {0}, b = {1}, c = {2}", a??-1, b??-1, c??-1);
        }
    }
}
