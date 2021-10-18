using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class WebappLoginLog
    {
        public long Id { get; set; }
        public long? Chid { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
