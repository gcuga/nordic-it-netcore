using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Core;

namespace Reminder.Storage.WebApi.Client
{
	public class ReminderStorageWebApiClient : IReminderStorage
	{
		private HttpClient _httpClient;
		private string _baseWebApiUrl;


		public ReminderStorageWebApiClient(string baseWebApiUrl)
		{
			_httpClient = new HttpClient();
			_baseWebApiUrl = baseWebApiUrl;
		}

		public int Count => throw new NotImplementedException();

		public Guid Add(ReminderItemRestricted reminderItemRestricted)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
				HttpMethod.Post,
				"https://localhost:44344/api/reminders/");

			ReminderItemCreateModel reminderItemCreateModel =
				new ReminderItemCreateModel(reminderItemRestricted);

			string content = JsonConvert.SerializeObject(reminderItemCreateModel);
			httpRequestMessage.Content = new StringContent(
				content,
				Encoding.UTF8,
				"application/json");

			// посылаем и получаем ответ
			var httpResponceMessage = _httpClient.SendAsync(httpRequestMessage).Result;

			if (httpResponceMessage.StatusCode != HttpStatusCode.Created)
			{
				throw new Exception(
					$"Error: {httpResponceMessage.StatusCode}, " +
					$"Content: {httpResponceMessage.Content.ReadAsStringAsync().Result}");
			}

			string resultContent = httpResponceMessage.Content.ReadAsStringAsync().Result;
			ReminderItemGetModel reminderItemGetModel =
				JsonConvert.DeserializeObject<ReminderItemGetModel>(resultContent);

			return reminderItemGetModel.Id;
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public ReminderItem Get(Guid id)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
				HttpMethod.Get,
				"https://localhost:44344/api/reminders/" + id.ToString());

			var httpResponseMessage = CallWebApi(HttpMethod.Get, id.ToString());
				
				_httpClient.SendAsync(httpRequestMessage).Result;

			if (httpResponceMessage.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			if (httpResponceMessage.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception(
					$"Error: {httpResponceMessage.StatusCode}, " +
					$"Content: {httpResponceMessage.Content.ReadAsStringAsync().Result}");
			}

			string content = httpResponceMessage.Content.ReadAsStringAsync().Result;
			ReminderItemGetModel reminderItemGetModel = 
				JsonConvert.DeserializeObject<ReminderItemGetModel>(content);

			return reminderItemGetModel.ToReminderItem();
		}

		public List<ReminderItem> Get(int count = 0, int startPostion = 0)
		{
			// TODO
			return null;
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPostion = 0)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
		{
			throw new NotImplementedException();
		}

		public void UpdateStatus(Guid id, ReminderItemStatus status)
		{
			throw new NotImplementedException();
		}

		private HttpResponseMessage CallWebApi(
			HttpMethod method,
			string relativeUrl,
			string content = null)
		{
			if (content != null)
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
					method,
					_baseWebApiUrl + relativeUrl ?? string.Empty);
			}


					   
			if (method == HttpMethod.Post || method == HttpMethod.Put)
			{

			}


				"https://localhost:44344/api/reminders/");

			ReminderItemCreateModel reminderItemCreateModel =
				new ReminderItemCreateModel(reminderItemRestricted);

			string content = JsonConvert.SerializeObject(reminderItemCreateModel);
			httpRequestMessage.Content = new StringContent(
				content,
				Encoding.UTF8,
				"application/json");

			// посылаем и получаем ответ
			var httpResponceMessage = _httpClient.SendAsync(httpRequestMessage).Result;

			if (httpResponceMessage.StatusCode != HttpStatusCode.Created)
			{
				throw new Exception(
					$"Error: {httpResponceMessage.StatusCode}, " +
					$"Content: {httpResponceMessage.Content.ReadAsStringAsync().Result}");
			}

			string resultContent = httpResponceMessage.Content.ReadAsStringAsync().Result;
			ReminderItemGetModel reminderItemGetModel =
				JsonConvert.DeserializeObject<ReminderItemGetModel>(resultContent);
		}
	}
}
