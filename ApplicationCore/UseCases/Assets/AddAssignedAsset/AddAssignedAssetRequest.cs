using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.AddAssignedAsset
{
    public class AddAssignedAssetRequest : IRequest<int>
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AssetId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public string Remarks { get; set; }
    }
}
