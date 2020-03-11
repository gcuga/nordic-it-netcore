using System;

namespace Reminder.Parsing
{
    public static class MessageParser
    {
        public static ParsedMessage ParseMessage(string text)
        {
            if (string.IsNullOrEmpty(text) )
            {
                return null;
            }

            int firstSpacePosition = text.IndexOf(' ');
            if (firstSpacePosition < 2)
            {
                return null;
            }

            string potentialDate = text.Substring(0, firstSpacePosition);

            //yyy-MM-ddThh:mm:ss.ffffff - ISO format
            if (!DateTimeOffset.TryParse(potentialDate, out var date))
            {
                return null;
            }

            var message = text.Substring(firstSpacePosition).Trim();
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            return new ParsedMessage(message, date);
        }
    }
}
