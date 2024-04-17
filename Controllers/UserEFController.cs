using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Dtos;
using API.Data;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserEFController : ControllerBase
    {
        DataContextEF _entityFraework;
        public UserEFController(IConfiguration config)
        {
            _entityFraework = new DataContextEF(config);
        }

        [HttpGet("GetUsers")]
        // Get all users from the database
        public IEnumerable<User> GetUsers()
        {

            IEnumerable<User> users = _entityFraework.Users.OrderByDescending(u => u.UserId).ToList();

            return users;
        }



        [HttpGet("GetSingleUser/{userid}")]
        // Get a single user from the database
        public User GetSingleUser(int userid)
        {
            User? user = _entityFraework.Users.Where(u => u.UserId == userid).FirstOrDefault();

            if (user != null) return user;
            throw new Exception("Fail to get user");
        }



        [HttpPut] // Used to Edit User
        // Update a single user in the database
        public IActionResult EditUser(User user)
        {
            User? userDb = _entityFraework.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            // If user not null update the data in that user
            if (userDb != null)
            {
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.Gender = user.Gender;
                userDb.Active = user.Active;

                if (_entityFraework.SaveChanges() > 0) return Ok();
                throw new Exception("Fail to Update User"); // If Entity Framework not > 0

            }
            throw new Exception("Fail to Update User"); // If userDb is null
        }



        [HttpPost] // Used to Add User
        // Add a single user in the database
        public IActionResult AddUser(UserToAddDto user)
        {
            User userDb = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Active = user.Active
            };

            _entityFraework.Add(userDb); // EF Add Method to Add new Data to the Model

            if (_entityFraework.SaveChanges() > 0) return Ok();
            throw new Exception("Fail to Add User"); // If Entity Framework not > 0
        }



        [HttpDelete("DeleteUser/{userid}")]
        // Delete a single user from the database
        public IActionResult DeleteUser(int userid)
        {
            User? userDb = new User();

            userDb = _entityFraework.Users.Where(u => u.UserId == userid)
                .FirstOrDefault<User>();

            if (userDb != null)
            {
                _entityFraework.Users.Remove(userDb);

                if (_entityFraework.SaveChanges() > 0) return Ok();
                throw new Exception("Fail to Delete User"); // If Entity Framework not > 0

            }
            throw new Exception("Fail to Delete User"); // If userDb is null

        }

    }
}