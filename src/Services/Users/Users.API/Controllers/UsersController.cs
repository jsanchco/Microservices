using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Service.Queries;
using Users.Service.Queries.DTOs;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersServiceQueries _usersServiceQueries;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersServiceQueries usersServiceQueries)
        {
            _logger = logger;
            _usersServiceQueries = usersServiceQueries;
        }

        // users
        [HttpGet]
        public async Task<DataCollection<UserDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<string> users = null;

            if (!string.IsNullOrEmpty(ids))
            {
                users = ids.Split(",");
            }

            return await _usersServiceQueries.GetAllAsync(page, take, users);
        }
    }
}
