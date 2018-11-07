using System;
using System.Collections.Generic;
using System.Text;

namespace MyManga.InMangaModels
{
    public class ChapterDetailResult
    {
        public int Id { get; set; }
        public int PagesCount { get; set; }
        public double Number { get; set; }
        public string Identification { get; set; }
        public string FriendlyChapterNumber { get; set; }
        public string FriendlyChapterNumberUrl { get; set; }

        public string ShowChapter { get { return $"Capitulo: {FriendlyChapterNumber}"; } }
    }
}
