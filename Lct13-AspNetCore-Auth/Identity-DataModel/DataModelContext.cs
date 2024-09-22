using Identity_DataModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_DataModel;

public class DataModelContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DataModelContext(DbContextOptions<DataModelContext> options) : base(options)
    {
    }
}
