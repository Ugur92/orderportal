using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FiyatlisteCarigrup
    {
        public long Id { get; set; }
        public long? Fiyatlisteid { get; set; }
        public long? Grupid { get; set; }
    }
}
