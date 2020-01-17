using System;
using System.Globalization;

namespace L04_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            FillContainer[] fillContainers =
                { new FillContainer(new Container(ContainerTypes.Large,  20), 0),
                  new FillContainer(new Container(ContainerTypes.Medium,  5), 0),
                  new FillContainer(new Container(ContainerTypes.Small,   1), 0)};

            int juiceVolumeCeiling;
            int filledContainers;
            bool quit = false;

            do
            {
                juiceVolumeCeiling = ReadVolumeCeiling();
                if (juiceVolumeCeiling > 0)
                {
                    filledContainers = CalculateContainerNeeded(juiceVolumeCeiling, fillContainers);
                    PrintContainerNeeded(filledContainers, fillContainers);
                }
                else
                {
                    Console.WriteLine("Нет сока для упаковки.");
                }

                Console.WriteLine("\nЗавершить работу - Q, продолжить - Enter:");
                quit = (Console.ReadLine().Trim().ToLower() == "q");
            } while (!quit);
        }

        public static int CalculateContainerNeeded(int volume, FillContainer[] fillContainers)
        {
            int filledContainers = 0;
            for (int i = 0; i < fillContainers.Length; i++)
            {
                fillContainers[i].Quantitiy = volume / fillContainers[i].Container.Capacity;
                volume %= fillContainers[i].Container.Capacity;
                if (fillContainers[i].Quantitiy > 0)
                {
                    filledContainers |= (int)fillContainers[i].Container.ContainerType;
                }
            }
            return filledContainers;
        }

        public static void PrintContainerNeeded(int filledContainers, FillContainer[] fillContainers)
        {
            Console.WriteLine("Вам потребуются следующие контейнеры:");
            for (int i = 0; i < fillContainers.Length; i++)
            {
                if ((filledContainers & (int)fillContainers[i].Container.ContainerType) > 0)
                {
                    Console.WriteLine("{0} л: {1} шт.", fillContainers[i].Container.Capacity,
                        fillContainers[i].Quantitiy);
                }
            }

            Console.WriteLine("Использованы контейнеры: {0}", 
                ((ContainerTypes)(filledContainers & (int)ContainerTypes.AllContainers)).ToString());
            Console.WriteLine("Не использованы контейнеры: {0}",
                ((ContainerTypes)(filledContainers ^ (int)ContainerTypes.AllContainers)).ToString());
        }

        public static int ReadVolumeCeiling()
        {
            Console.Write("Какой объем сока (в литрах) требуется упаковать: ");
            double juiceVolume;
            bool tryParse = double.TryParse(Console.ReadLine().Replace(',', '.'),
                NumberStyles.Float, CultureInfo.InvariantCulture, out juiceVolume);
            if (!tryParse || juiceVolume <= 0)
            {
                return -1;
            }

            return (int)Math.Ceiling(juiceVolume);
        }
    }

    [Flags]
    public enum ContainerTypes
    {
        None    = 0,
        Small   = 0b_0001,
        Medium  = 0b_0010,
        Large   = 0b_0100,
        AllContainers = 0b_0111
    }

    public class Container
    {
        private ContainerTypes _containerType;
        private int _capacity;

        public ContainerTypes ContainerType { get => _containerType; }
        public int Capacity { get => _capacity; }

        public Container(ContainerTypes containerType, int capacity)
        {
            _containerType = containerType;
            _capacity = capacity;
        }
    }

    public class FillContainer
    {
        Container _container;
        int _quantitiy;

        public Container Container { get => _container; }
        public int Quantitiy { get => _quantitiy; set => _quantitiy = value; }

        public FillContainer(Container container, int quantitiy)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _quantitiy = quantitiy;
        }
    }
}
