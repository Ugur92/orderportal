using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Chtanim
    {
        public long Id { get; set; }
        public string Kod { get; set; }
        public string Unvan { get; set; }
        public string B2bkullaniciadi { get; set; }
        public string B2bparola { get; set; }
    }
}
