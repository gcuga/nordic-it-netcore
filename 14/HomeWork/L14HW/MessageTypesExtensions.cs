using L14HW.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace L14HW
{
    public static class MessageTypesExtensions
    {
        private static readonly Dictionary<MessageTypes, string> _messageTypeName 
            = new Dictionary<MessageTypes, string>()
            {
                { MessageTypes.Unknown, "MessageTypes.Unknown" },
                { MessageTypes.Info,    "MessageTypes.Info" },
                { MessageTypes.Warning, "MessageTypes.Warning" },
                { MessageTypes.Error,   "MessageTypes.Error" }
            };

        public static string GetName(this MessageTypes messageType)
        {
            return GetName(messageType, CultureInfo.CurrentCulture);
        }

        public static string GetName(this MessageTypes messageType, CultureInfo cultureInfo)
        {
            string messageTypeName = messageType.ToString();

            if (cultureInfo is null || CultureInfo.Equals(cultureInfo, CultureInfo.InvariantCulture))
            {
                return messageTypeName;
            }

            try
            {
                messageTypeName = Resources.ResourceManager.GetString(_messageTypeName[messageType], cultureInfo);
            }
            catch (Exception)
            {
                // по хорошему нужно логировать причину, их тут может быть много
                messageTypeName = messageType.ToString();
            }

            if (messageTypeName is null || messageTypeName == string.Empty)
            {
                messageTypeName = messageType.ToString();
            }

            return messageTypeName;
        }

        public static string GetBinaryLiteral(this MessageTypes messageType)
        {
            return $"0b{Convert.ToString((int)messageType, 2).PadLeft(4, '0')}";
        }
    }
}
