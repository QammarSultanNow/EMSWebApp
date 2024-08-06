using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.UpdateEmployees
{
    public class UpdateEmployeeRequest : IRequest<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adress { get; set; }


        [Required]
        public string Designation { get; set; }

        [Required]
        public string ContactNo { get; set; }

        public string? ImagePath { get; set; }

        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
