using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Dtos;
using API.Data;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserSalaryEFController : ControllerBase
    {
        DataContextEF _entityFramework;
        public UserSalaryEFController(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        // Get All User Salaries from the database table
        [HttpGet("GetUserSalary")]
        public IEnumerable<UserSalary> GetUserSalary()
        {
            IEnumerable<UserSalary> userSalaries = _entityFramework.UserSalaries.OrderByDescending(u => u.UserId).ToList();

            return userSalaries;
        }

        // Get a single User Salary from the database table
        [HttpGet("GetSingleUserSalary/{userid}")]
        public UserSalary GetUserSalary(int userid)
        {
            UserSalary? userSalary = _entityFramework.UserSalaries.Where(u => u.UserId == userid).FirstOrDefault();

            if (userSalary != null) return userSalary;
            throw new Exception("Fail to get user salary");
        }

        // Add a new User Salary to the database table
        [HttpPost]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            UserSalary userSalaryDb = new UserSalary()
            {
                UserId = userSalary.UserId,
                Salary = userSalary.Salary,
                AvgSalary = userSalary.AvgSalary
            };

            _entityFramework.Add(userSalaryDb); // EF Add Method to Add new Data to the Model

            if (_entityFramework.SaveChanges() > 0) return Ok();
            throw new Exception("Fail to Add User Salary"); // If Entity Framework not > 0
        }

        [HttpPut]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? user = _entityFramework.UserSalaries.Where(u => u.UserId == userSalary.UserId).FirstOrDefault();

            // If user is not null update the userSalary
            if (user != null)
            {
                user.Salary = userSalary.Salary;
                user.AvgSalary = userSalary.AvgSalary;

                if (_entityFramework.SaveChanges() > 0) return Ok();
                throw new Exception("Fail to Edit User Salary"); // If Entity Framework not > 0
            }
            throw new Exception("Fail to Edit User Salary"); // If userDb is null
        }

        [HttpDelete("DeleteUser/{userid}")]
        // Delete a single user salary from the database table
        public IActionResult DeleteUserSalary(int userid)
        {
            UserSalary? userSalary = _entityFramework.UserSalaries.Where(u => u.UserId == userid).FirstOrDefault();

            if (userSalary != null)
            {
                _entityFramework.Remove(userSalary);
                if (_entityFramework.SaveChanges() > 0) return Ok();
                throw new Exception("Fail to Delete User Salary"); // If Entity Framework not > 0
            }
            throw new Exception("Fail to Delete User Salary"); // If userSalary is null
        }


    }
}