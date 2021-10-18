using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FiyatlisteAltsevkadres
    {
        public long Id { get; set; }
        public long? Fiyataltlisteid { get; set; }
        public long? Sevkadresid { get; set; }
    }
}
