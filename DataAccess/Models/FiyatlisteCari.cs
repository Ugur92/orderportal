using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class FiyatlisteCari
    {
        public long Id { get; set; }
        public long? Fiyatlisteid { get; set; }
        public long? Chid { get; set; }
    }
}
