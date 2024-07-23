using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.AssetsModel
{
   
    public class Assets
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int PurchasingPrice { get; set; }

        [Required]
        public string Status { get; set; }

        public string? ImagePath { get; set; }

        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
