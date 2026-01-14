using System;
using System.Collections.Generic;
using System.Text;

namespace Auditfunction
{
    public  class AuditData
    {
        public string? WorkEffortType { get; set; }
        public string? WorkEffortId { get; set; }
        public string? AuditType { get; set; }
        public DateTime WorkEffortCreatedDate { get; set; }
        public DateTime? WorkEffortExpirationDate { get; set; }
        public int? WorkEffortManagerId { get; set; }
        public string? WorkEffortStatus { get; set; }
    }
}
