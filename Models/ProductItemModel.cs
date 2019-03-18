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
        public string GetIDColumn => "id";
        [Column("name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        public string GetNameColumn => "name";
        [Column("parttype")]
        [StringLength(255)]
        [Required]
        public string PartType { get; set; }
        public string GetPartTypeColumn => "parttype";
        [Column("code")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }
        public string GetCodeColumn => "code";
        [Column("image")]
        [DataType(DataType.Text)]
        public string Image { get; set; }
        public string GetImageColumn => "image";
        [Column("price")]
        [Required]
        public double Price { get; set; }
        public string GetPriceColumn => "price";
    }
}
