using System;
using System.Collections.Generic;
using System.Text;

namespace L13CW
{
    class Helicopter : Aircraft
    {
        public byte BladesCount { get; private set; }

        public Helicopter(int maxHeight, byte bladesCount) : base(maxHeight, 0)
        {
            BladesCount = bladesCount;
            Console.WriteLine("It’s a helicopter, welcome aboard!");
        }

        public override string ToString()
        {
            return $"{nameof(BladesCount)}: {BladesCount}\n" + base.ToString();
        }

        public void WriteAllProperties()
        {
            Console.WriteLine(this);
        }
    }
}
