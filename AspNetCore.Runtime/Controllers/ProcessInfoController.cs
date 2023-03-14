using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;

namespace AspNetCore.Runtime.Controllers
{
	[Route("api/process-info")]
	public class ProcessInfoController : Controller
	{
		[HttpGet]
		[Route("")]
		public string /*dynamic*/ GetProcessInfo()
		{
			var currentProcess = Process.GetCurrentProcess();

			ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);

			ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
			ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);

			return JsonConvert.SerializeObject(new
			{
				ProcessId = currentProcess.Id,

				ThreadCount = currentProcess.Threads.Count,

				ThreadPoolInfo = new
				{
					ThreadCount = ThreadPool.ThreadCount,

					WorkerThreads = workerThreads,
					CompletionPortThreads = completionPortThreads,

					MinWorkerThreads = minWorkerThreads,
					MinCompletionPortThreads = minCompletionPortThreads,

					MaxWorkerThreads = maxWorkerThreads,
					MaxCompletionPortThreads = maxCompletionPortThreads
				}
			}, Formatting.Indented);
		}
	}
}
