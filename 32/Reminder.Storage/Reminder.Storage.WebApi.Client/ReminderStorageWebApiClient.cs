using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Core;

namespace Reminder.Storage.WebApi.Client
{
	public class ReminderStorageWebApiClient : IReminderStorage
	{
		private HttpClient _httpClient;
		private string _baseWebApiUrl;


		public ReminderStorageWebApiClient(IConfiguration config, IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_baseWebApiUrl = config["storageWebApiUrl"];
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

		public List<ReminderItem> Get(int count = -1, int startPosition = 0)
		{
			string relativeUrl = $"?count={count}&startPosition={startPosition}";

			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				relativeUrl);

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw GetException(httpResponseMessage);
			}

			var content = httpResponseMessage.Content.ReadAsStringAsync().Result;
			List<ReminderItemGetModel> list =
				JsonConvert.DeserializeObject<List<ReminderItemGetModel>>(content);

			return list
				.Select(r => r.ToReminderItem())
				.ToList();
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPosition = 0)
		{
			string relativeUrl = $"?status={(int)status}&count={count}&startPosition={startPosition}";

			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				relativeUrl);

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw GetException(httpResponseMessage);
			}

			var content = httpResponseMessage.Content.ReadAsStringAsync().Result;
			List<ReminderItemGetModel> list =
				JsonConvert.DeserializeObject<List<ReminderItemGetModel>>(content);

			return list
				.Select(r => r.ToReminderItem())
				.ToList();
		}

		public bool Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
		{
			foreach (Guid id in ids)
			{
				UpdateStatus(id, status);
			}
		}

		public void UpdateStatus(Guid id, ReminderItemStatus status)
		{
			ReminderItemUpdateModel reminderItemUpdateModel = new ReminderItemUpdateModel { Status = status };
			string content = JsonConvert.SerializeObject(reminderItemUpdateModel);

			var httpResponseMessage = CallWebApi(
				HttpMethod.Patch,
				id.ToString(),
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.NoContent)
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
