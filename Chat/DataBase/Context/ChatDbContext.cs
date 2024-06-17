using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataBase.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer
            (@"data source=sql.bsite.net\MSSQL2016;initial catalog=sannido_ChatDb;User ID=sannido_ChatDb;Password=12qzwx; Trust Server Certificate=True;");
    
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<ChatDetail> ChatDetails { get; set; }
}