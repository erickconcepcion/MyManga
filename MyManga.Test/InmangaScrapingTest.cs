using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using InMangaModels;
using System.Linq;
using System.Text;

namespace MyManga.Test
{
    public class InmangaScrapingTest
    {
        public const string AllManga = "https://inmanga.com/OnMangaQuickSearch/Source/QSMangaList.json";
        public const string MangaDetails = "https://inmanga.com/chapter/getall?mangaIdentification={0}";
        public const string PageList = "https://inmanga.com/chapter/chapterIndexControls?identification={0}";
        public const string PageUrl = "https://inmanga.com/images/manga/{0}/chapter/{1}/page/{2}/{3}";
        public async Task<string> GetSuscessStringResponse(string url)
        {
            string resString = "";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Is not a success status code.");
                }
                resString = await response.Content.ReadAsStringAsync();
            }
            return resString;
        }
        public async Task<IEnumerable<MangaResult>> GetAllMangaResponse()
        {
            var res = await GetSuscessStringResponse(AllManga);            
            return JsonConvert.DeserializeObject<List<MangaResult>>(res);
        }

        public async Task<ChapterMessageResult> GetMangaDetails(string mangaId)
        {
            var res = await GetSuscessStringResponse(string.Format(MangaDetails, mangaId));
            var desResponse = JsonConvert.DeserializeObject<ChapterResult>(res);
            return JsonConvert.DeserializeObject<ChapterMessageResult>(desResponse.data);
        }
        public async Task<IEnumerable<string>> GetListPages(MangaResult manga, ChapterDetailResult chapter)
        {
            var res = await GetSuscessStringResponse(string.Format(PageList, chapter.Identification));
            var doc = new HtmlDocument();
            doc.LoadHtml(res);
            var pageIdList = doc.GetElementbyId("PageList")
                .ChildNodes.Where(n=>n.Name=="option").ToList()
                .Select(n=>string.Format(PageUrl,
                    manga.Name.Replace(" ","-"), chapter.FriendlyChapterNumberUrl, n.InnerText,n.GetAttributeValue("value", ""))
                );
            return pageIdList;
        }

        [Fact]
        public async void GetMangaTest()
        {
            
            var mangas = await GetAllMangaResponse();
            var mangaDetails = await GetMangaDetails(mangas.FirstOrDefault().Identification);

        }

        [Fact]
        public async void GetPageTest()
        {
            var manga = new MangaResult {
                Identification = "74d824c1-85ba-4544-8fb2-aa4ccecd9eea",
                Name = "Nanatsu no Taizai",
                BroadcastStatus = 1
            };
            var chapter = new ChapterDetailResult {
                PagesCount = 28,
                Id = 98,
                Number = 121.0,
                Identification = "89ea769e-c871-4fea-95cd-e19d4421ceaa",
                FriendlyChapterNumber = "121", FriendlyChapterNumberUrl = "121"
            };
            var listPages=await GetListPages(manga, chapter);

        }
    }
}
