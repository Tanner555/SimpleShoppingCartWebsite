using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebsiteTest1.Models
{
    [Table("products")]
    public class ProductItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Column("parttype")]
        [StringLength(255)]
        [Required]
        public string PartType { get; set; }
        [Column("code")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }
        [Column("image")]
        [DataType(DataType.Text)]
        public string Image { get; set; }
        [Column("price")]
        [Required]
        public double Price { get; set; }
    }
}
