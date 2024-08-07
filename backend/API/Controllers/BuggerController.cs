using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggerController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFoundRequest()
        {
            Product? product = _storeContext.Products.Find(44);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }

        [HttpGet("server-error")]
        public ActionResult GetServerErrorRequest()
        {
            Product? product =
                _storeContext.Products.Find(44) ?? throw new Exception("Test Exception");
            return Ok(product);
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}
