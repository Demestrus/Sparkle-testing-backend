using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SparkleTesting.Domain;

namespace SparkleTesting.Persistence
{
    public class SparkleDbContext : IdentityDbContext<User>
    {
    }
}
