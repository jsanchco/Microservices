using Identity.Persistence.Database.Configuration;
using IdentityServer4.Models;
using MongoDB.Driver;

namespace Identity.Persistence.Database
{
    public class ApplicationDbContext
    {
        public IMongoDatabase Database;
        public IMongoCollection<Client> Clients;
        public IMongoCollection<IdentityResource> IdentityResources;
        public IMongoCollection<ApiResource> ApiResources;
        public IMongoCollection<PersistedGrant> PersistedGrants;

        public ApplicationDbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);

            Clients = Database.GetCollection<Client>("Clients");
            IdentityResources = Database.GetCollection<IdentityResource>("IdentityResources");
            ApiResources = Database.GetCollection<ApiResource>("ApiResources");
            PersistedGrants = Database.GetCollection<PersistedGrant>("PersistedGrants");
        }
    }
}
