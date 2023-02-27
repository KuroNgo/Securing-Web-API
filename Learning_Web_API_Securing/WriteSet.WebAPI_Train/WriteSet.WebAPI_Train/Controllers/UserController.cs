using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using WriteSet.WebAPI_Train.Models;

namespace WriteSet.WebAPI_Train.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        public UserController()
        {
        }

        private List<Users> AllUser()
        {
            List<Users> lists = new List<Users>();
            for (int i = 1; i < 6; i++)// Tao ra 6 users
            {
                Users u = new Users() // Tao ra User
                {
                    Username = $"user {i}",
                    Password = $"Password {i}",
                    Fullname = $"Fullname {i}",
                    IsActive = true
                };
                lists.Add(u);//Them vao danh sach Users
            }
            return lists;
        }

        //GET api/User/GetAllUser
        [System.Web.Http.HttpGet] // Cho phép truy cập với phương thức là GET
        public HttpResponseMessage GetAllUser()
        {
            var list = AllUser();
            if (list != null)
                return Request.CreateResponse(HttpStatusCode.OK, list);
            else return Request.CreateResponse(HttpStatusCode.NotFound,"Not found");
        }

        //POST api/Users/CreateNew
        public HttpResponseMessage CreateNew(Users u)
        {
            try
            {
                var list = AllUser();
                list.Add(u);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/Users/UpdateUser
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Post(Users u)
        {
            try
            {
                var list = AllUser();
                // lấy index của User thông qua username
                int index = list.FindIndex(p => p.Username == u.Username);
                list[index] = u;
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Users/DeleteUser
        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete(Users u)
        {
            try
            {
                var list = AllUser();
                int index = list.FindIndex(p => p.Username == u.Username);
                list.RemoveAt(index);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
                throw;
            }
        }
    }
}
