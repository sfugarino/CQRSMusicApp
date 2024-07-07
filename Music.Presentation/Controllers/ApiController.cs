using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Presentation.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        public ApiController(ISender sender)
        {
            Sender = sender;
        }
    }
}
