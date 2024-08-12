using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.CreateAssets
{
    public class CreateAssetsRequst :IRequest<int>
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

        [NotMapped]
        public IFormFile Image {  get; set; }
    }
}
