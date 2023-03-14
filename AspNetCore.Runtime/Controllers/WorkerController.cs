using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Runtime.Controllers
{
	[Route("api/worker")]
	public class WorkerController : Controller
	{
		private void DoSqlWork(int time)
		{
			var timespan = TimeSpan.FromMilliseconds(time);

			string sqltime = timespan.ToString();

			using (var connection = new SqlConnection("Server=.\\SQLEXPRESS; Database=master; Integrated Security=True"))
			{
				connection.Open();

				using (var command = new SqlCommand($"WAITFOR DELAY '{sqltime}'", connection))
				{
					command.ExecuteNonQuery();
				}
			}
		}

		[HttpGet]
		[Route("do-work")]
		public dynamic DoWork(int time)
		{
			if (time >= 0)
			{
				//Thread.Sleep(time);
				DoSqlWork(time);
			}

			return JsonConvert.SerializeObject(new
			{
				t1 = Request.HttpContext.Items["TIME_1"],
				t2 = Request.HttpContext.Items["TIME_2"],
				t3 = Request.HttpContext.Items["TIME_3"],
				t4 = Request.HttpContext.Items["TIME_4"]
			}, Formatting.Indented);
		}

		[HttpGet]
		[Route("do-work-async")]
		public async Task<string> DoWorkAsync(int time)
		{
			if (time >= 0)
			{
				await Task.Delay(time);
			}

			return "ok";
		}

		[HttpGet]
		[Route("do-work-async-waited")]
		public async Task<string> DoWorkAsyncWaited(int time)
		{
			if (time >= 0)
			{
				Task.Delay(time).Wait();
			}

			return "ok";
		}
	}
}
