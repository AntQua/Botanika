using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLOUD462022.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(100, ErrorMessage = "Maximum 100 charaters allowed")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }

    }
}
