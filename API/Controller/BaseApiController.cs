using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class BaseApiController:ControllerBase
    {
        public BaseApiController(){
            
        }
    }
}