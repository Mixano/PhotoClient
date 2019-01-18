using DataModel;
using Microsoft.EntityFrameworkCore;
using System;


namespace TreasureServer.DataBase
{
    public class PhotoContext : DbContext
    {
        
        public DbSet<Photo> Photos { get; set; }
        public PhotoContext(DbContextOptions<PhotoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

}
