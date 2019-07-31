using System;

namespace TuringBackend.Models
{
    public class Audit
    {
        public int AuditId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}