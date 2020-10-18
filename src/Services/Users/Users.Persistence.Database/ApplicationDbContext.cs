using MongoDB.Driver;
using Users.Persistence.Database.Configuration;
using Users.Domain;

namespace Users.Persistence.Database
{
    public class ApplicationDbContext
    {
        public IMongoCollection<User> Users;

        public ApplicationDbContext(IUsersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Users = database.GetCollection<User>(settings.UsersCollectionName);
        }
    }
}
