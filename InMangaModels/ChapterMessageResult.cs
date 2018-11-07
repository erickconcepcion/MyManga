using System;
using System.Collections.Generic;
using System.Text;

namespace InMangaModels
{
    public class ChapterMessageResult
    {
        public string message { get; set; }
        public bool success { get; set; }
        public IEnumerable<ChapterDetailResult> result { get; set; }
    }
}
