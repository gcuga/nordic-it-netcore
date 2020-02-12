using System;
using System.Collections.Generic;
using System.Text;

namespace L12CW
{
    class BaseDocument
    {
        public string DocName { get; set; }
        public string DocNumber { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public virtual string PropertiesString 
        {
            get
            {
                return $"{DocName} : {DocNumber} от {IssueDate}";
            }
        }

        public BaseDocument(string docName, string docNumber, DateTimeOffset issueDate)
        {
            DocName = docName ?? throw new ArgumentNullException(nameof(docName));
            DocNumber = docNumber ?? throw new ArgumentNullException(nameof(docNumber));
            IssueDate = issueDate;
        }

        public void WriteConsole()
        {
            Console.WriteLine(PropertiesString);
        }

        public void ChangeIssueDate(DateTimeOffset newIssueDate)
        {
            IssueDate = newIssueDate;
        }

        public override string ToString()
        {
            return PropertiesString;
        }
    }
}
