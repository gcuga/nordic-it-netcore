using System;
using System.Collections.Generic;
using System.Text;

namespace L12CW
{
    internal class Passport : BaseDocument
    {
        public string Country { get; set; }
        public string PersonName { get; set; }
        public override string PropertiesString
        {
            get
            {
                return $"{PersonName} {Country}\n" 
                    + $"{base.DocName} : {base.DocNumber} от {base.IssueDate}";
            }
        }

        public Passport(string personName, string country, 
             string docNumber, DateTimeOffset issueDate) 
            : base(docName: "Passport", docNumber, issueDate)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));
            PersonName = personName ?? throw new ArgumentNullException(nameof(personName));
        }


        //public override void WriteConsole()
        //{
        //    Console.WriteLine(this.PropertiesString);
        //}

        //public new void WriteConsole()
        //{
        //    Console.WriteLine(this.PropertiesString);
        //}

    }
}
