using System;
using System.Collections.Generic;
using System.Text;

namespace L17CW
{
    class RandomDataGenerator
    {
        public delegate void RandomDataGeneratedHandler(int bytesDone, int totalBytes);

        public event RandomDataGeneratedHandler RandomDataGenerated;
        public event EventHandler RandomDataGenerationDone;

        public byte[] GetRandomData(int dataSize, int bytesDoneToRaiseEvent)
        {
            byte[] result = new byte[dataSize];
            var rand = new Random();
            int counter = 0;
            int holdDataSize = dataSize;
            while (dataSize > 0)
            {
                for (int i = 0; i < bytesDoneToRaiseEvent; i++)
                {
                    if (dataSize < 1)
                    {
                        break;
                    }
                    result[counter] = (byte)rand.Next(0, 256);
                    counter++;
                    dataSize--;
                }
                RandomDataGenerated?.Invoke(counter, holdDataSize);
            }
            Console.WriteLine($"{nameof(counter)} = {counter}");
            RandomDataGenerationDone(this, EventArgs.Empty);
            //rand.NextBytes(result);
            return result;
        }
    }
}
