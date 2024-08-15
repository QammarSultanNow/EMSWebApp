using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.UseCases.Employees.CreateEmployee
{

    public class CreateEmployeesRequest : IRequest<int>
    {
        [Key]
        public int Id { get; set; }

       
        public string Name { get; set; }

       
        public string Email { get; set; }

        
        public string Adress { get; set; }


     
        public string Designation { get; set; }

   
        public string ContactNo { get; set; }

        public string? ImagePath { get; set; }

        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public IFormFile Image { get; set; }
    }


}
