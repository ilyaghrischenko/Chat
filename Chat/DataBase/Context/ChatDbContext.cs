using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataBase.Context;

public class ChatDbContext : DbContext
{
    public ChatDbContext() { }
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer
            (@"data source=sql.bsite.net\MSSQL2016;initial catalog=iluhahr_Chat;User ID=iluhahr_Chat;Password=12qzwx; Trust Server Certificate=True;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Связь между Message и User
        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь между Message и ChatDetail
        modelBuilder.Entity<Message>()
            .HasOne(m => m.ChatDetail)
            .WithMany(cd => cd.Messages)
            .HasForeignKey(m => m.ChatDetailId);

        // Связь между ChatDetail и User1
        modelBuilder.Entity<ChatDetail>()
            .HasOne(cd => cd.User1)
            .WithMany()
            .HasForeignKey(cd => cd.User_1Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь между ChatDetail и User2
        modelBuilder.Entity<ChatDetail>()
            .HasOne(cd => cd.User2)
            .WithMany()
            .HasForeignKey(cd => cd.User_2Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<ChatDetail> ChatDetails { get; set; }
}