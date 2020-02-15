using System;
using System.Collections.Generic;
using System.Text;

namespace L13CW
{
    class Plane : Aircraft
    {
        public byte EnginesCount { get; private set; }

        public Plane(int maxHeight, byte bladesCount) : base(maxHeight, 0)
        {
            EnginesCount = bladesCount;
            Console.WriteLine("It’s a plane, welcome aboard!");
        }

        public override string ToString()
        {
            return $"{nameof(EnginesCount)}: {EnginesCount}\n" + base.ToString();
        }

        public void WriteAllProperties()
        {
            Console.WriteLine(this);
        }
    }
}
