using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace use_open_source_fast_report.Models
{
    [Table("Report")]
    public class Report
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }

        internal void RegisterData()
        {
            throw new NotImplementedException();
        }
    }
}
