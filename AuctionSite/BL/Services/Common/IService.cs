﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Common;
using BL.QueryObjects.Common;

namespace BL.Services.Common
{
    public interface IService<TDto, TFilterDto>
        where TFilterDto : FilterDtoBase, new()
        where TDto : DtoBase
    {
        Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync();
    }
}
