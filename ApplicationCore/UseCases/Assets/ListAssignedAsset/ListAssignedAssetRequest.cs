﻿using ApplicationCore.AssetsModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Assets.ListAssignedAsset
{
    public class ListAssignedAssetRequest : IRequest<IEnumerable<ListEmployeeAssetViewModel>>
    {
        public int Id { get; set; }
    }
}
