using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.IdentityModuleDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class ApiBaseController: ControllerBase
    {
        //get Email from token
        protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email);
    }
}
