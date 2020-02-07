using System;

namespace L10HW
{
    class Person
    {
        public string Name { get; set; }
        public uint Age { get; set; }

        public uint AgeInFourYears 
        {
            get
            {
                return Age + 4;
            }
        }

        public string Description
        {
            get
            {
                return $"Name: {Name}, age in 4 years: {AgeInFourYears}.";
            }
        }

        public Person(string name, uint age)
        {
            Name = name; //?? throw new ArgumentNullException(nameof(name));
            Age = age;
        }
    }
}
