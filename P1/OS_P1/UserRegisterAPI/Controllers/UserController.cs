using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegisterAPI.Models;

namespace UserRegisterAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _db;

        public UserController(ILogger<UserController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet(Name = "LoginUser")]
        public JsonResult LoginUser(string username, string password)
        {
            try
            {
                if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(username))
                {
                    return new JsonResult("********Please Enter Username and Password********")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var user = _db.Users.Where(e => e.Username.Equals(username) && e.Password.Equals(password)).FirstOrDefault();

                if (user == null)
                {
                    return new JsonResult("********No login********")
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                return new JsonResult(
                    new
                    {
                        message = "********Login Successful********",
                        user = user
                    }
                    )
                {
                    StatusCode = StatusCodes.Status200OK
                };


            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

            }

        }


        [HttpPost(Name = "Signup")]
        public JsonResult Signup(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    _db.Users.Add(model);
                    _db.SaveChanges();
                    return new JsonResult("********Signup successfuly*********")
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                else
                {
                    return new JsonResult("********No signup please add valid user********")
                    {
                        StatusCode = StatusCodes.Status417ExpectationFailed
                    };
                }


            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

            }

                }

    }
}
