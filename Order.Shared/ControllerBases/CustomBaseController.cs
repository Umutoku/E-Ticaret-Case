using Microsoft.AspNetCore.Mvc;
using Order.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(CustomResponse<T> customResponse)
        {
            return new ObjectResult(customResponse)
            {
                StatusCode = customResponse.StatusCode
            };
        }
    }
}
