using System;
using System.Collections.Generic;
using System.Threading;
using Reminder.Domain.EventArgs;
using Reminder.Domain.Model;
using Reminder.Storage.Core;
using System.Linq;

namespace Reminder.Domain
{
    public class ReminderDomain
    {
        private IReminderStorage _storage;

        private Timer _awaitingRemindersCheckTimer;

        private Timer _readyReminderSendTimer;

        public EventHandler<ReminderItemStatusChangedEventArgs> ReminderItemStatusChanged { get; set; }

        public ReminderDomain(IReminderStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void Run()
        {
            _awaitingRemindersCheckTimer =
                new Timer(CheckAwaitingReminders,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1));
        }

        private void CheckAwaitingReminders(object dummy = null)
        {
            // read items in status awaiting
            List<ReminderItem> remindersAwaiting = _storage.Get(ReminderItemStatus.Awaiting);

            // check and if IsTimeToSend
            foreach (var item in remindersAwaiting)
            {
                if (item.IsTimeToSend)
                {
                    var previousStatus = item.Status;
                    // then update status to ReadyToSend
                    item.Status = ReminderItemStatus.ReadyToSend;
                    _storage.Update(item);

                    ReminderItemStatusChanged?.Invoke(
                        this,
                        new ReminderItemStatusChangedEventArgs(
                            new ReminderItemStatusChangedModel(
                                item,
                                previousStatus)));
                }
            }
        }

        public void SendReadyReminders()
        {
            // read items in status ready to send
            // and try to send
            List<ReminderItem> remindersReadyToSend =
                _storage.Get(ReminderItemStatus.ReadyToSend);

            foreach (var item in remindersReadyToSend)
            {
                // if okay raise event SendingSucceeded
                try
                {
                    // try send

                    var previousStatus = item.Status;
                    // then update status to ReadyToSend
                    item.Status = ReminderItemStatus.SuccessfullySend;
                    _storage.Update(item);

                    ReminderItemStatusChanged?.Invoke(
                        this,
                        new ReminderItemStatusChangedEventArgs(
                            new ReminderItemStatusChangedModel(
                                item,
                                previousStatus)));
                }
                catch (Exception e)
                {

                    throw;
                }





                // if okay raise event SendingSucceeded

                // if exception raise event SendingFailed
            }
        }
    }
}
