using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Core;

namespace Reminder.Storage.WebApi.Client
{
	public class ReminderStorageWebApiClient : IReminderStorage
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseWebApiUrl;

		public ReminderStorageWebApiClient(string baseWebApiUrl)
		{
			if (baseWebApiUrl == null)
				throw new ArgumentException(
					$"Parameter '{nameof(baseWebApiUrl)}' should not be empty.",
					nameof(baseWebApiUrl));

			_baseWebApiUrl = baseWebApiUrl.TrimEnd('/');
			_httpClient = new HttpClient();
		}

		public ReminderStorageWebApiClient(IConfiguration config, IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_baseWebApiUrl = config["storageWebApiUrl"];
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPosition = 0)
		{
			var queryParams = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("status", ((int) status).ToString())
			};

			if (count > 0)
			{
				queryParams.Add(new KeyValuePair<string, string>("count", count.ToString()));
			}

			if (startPosition > 0)
			{
				queryParams.Add(new KeyValuePair<string, string>("startPosition", startPosition.ToString()));
			}

			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				"/api/reminders" + BuildQueryString(queryParams));

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw CreateException(httpResponseMessage);
			}

			var list = JsonConvert.DeserializeObject<List<ReminderItemGetModel>>(
				httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());

			if (list == null)
				throw new Exception("Body cannot be parsed as List<ReminderItemGetModel>.");

			return list
				.Select(m => m.ToReminderItem())
				.ToList();
		}

		public List<ReminderItem> Get(int count = 0, int startPosition = 0)
		{
			var queryParams = new List<KeyValuePair<string, string>>();

			if (count > 0)
			{
				queryParams.Add(new KeyValuePair<string, string>("count", count.ToString()));
			}

			if (startPosition > 0)
			{
				queryParams.Add(new KeyValuePair<string, string>("startPosition", startPosition.ToString()));
			}

			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				"/api/reminders" + BuildQueryString(queryParams));

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw CreateException(httpResponseMessage);
			}

			var list = JsonConvert.DeserializeObject<List<ReminderItemGetModel>>(
				httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());

			if (list == null)
				throw new Exception("Body cannot be parsed as List<ReminderItemGetModel>.");

			return list
				.Select(m => m.ToReminderItem())
				.ToList();
		}

		public ReminderItem Get(Guid id)
		{
			var httpResponseMessage = CallWebApi(
				HttpMethod.Get,
				$"/api/reminders/{id}");

			if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
			{
				throw CreateException(httpResponseMessage);
			}

			var reminderItemGetModel = JsonConvert.DeserializeObject<ReminderItemGetModel>(
				httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());

			if (reminderItemGetModel == null)
				throw new Exception("Body cannot be parsed as ReminderItemGetModel.");

			return reminderItemGetModel.ToReminderItem();
		}

		public Guid Add(ReminderItemRestricted restrictedReminder)
		{
			var reminderItemCreateModel = new ReminderItemCreateModel(restrictedReminder);
			var content = JsonConvert.SerializeObject(reminderItemCreateModel);

			var httpResponseMessage = CallWebApi(
				HttpMethod.Post,
				"/api/reminders",
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.Created)
			{
				throw CreateException(httpResponseMessage);
			}

			return JsonConvert
				.DeserializeObject<ReminderItemGetModel>(
					httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult())
				.Id;
		}

		public void UpdateStatus(Guid id, ReminderItemStatus status)
		{
			var content = JsonConvert.SerializeObject(new ReminderItemUpdateModel { Status = status });

			var httpResponseMessage = CallWebApi(
				HttpMethod.Patch,
				$"/api/reminders/{id}",
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.NoContent
				&& httpResponseMessage.StatusCode != HttpStatusCode.NotFound)
			{
				throw CreateException(httpResponseMessage);
			}
		}

		public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
		{
			var reminderItemsUpdateModel = new ReminderItemsUpdateModel
			{
				Ids = ids.ToList(),
				Status = status
			};

			var content = JsonConvert.SerializeObject(reminderItemsUpdateModel);

			var httpResponseMessage = CallWebApi(
				HttpMethod.Patch,
				"/api/reminders",
				content);

			if (httpResponseMessage.StatusCode != HttpStatusCode.NoContent)
			{
				throw CreateException(httpResponseMessage);
			}
		}

		public int Count
		{
			get
			{
				var httpResponseMessage = CallWebApi(
					HttpMethod.Head,
					"/api/reminders");

				if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
				{
					throw CreateException(httpResponseMessage);
				}

				const string totalCountHeaderName = "X-Total-Count";
				if (!httpResponseMessage.Headers.Contains(totalCountHeaderName))
				{
					throw new Exception($"There is no expected header '{totalCountHeaderName}' found");
				}

				string xTotalCountHeader = httpResponseMessage.Headers
					.GetValues(totalCountHeaderName)
					.First();

				return int.Parse(xTotalCountHeader);
			}
		}

		public void Clear()
		{
			var httpResponseMessage = CallWebApi(
				HttpMethod.Put,
				"/api/reminders");

			if (httpResponseMessage.StatusCode != HttpStatusCode.NoContent)
			{
				throw CreateException(httpResponseMessage);
			}
		}

		public bool Remove(Guid id)
		{
			var httpResponseMessage = CallWebApi(
				HttpMethod.Delete,
				$"/api/reminders/{id}");

			if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
			{
				return false;
			}

			if (httpResponseMessage.StatusCode != HttpStatusCode.NoContent)
			{
				throw CreateException(httpResponseMessage);
			}

			return true;
		}

		private HttpResponseMessage CallWebApi(
			HttpMethod method,
			string relativeUrl,
			string content = null)
		{
			var request = new HttpRequestMessage(
				method,
				_baseWebApiUrl + relativeUrl);

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

			if (content != null)
			{
				request.Content = new StringContent(
					content,
					Encoding.UTF8,
					"application/json");
			}

			return _httpClient.SendAsync(request).GetAwaiter().GetResult();
		}

		private string BuildQueryString(List<KeyValuePair<string, string>> queryParams)
		{
			if (queryParams == null)
			{
				throw new ArgumentNullException(nameof(queryParams));
			}

			if (queryParams.Count == 0)
			{
				return string.Empty;
			}

			return "?" + string.Join("&", queryParams
				.Select(kvp => kvp.Key + "=" + kvp.Value)
				.ToArray());
		}

		private Exception CreateException(HttpResponseMessage httpResponseMessage)
		{
			return new Exception(
				$"Status code: {httpResponseMessage.StatusCode}.\n" +
				$"Content:\n{httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
		}
	}
}
