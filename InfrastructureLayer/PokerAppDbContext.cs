using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Infrastructure
{    
    public class PokerAppDbContext : IdentityDbContext<IdentityUser> //DbContext
    {
        public PokerAppDbContext(DbContextOptions<PokerAppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserRooms>().HasKey(sc => new { sc.UserId, sc.RoomId });
            //modelBuilder.Entity<User>()
            //     .HasMany<Room>(s => s.Teams)
            //     .WithMany(c => c.TeamMembers)
            //     .Map(cs =>
            //     {
            //         cs.MapLeftKey("UserRefId");
            //         cs.MapRightKey("RoomRefId");
            //         cs.ToTable("UserRoom");
            //     });


            modelBuilder.Entity<User>()
                .HasMany<Room>(x => x.Teams).WithMany(x => x.TeamMembers);

            //modelBuilder.Entity<Room>().Property(p => p.TeamMembers)
            //.HasConversion(
            //     v => JsonConvert.SerializeObject(v),
            //     v => JsonConvert.DeserializeObject<List<string>>(v));

            //modelBuilder.Entity<User>()
            //    .Property(user => user.Role).HasDefaultValue("User");

            modelBuilder.Entity<Room>()
               .HasOne(x => x.Admin);

            // modelBuilder.Entity<UsersInTeams>()
            //.HasKey(e => new { e.UserId, e.RoomId });

            // modelBuilder.Entity<UsersInTeams>()
            // .HasOne<User>(e => e.User)
            // .WithMany(p => p.Teams);

            // modelBuilder.Entity<UsersInTeams>()
            // .HasOne<Room>(e => e.Team)
            // .WithMany(p => p.TeamMembers);
            //    modelBuilder.Entity<UserRooms>()
            //    .HasOne(x => x.User)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<UserRooms>().HasKey(sc => new { sc.UserId, sc.RoomId });
        }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UsersInTeams> UserRooms { get; set; }
        public DbSet<Feature> Features { get; set; }

    }

    //public class AuthenticationContext : IdentityDbContext<UserIdentity>
    //{
    //    public AuthenticationContext(DbContextOptions options) : base(options)
    //    {

    //    }


    //    public DbSet<User> UserIdentities { get; set; }

    //}
}
