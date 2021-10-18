using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FiyatlisteCarikategori
    {
        public long Id { get; set; }
        public long? Fiyatlisteid { get; set; }
        public string Kategoritip { get; set; }
        public long? Kategoriid { get; set; }
    }
}
