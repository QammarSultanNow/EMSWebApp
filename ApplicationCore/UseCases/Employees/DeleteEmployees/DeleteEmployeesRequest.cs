﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employees.DeleteEmployees
{
    public class DeleteEmployeesRequest :IRequest<int>
    {
        public int Id { get; set; }
    }
}
