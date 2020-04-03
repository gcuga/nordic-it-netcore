using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MihaZupan;

namespace Reminder.Receiver.Telegram.Tests
{
	[TestClass]
	public class TelegramReminderReceiverTests
	{
		private const string _telegramBotAccessToken = "633428988:AAHLW_LaS7A47PDO2I8sbLkIIM9L0joPOSQ";
		private const string _telegramBotProxyHost = "proxy.golyakov.net";
		private const int _telegramBotProxyPort = 1080;

		[TestMethod]
		public void GetHelloFromBot_Returns_Not_Empty_String()
		{
			IWebProxy telegramProxy =
				new HttpToSocks5Proxy(_telegramBotProxyHost, _telegramBotProxyPort);
			var receiver = new TelegramReminderReceiver(_telegramBotAccessToken, telegramProxy);

			string description = receiver.GetHelloFromBot();

			Assert.IsNotNull(description);
		}
	}
}
