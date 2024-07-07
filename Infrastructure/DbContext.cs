using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

public class AspireAppDbContext : DbContext
{
    public AspireAppDbContext(DbContextOptions<AspireAppDbContext> options) : base(options)
    {
    }
}

