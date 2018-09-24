using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppCode.Models.Db
{
    [Table("Table_A")]
    public class TableA
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}