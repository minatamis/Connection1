using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection1.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("Category")]
        public int CategId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductName { get; set; }

        public DateTime? AddedDate { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public double Price { get; set; }

        public bool IsBestSeller { get; set; }

        public string ProductImagePath { get; set; } = "";

        public virtual MenuCategory Category { get; set; }
    }
}
