using System;
using System.Collections.Generic;

namespace voice.middleware.Models.Level.Response
{
    public class LevelDetail
    {
        public string Id { get; set; }
        public int number { get; set; }
        public int Level { get; set; }
        public int XpPoints { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class LevelListResponse
    {
        public List<LevelDetail> details { get; set; }
    }
}