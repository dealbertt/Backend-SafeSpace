using System.Collections.Generic;
using System.Reflection.Emit;
using Backend_SafeSpace.Migrations;
using Microsoft.EntityFrameworkCore;
namespace Backend_SafeSpace
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Profile> profiles { get; set; }

        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<UserChatroom> UserChatrooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<User>()
                .HasOne(u => u.profile)
                 .WithOne(p => p.user)
                .HasForeignKey<Profile>(p => p.UserId);


            modelbuilder.Entity<UserChatroom>()
                .HasKey(uc => new { uc.UserId, uc.ChatroomId }); // Composite key

            modelbuilder.Entity<UserChatroom>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChatrooms)
                .HasForeignKey(uc => uc.UserId);

            modelbuilder.Entity<UserChatroom>()
                .HasOne(uc => uc.Chatroom)
                .WithMany(c => c.UserChatrooms)
                .HasForeignKey(uc => uc.ChatroomId);

            modelbuilder.Entity<Message>()
            .HasOne(m => m.Chatroom)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatroomId);

            modelbuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId);
        }
    }

}
