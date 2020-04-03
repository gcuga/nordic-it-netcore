using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

		public Guid Add(ReminderItemRestricted reminder)
		{
			ReminderItemCreateModel reminderItemCreateModel = new ReminderItemCreateModel(reminder);
			string content = JsonConvert.SerializeObject(reminderItemCreateModel);
			
			var httpResponseMessage = CallWebApi(
				HttpMethod.Post, 
				string.Empty,
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.Created)
			{
				throw GetException(httpResponseMessage);
			}

			string resultContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
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
			var httpResponseMessage = CallWebApi(
				HttpMethod.Get, 
				id.ToString());

			if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw GetException(httpResponseMessage);
			}

			string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
			ReminderItemGetModel reminderItemGetModel = 
				JsonConvert.DeserializeObject<ReminderItemGetModel>(content);

			return reminderItemGetModel.ToReminderItem();
		}

		public List<ReminderItem> Get(int count = 0, int startPostion = 0)
		{
			throw new NotImplementedException();
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPostion = 0)
		{
			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				string.Empty);

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw GetException(httpResponseMessage);
			}

			string content = httpResponseMessage.Content.ReadAsStringAsync().Result;

			List<ReminderItemGetModel> reminderItemGetModels =
				JsonConvert.DeserializeObject<List<ReminderItemGetModel>>(content);

			List<ReminderItem> reminderItems =
				reminderItemGetModels
					.Select(x => x.ToReminderItem()).
					ToList();

			return reminderItems;
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
			string content = JsonConvert.SerializeObject(status);
			var httpResponseMessage = CallWebApi(
				HttpMethod.Patch,
				id.ToString(),
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw GetException(httpResponseMessage);
			}
		}

		private HttpResponseMessage CallWebApi(
			HttpMethod method,
			string relativeUrl,
			string content = null)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
				method,
				_baseWebApiUrl + relativeUrl ?? string.Empty);

			if (content != null)
			{
				httpRequestMessage.Content = new StringContent(
					content,
					Encoding.UTF8,
					"application/json");
			}

			return _httpClient.SendAsync(httpRequestMessage).Result;
		}

		private Exception GetException(HttpResponseMessage httpResponseMessage)
		{
			return new Exception(
				$"Error: {httpResponseMessage.StatusCode}, " +
				$"Content: {httpResponseMessage.Content.ReadAsStringAsync().Result}");
		}
	}
}
