using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Receiver.Core
{
    public interface IReminderReceiver
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        void Run();
    }
}
