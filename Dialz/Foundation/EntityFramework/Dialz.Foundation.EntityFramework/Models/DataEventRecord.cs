using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.EntityFramework.Models
{
    public class DataEventRecord
    {
        [Key]
        public long DataEventRecordId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("SourceInfoId")]
        public SourceInfo SourceInfo { get; set; }
        public long SourceInfoId { get; set; }
    }
}
