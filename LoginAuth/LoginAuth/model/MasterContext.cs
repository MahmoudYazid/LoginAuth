using Microsoft.EntityFrameworkCore;

namespace LoginAuth.model
{
    public class MasterContext :DbContext
    {
        public DbSet<UserModel> users { get; set; }

        public MasterContext(DbContextOptions<MasterContext> optoions) : base(optoions) { 
        
        
        }
    }
}
