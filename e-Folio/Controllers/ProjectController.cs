using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_Folio
{
    [Route("api/books")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public ProjectController()
        {

        }
    }
}