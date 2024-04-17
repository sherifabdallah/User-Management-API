using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Dtos;
using API.Data;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DataContextDapper _dapper;
        public UserController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("GetUsers")]
        // Get all users from the database
        public IEnumerable<User> GetUsers()
        {
            string sql = @"SELECT [UserId],
                                  [FirstName],
                                  [LastName],
                                  [Email],
                                  [Gender],
                                  [Active]
                           FROM [DotNetCourseDatabase].[dbo].[Users]
                           ORDER BY UserId DESC
                           ";

            IEnumerable<User> users;
            users = _dapper.LoadData<User>(sql);

            return users;
        }


        [HttpGet("GetSingleUser/{userid}")]
        // Get a single user from the database
        public User GetSingleUser(int userid)
        {
            string sql = @$"SELECT [UserId],
                                  [FirstName],
                                  [LastName],
                                  [Email],
                                  [Gender],
                                  [Active]
                           FROM [DotNetCourseDatabase].[dbo].[Users]
                           WHERE [UserId] = {userid}
                           ";

            User user;
            user = _dapper.LoadDataSingle<User>(sql);

            return user;
        }

        [HttpPut] // Used to Edit User
        // Update a single user in the database
        public IActionResult EditUser(User user)
        {

            string sql = @$" 
                        UPDATE dbo.Users SET
                                     [FirstName] = '{user.FirstName}',
                                     [LastName] = '{user.LastName}',
                                     [Email] = '{user.Email}',
                                     [Gender] = '{user.Gender}',
                                     [Active] = {user.Active}
                        WHERE [UserId] = {user.UserId}
                        ";

            if (_dapper.Execute(sql))
            {
                // Ok() is a method from ControllerBase class wich returns Status 200 wich means request was successful
                return Ok();
            }

            throw new Exception("Faild to Update User");

        }

        [HttpPost] // Used to Add User
        // Add a single user in the database
        public IActionResult AddUser(UserToAddDto user)
        {

            string sql = @$" 
                         INSERT INTO dbo.Users
                                     (
                                     [FirstName],
                                     [LastName],
                                     [Email],
                                     [Gender],
                                     [Active])
                         VALUES      (
                                     '{user.FirstName}',
                                     '{user.LastName}',
                                     '{user.Email}',
                                     '{user.Gender}',
                                     '{user.Active}');
                        ";

            if (_dapper.Execute(sql))
            {
                // Ok() is a method from ControllerBase class wich returns Status 200 wich means request was successful
                return Ok();
            }
            throw new Exception("Faild to Add User");

        }

        [HttpDelete("DeleteUser/{userid}")]
        // Delete a single user from the database
        public IActionResult DeleteUser(int userid)
        {
            string sql = @$"DELETE FROM [DotNetCourseDatabase].[dbo].[Users] WHERE UserId = {userid}";
            if (_dapper.Execute(sql))
            {
                // Ok() is a method from ControllerBase class which returns Status 200, indicating the request was successful
                return Ok();
            }
            throw new Exception("Fail to Delete User");
        }

    }
}
