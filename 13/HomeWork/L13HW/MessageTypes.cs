using System;

namespace L13HW
{
    [Flags]
    public enum MessageTypes
    {
        Unknown = 0b_0000,
        Info    = 0b_0001,
        Warning = 0b_0010,
        Error   = 0b_0100,
    }
}
