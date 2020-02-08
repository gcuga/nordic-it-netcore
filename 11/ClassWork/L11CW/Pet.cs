using System;
using System.Collections.Generic;
using System.Text;

namespace L11CW
{
    class Pet
    {
        public enum AnimalKind { Unknown, Mouse, Cat, Dog }
        public enum SexType { Unknown, M, F }

        private string _birthPlace;
        private SexType _sex;

        public AnimalKind Kind;
        public string Name;

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
        public int Age
        {
            get
            {
                DateTimeOffset now = DateTimeOffset.Now;
                int year = now.Year - DateOfBirth.Year;
                if (
                     now.Month < DateOfBirth.Month ||
                       (
                         now.Month == DateOfBirth.Month &&
                         now.Day < DateOfBirth.Day
                       )
                   )
                {
                    year--;
                }

                return year;
            }
        }

        public DateTimeOffset DateOfBirth { get; set; }

        public string Description
        {
            get
            {
                return $"{Name} is a {Kind} ({Sex}) of {Age} years old." +
                    $" birth place: {_birthPlace}.";
            }
        }

        public string ShortDescription
        {
            get
            {
                return $"{Name} is a {Kind} of {Age} years old.";
            }
        }

        public Pet()
        {
        }

        public Pet(string birthPlace, AnimalKind kind, string name, SexType sex, DateTimeOffset dateOfBirth)
        {
            _birthPlace = birthPlace ?? throw new ArgumentNullException(nameof(birthPlace));
            Kind = kind;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Sex = sex;
            DateOfBirth = dateOfBirth;
        }

        public void SetBirthPlace(string birthPlace)
        {
            _birthPlace = birthPlace;
        }

        public void WriteDescription(bool isFullDescription = false)
        {
            Console.WriteLine(isFullDescription ? Description : ShortDescription);
        }
    }
}
