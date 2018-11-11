using HtmlAgilityPack;
using MyManga.InMangaModels;
using MyManga.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyManga.Services
{
    public class InMangaService
    {
        public const string AllManga = "https://inmanga.com/OnMangaQuickSearch/Source/QSMangaList.json";
        public const string MangaDetails = "https://inmanga.com/chapter/getall?mangaIdentification={0}";
        public const string PageList = "https://inmanga.com/chapter/chapterIndexControls?identification={0}";
        public const string PageUrl = "https://inmanga.com/images/manga/{0}/chapter/{1}/page/{2}/{3}";
        public async Task<string> GetSuscessStringResponse(string url)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new UnsuccessfulRequestException();
            }
            string resString = "";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);                
                if (!response.IsSuccessStatusCode)
                {
                    throw new UnsuccessfulRequestException(response.StatusCode);
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
                .ChildNodes.Where(n => n.Name == "option").ToList()
                .Select(n => string.Format(PageUrl,
                    manga.Name.Replace(" ", "-"), chapter.FriendlyChapterNumberUrl, n.InnerText, n.GetAttributeValue("value", ""))
                );
            return pageIdList;
        }

        public async Task<IEnumerable<MangaPage>> GetListPageModels(MangaResult manga, ChapterDetailResult chapter)
        {
            var res = await GetSuscessStringResponse(string.Format(PageList, chapter.Identification));
            var doc = new HtmlDocument();
            doc.LoadHtml(res);
            var pageIdList = doc.GetElementbyId("PageList")
                .ChildNodes.Where(n => n.Name == "option").ToList()
                .Select(n => new MangaPage {
                    Identification = n.GetAttributeValue("value", ""),
                    PageNumber = $"{n.InnerText}/{chapter.PagesCount}",
                    ImageUrl = string.Format(PageUrl,
                        manga.Name.Replace(" ", "-"), chapter.FriendlyChapterNumberUrl, n.InnerText, 
                        n.GetAttributeValue("value", ""))
                }               
                );
            return pageIdList;
        }

    }
}
