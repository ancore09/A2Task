using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using A2_test_task;
using PuppeteerSharp;

namespace A2Task
{
    internal class Program
    {
        private static Scrapper scrapper;
        private static Cache cache;
        private static DBManager dbManager;
        
        public static async Task Main(string[] args)
        {
            cache = new Cache();
            cache.Update();

            dbManager = DBManager.GetInstance();

            var browserFetcher = new BrowserFetcher();

            // Эту строчку необходимо раскомментировать при первом запуске программы, чтобы библиотека установила браузер
            // browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
            
            var options = new LaunchOptions()
            {
                Headless = true,
                ExecutablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision)
            };
            scrapper = new Scrapper("https://www.lesegais.ru/open-area/deal", options);
            await Task.Delay(2000);

            Timer timer = new Timer();
            timer.Interval = 600_000;
            timer.Elapsed += FetchAndParse;
            timer.Start();
            Console.WriteLine("Timer started");

            Console.ReadLine();
            timer.Stop();
        }

        public static async void FetchAndParse(object source, ElapsedEventArgs args)
        {
            scrapper.GoToBaseUrl();
            
            List<Model> list = await scrapper.GetInfoFromPage(5, 3000, 1000);
            
            Console.WriteLine("Scrapped rows: " + list.Count);
            
            int numOfSameRows = 0;
            foreach (var model in list)
            {
                if (cache.Contains(model))
                {
                    numOfSameRows++;
                    continue;
                }

                Console.WriteLine(model);
                cache.Add(model);
                dbManager.InsertRow(model);
            }

            Console.WriteLine("Number of same rows: " + numOfSameRows);
            cache.Update();
            Console.WriteLine("Cache count: "+cache.Set.Count);
            Console.WriteLine();
        }
    }
}