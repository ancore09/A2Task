using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using A2Task;
using PuppeteerSharp;

namespace A2_test_task
{
    public class Scrapper
    {
        private string Url { get; set; }
        private Browser Browser { get; set; }
        private Page Page { get; set; }

        public Scrapper(string url, LaunchOptions options)
        {
            Url = url;
            Browser = Puppeteer.LaunchAsync(options).Result;
            Page = Browser.NewPageAsync().Result;
            Page.GoToAsync(Url);
        }

        public async void GoToBaseUrl()
        {
            await Page.GoToAsync(Url);
        }

        public async Task<List<Model>> GetInfoFromPage(int pages, int delayBeforeTask, int delayBwnClicks)
        {
            List<Model> result = new List<Model>();

            await Task.Delay(delayBeforeTask);
            var nextBtn =
                await Page.QuerySelectorAllAsync(@"span.x-btn-button");

            for (int i = 0; i < pages; i++)
            {
                var info = await Page.EvaluateExpressionAsync<string[][]>(
                    @"Array.from(document.querySelectorAll('div.ag-row')).filter(x => x.children.length == 7).map(x => Array.from(x.children).map(y => y.outerText))");

                for (var index = 0; index < info.Length-2; index++)
                {
                    var strings = info[index];
                    var pr = strings[6].Split('/')[0].Replace("Пр: ", "").Replace(" ", "");
                    var pk = strings[6].Split('/')[1].Replace(" Пк: ", "");
                    result.Add(new Model(strings[1], strings[2], strings[3], strings[4], Double.Parse(pk), Double.Parse(pr), DateTime.ParseExact(strings[5], "dd.MM.yyyy", CultureInfo.InvariantCulture), strings[0]));
                }

                if (i != pages - 1)
                {
                    await nextBtn[2].ClickAsync();
                    await Task.Delay(delayBwnClicks);
                }
            }

            Console.WriteLine("Rows were scrapped");
            return result;
        }
    }
}