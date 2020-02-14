using System;
using System.Collections.Generic;
using System.Text;

namespace L12HW
{
    class ChatReminderItem : ReminderItem
    {
        public string ChatName { get; set; }
        public string AccountName { get; set; }

        public ChatReminderItem(string chatName,
                                string accountName,
                                DateTimeOffset alarmDate,
                                string alarmMessage = null) : base(alarmDate, alarmMessage)
        {
            ChatName = chatName ?? throw new ArgumentNullException(nameof(chatName));
            AccountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
        }

        public override string ToString()
        {
            return $"Reminder for chat/user: {ChatName}/{AccountName}\n" + base.ToString();
        }
    }
}
