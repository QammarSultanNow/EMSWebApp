using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.GetEmployeeOnDptId
{
    public class GetEmployeeOnDptIdHandler : IRequestHandler<GetEmployeeOnDptIdRequest, IEnumerable<EmployeeInformation>>
    {
        private readonly IAssetsRepository _assetsRepository;
        public GetEmployeeOnDptIdHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }




        public async Task<IEnumerable<EmployeeInformation>> Handle(GetEmployeeOnDptIdRequest request, CancellationToken cancellationToken)
        {
            EmployeeInformation emp = new EmployeeInformation();
            emp.Id = request.Id;
           
            var result = await _assetsRepository.GetEmployeeListOnDepartmentIdRepo(emp.Id);
            return result;
        }

    }
}
