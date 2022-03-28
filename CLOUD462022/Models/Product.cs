using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Models
{

    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The product name field is required")]
        [Display(Name = "Product Name")]
        [StringLength(100, ErrorMessage = "Maximum {1} charaters are allowed")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The product price field is required")]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1,999.99, ErrorMessage = "Price must be between 1 and 999,99")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Image Thumbnail")]
        public string ImageThumbnailUrl { get; set; }

        [Required(ErrorMessage = "The product description field is required")]
        [Display(Name = "Product Description")]
        [MaxLength(200, ErrorMessage = "Maximum {1} charaters are allowed")]
        public string Description { get; set; }

        [Display(Name = "Available in Stock?")]
        public bool InStock { get; set; }

        [Display(Name = "Is favourite?")]
        public bool IsFavourite { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Product Category")]
        public virtual Category Category { get; set; }


    }

}
