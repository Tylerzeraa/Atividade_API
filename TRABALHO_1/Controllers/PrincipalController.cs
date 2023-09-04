using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using TRABALHO_1.Configuration;

namespace TRABALHO_1.Controllers
{
    public class PrincipalController
    {
        public abstract class PrincipalAlunoController : ControllerBase
        {
            protected IActionResult ApiResponse<T>(T data, string message = null)
            {
                var response = new ApiResponse<T>
                {
                    Sucess = true,
                    Message = message,
                    Data = data
                };
                return Ok(response);
            }


            protected IActionResult ApiBadRequestResponse(ModelStateDictionary modelState, string message = "invalido")
            {

                return BadRequest(message);

            }
        }
    }
}
