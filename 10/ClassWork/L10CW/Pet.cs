using System;
using System.Collections.Generic;
using System.Text;

namespace L10CW
{
    class Pet
    {
        public enum AnimalKind { Unknown, Mouse, Cat, Dog }
        public enum SexType { Unknown, M, F }

        private string _birthPlace;
        public AnimalKind Kind;
        public string Name;

        private SexType _sex;
        public SexType Sex
        {
            get { return _sex; }
            set 
            {
                if (!Enum.IsDefined(typeof(SexType), value))
                {
                    throw new ArgumentException("Задан несуществующий пол");
                }
                _sex = value;
            }
        }
        public int Age { get; set; }

        public string Description
        {
            get
            {
                return $"{Name} is a {Kind} ({Sex}) of {Age} years old." +
                    $" birth place: {_birthPlace}.";
            }
        }
        public void SetBirthPlace(string birthPlace)
        {
            _birthPlace = birthPlace;
        }
    }
}
