using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;

namespace ProjectHub.Data
{
    public class ProjectHubDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<UserKind> UserKinds { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Investor> Investors { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Designer> Designers { get; set; }

        public DbSet<ProjectDesigner> ProjectDesigners { get; set; }

        public DbSet<Discussion> Discussions { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<UserDiscussion> UserDiscussions { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Review> Reviews { get; set; }


        public ProjectHubDbContext(DbContextOptions<ProjectHubDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<ApplicationUser>().Property("Image").HasColumnType("varbinary(MAX)");
                        
            builder.Entity<ApplicationUser>()
                   .HasMany(u=>u.RatesReceived)
                   .WithOne(r=>r.Recipient)
                   .HasForeignKey(r=>r.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                   .HasMany(u=>u.RatesSent)
                   .WithOne(r=>r.Author)
                   .HasForeignKey(r=>r.AuthorId);

            builder.Entity<Project>()
                    .HasOne(nameof(Project.Investor))
                    .WithMany(nameof(Investor.Projects))
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Rate>()
                   .HasOne(r=>r.Recipient)
                   .WithMany(u=>u.RatesReceived)
                   .HasForeignKey(r=>r.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rate>()
                   .HasOne(r => r.Author)
                   .WithMany(u => u.RatesSent)
                   .HasForeignKey(r => r.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                   .HasOne(nameof(Rate.Recipient))
                   .WithMany(nameof(ApplicationUser.ReviewsReceived))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                   .HasOne(nameof(Rate.Author))
                   .WithMany(nameof(ApplicationUser.ReviewsSent))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Offer>()
                   .HasOne(nameof(Offer.Project))
                   .WithMany(nameof(Project.Offers))
                   .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<ProjectDesigner>()
                   .HasOne(nameof(ProjectDesigner.Project))
                   .WithMany(nameof(Project.Designers))
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<UserProject>().HasKey(up => new { up.UserId, up.ProjectId });

            builder.Entity<UserDiscussion>()
                   .HasKey(ud => new { ud.UserId, ud.DiscussionId });


            builder.Entity<ProjectDesigner>()
                   .HasKey(pd => new { pd.ProjectId, pd.DesignerId });

            builder.Entity<Rate>()
                   .HasKey(r => new { r.AuthorId, r.RecipientId });

            builder.Entity<Offer>()
                   .Property(nameof(Offer.Price))
                   .HasColumnType("decimal");

            base.OnModelCreating(builder);

        }


    }
}
