using System;

namespace MyManga.InMangaModels
{
    public class MangaResult
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        public string CreationDate { get; set; }
        public int BroadcastStatus { get; set; }
        public string BroadcastStatusDescription { get; set; }

        public string ThumbnailPathComplete
        {
            get
            {
                return $"https://inmanga.com{ThumbnailPath}";
            }
        }

    }
}
