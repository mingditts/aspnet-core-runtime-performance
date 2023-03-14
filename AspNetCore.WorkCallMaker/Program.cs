using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace AspNetCore.WorkCallMaker
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string uri = args[0];

			int numberOfCalls = Convert.ToInt32(args[1]);

			int time = Convert.ToInt32(args[2]);

			var threads = new List<Thread>();

			for (int i = 0; i < numberOfCalls; i++)
			{
				threads.Add(new Thread(() =>
				{
					using HttpClient client = new HttpClient();

					var sw = Stopwatch.StartNew();

					var json = client.GetStringAsync($"{uri}?time={time}").Result;

					sw.Stop();

					Console.WriteLine($"Response http: {json}, in {sw.ElapsedMilliseconds} [ms]");
				}));

				threads.LastOrDefault().Start();
			}

			foreach (var thread in threads)
			{
				thread.Join();
			}
		}
	}
}
