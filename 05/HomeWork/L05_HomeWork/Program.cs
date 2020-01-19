using System;
using System.Globalization;

namespace L05_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            GeometricShapes shapeType;
            Shapes shape;
            bool quit = false;
            do
            {
                shapeType = ReadingService.ReadShapeType();
                shape = null;
                switch (shapeType)
                {
                    case GeometricShapes.Unknown:
                        Console.WriteLine("Некорректный тип фигуры");
                        break;
                    case GeometricShapes.Circle:
                        shape = new Circle();
                        break;
                    case GeometricShapes.EquilateralTriangle:
                        shape = new EquilateralTriangle();
                        break;
                    case GeometricShapes.Rectangle:
                        shape = new Rectangle();
                        break;
                    default:
                        Console.WriteLine("Неизвестная ошибка при выборе типа фигуры");
                        break;
                }

                if (shape != null && shape.ReadParameters())
                {
                    shape.PrintArea();
                    shape.PrintPerimeter();
                }

                Console.WriteLine("\nЗавершить работу - Q, продолжить - Enter:");
                quit = (Console.ReadLine().Trim().ToLower() == "q");
                Console.WriteLine();
            } while (!quit);
        }
    }

    internal enum GeometricShapes
    {
        Unknown = 0,
        Circle = 1,
        EquilateralTriangle = 2,
        Rectangle = 3
    }

    internal interface Shapes
    {
        public bool ReadParameters();
        public void PrintArea();
        public void PrintPerimeter();
    }

    internal class Circle : Shapes
    {
        private double _diameter;
        private bool _successfullyRead;
        private const string _parametersNotSet = "Параметры круга не заданы";

        private void Reset()
        {
            _diameter = 0;
            _successfullyRead = false;
        }

        public Circle()
        {
            this.Reset();
        }

        bool Shapes.ReadParameters()
        {
            Console.Write("Введите диаметр: ");
            _diameter = ReadingService.ReadPositiveDouble();
            if (_diameter >= 0)
            {
                _successfullyRead = true;
            }
            else
            {
                this.Reset();
                Console.WriteLine("Диаметр круга должен быть положительным вещественным числом");
            }
            return _successfullyRead;
        }

        void Shapes.PrintArea()
        {
            if (! _successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Площащь круга: {0}",
                Math.Round((Math.PI / 4) * _diameter * _diameter, 2, MidpointRounding.AwayFromZero));
        }

        void Shapes.PrintPerimeter()
        {
            if (!_successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Длина периметра: {0}",
                Math.Round(Math.PI * _diameter, 2, MidpointRounding.AwayFromZero));
        }
    }

    internal class EquilateralTriangle : Shapes
    {
        private double _sideLength;
        private bool _successfullyRead;
        private const string _parametersNotSet = "Параметры равностороннего треугольника не заданы";

        private void Reset()
        {
            _sideLength = 0;
            _successfullyRead = false;
        }

        public EquilateralTriangle()
        {
            this.Reset();
        }

        bool Shapes.ReadParameters()
        {
            Console.Write("Введите длину стороны: ");
            _sideLength = ReadingService.ReadPositiveDouble();
            if (_sideLength >= 0)
            {
                _successfullyRead = true;
            }
            else
            {
                this.Reset();
                Console.WriteLine("Длина стороны треугольника должна быть положительным вещественным числом");
            }
            return _successfullyRead;
        }

        void Shapes.PrintArea()
        {
            if (!_successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Площащь треугольника: {0}",
                Math.Round((Math.Sqrt(3) / 4) * _sideLength * _sideLength, 2, MidpointRounding.AwayFromZero));
        }

        void Shapes.PrintPerimeter()
        {
            if (!_successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Длина периметра: {0}",
                Math.Round(_sideLength * 3, 2, MidpointRounding.AwayFromZero));
        }
    }

    internal class Rectangle : Shapes
    {
        private double _width;
        private double _hight;
        private bool _successfullyRead;
        private const string _parametersNotSet = "Параметры прямоугольника не заданы";

        private void Reset()
        {
            _width = 0;
            _hight = 0;
            _successfullyRead = false;
        }

        public Rectangle()
        {
            this.Reset();
        }

        bool Shapes.ReadParameters()
        {
            Console.Write("Введите ширину прямоугольника: ");
            _width = ReadingService.ReadPositiveDouble();
            if (_width < 0)
            {
                this.Reset();
                Console.WriteLine("Ширина прямоугольника должна быть положительным вещественным числом");
                return _successfullyRead;
            }

            Console.Write("Введите высоту прямоугольника: ");
            _hight = ReadingService.ReadPositiveDouble();
            if (_hight < 0)
            {
                this.Reset();
                Console.WriteLine("Высота прямоугольника должна быть положительным вещественным числом");
                return _successfullyRead;
            }

            _successfullyRead = true;
            return _successfullyRead;
        }

        void Shapes.PrintArea()
        {
            if (!_successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Площащь прямоугольника: {0}",
                Math.Round(_width * _hight, 2, MidpointRounding.AwayFromZero));
        }

        void Shapes.PrintPerimeter()
        {
            if (!_successfullyRead)
            {
                Console.WriteLine(_parametersNotSet);
                return;
            }

            Console.WriteLine("Длина периметра: {0}",
                Math.Round(_width*2 + _hight*2, 2, MidpointRounding.AwayFromZero));
        }
    }

    internal static class ReadingService
    {
        public static GeometricShapes ReadShapeType()
        {
            Console.WriteLine("Введите тип фигуры:");
            Console.WriteLine("1 - круг");
            Console.WriteLine("2 - равносторонний треугольник");
            Console.WriteLine("3 - прямоугольник");

            int input = ReadPositiveInteger();
            if (input < 1 || input > 3)
            {
                return GeometricShapes.Unknown;
            }

            return (GeometricShapes) input;
        }

        // первый вариант обработки нескольких исключений
        // минусы: необходимость использовать флаги, битовый флаг уменьшит объем кода
        // но ухудшит понимаемость и читаемость, какой то флаг можно забыть,
        // дублирование общего кода, которое нельзя избежать, переменная e существует 
        // только внутри каждого catch и не существует в finally
        // переменная e в блоке catch (Exception e) не используется
        public static double ReadPositiveDouble()
        {
            double input = -1;
            bool knownException = false;
            bool exceptionRaised = false;
            string exceptionString = "";
            try
            {
                input = double.Parse(Console.ReadLine().Replace(',', '.').Trim(),
                    NumberStyles.Float, CultureInfo.InvariantCulture);
                input = (input >= 0) ? input : -1;
            }
            catch (ArgumentNullException e)
            {
                knownException = true;
                exceptionRaised = true;
                exceptionString = $"Type: {e.GetType()}, message: {e.Message}";
            }
            catch (ArgumentException e)
            {
                knownException = true;
                exceptionRaised = true;
                exceptionString = $"Type: {e.GetType()}, message: {e.Message}";
            }

            catch (FormatException e)
            {
                knownException = true;
                exceptionRaised = true;
                exceptionString = $"Type: {e.GetType()}, message: {e.Message}";
            }
            catch (OverflowException e)
            {
                knownException = true;
                exceptionRaised = true;
                exceptionString = $"Type: {e.GetType()}, message: {e.Message}";
            }
            catch (Exception e)
            {
                knownException = false;
                exceptionRaised = true;
                throw;
            }
            finally
            {
                if (exceptionRaised)
                {
                    input = -1;
                }

                if (knownException)
                {
                    Console.WriteLine("Exception " + exceptionString);
                }
            }

            return input;
        }

        // второй вариант обработки нескольких исключений (пишут что это антипаттерн)
        // минусы - можно забыть throw;
        // плюсы - всегда, без всяких флагов, если возникло исключение выполняется общий обязательный код
        public static int ReadPositiveInteger()
        {
            int input = -1;
            try
            {
                input = int.Parse(Console.ReadLine(), NumberStyles.Integer);
                input = (input >= 0) ? input : -1;
            }
            catch (Exception e)
            {
                input = -1;
                if (e is ArgumentNullException ||
                    e is ArgumentException ||
                    e is FormatException ||
                    e is OverflowException)
                {
                    Console.WriteLine("Exception Type: {0}, message: {1}", e.GetType(), e.Message);
                }
                else
                {
                    throw;
                }
            }

            return input;
        }
    }
}
