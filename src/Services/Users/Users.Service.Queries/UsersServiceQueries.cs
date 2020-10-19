using MongoDB.Driver;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Domain;
using Users.Persistence.Database;
using Users.Service.Queries.DTOs;

namespace Users.Service.Queries
{
    public interface IUsersServiceQueries
    {
        Task<UserDto> GetAsync(string id);
        Task<UserDto> GetByFirstnameAsync(string firstname);
        Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null);
    }

    public class UsersServiceQueries : IUsersServiceQueries
    {
        private readonly ApplicationDbContext _context;

        public UsersServiceQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetByFirstnameAsync(string firstname)
        {
            var user = await _context.Users.Find(x => x.Firstname == firstname).FirstOrDefaultAsync();

            return user.MapTo<UserDto>();
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _context.Users.Find(x => x.Id == id).FirstOrDefaultAsync();

            return user.MapTo<UserDto>();
        }

        public async Task<DataCollection<UserDto>> GetAllAsync(int page, int take, IEnumerable<string> users = null)
        {
            DataCollection<User> collection = users == null
                ? await _context.Users.Find(user => true)
                .GetPagedAsync(page, take)
                : await _context.Users.Find(user => true || users.Contains(user.Id))
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }
    }
}
