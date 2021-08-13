using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Data.Models.Users;

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
                   .HasMany(u => u.RatesReceived)
                   .WithOne(r => r.Recipient)
                   .HasForeignKey(r => r.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                   .HasMany(u => u.RatesSent)
                   .WithOne(r => r.Author)
                   .HasForeignKey(r => r.AuthorId);

            builder.Entity<ApplicationUser>()
                   .HasMany(u => u.ReviewsReceived)
                   .WithOne(r => r.Recipient)
                   .HasForeignKey(r => r.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                   .HasMany(u => u.ReviewsSent)
                   .WithOne(r => r.Author)
                   .HasForeignKey(r => r.AuthorId);

            builder.Entity<Project>()
                   .HasOne(p => p.Investor)
                   .WithMany(i => i.Projects)
                   .HasForeignKey(p => p.InvestorId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Project>()
                   .HasOne(p => p.Manager)
                   .WithMany(i => i.Projects)
                   .HasForeignKey(p => p.ManagerId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Project>()
                   .HasOne(p => p.Contractor)
                   .WithMany(i => i.Projects)
                   .HasForeignKey(p => p.ContractorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ProjectDesigner>()
                   .HasOne(d => d.Designer)
                   .WithMany(pd => pd.Projects)
                   .HasForeignKey(pd => pd.DesignerId);

            builder.Entity<ProjectDesigner>()
                   .HasOne(pd => pd.Project)
                   .WithMany(p => p.Designers)
                   .HasForeignKey(pd => pd.ProjectId);

            builder.Entity<Offer>()
                   .HasOne(offer => offer.Project)
                   .WithMany(p => p.Offers)
                   .HasForeignKey(offer => offer.ProjectId)
                   .OnDelete(DeleteBehavior.ClientCascade);            

            builder.Entity<UserDiscussion>()
                   .HasKey(ud => new { ud.UserId, ud.DiscussionId });

            builder.Entity<ProjectDesigner>()
                   .HasKey(pd => new { pd.ProjectId, pd.DesignerId });

            builder.Entity<Rate>()
                   .HasKey(r => new { r.AuthorId, r.RecipientId });

            builder.Entity<Review>()
                   .HasKey(r => new { r.AuthorId, r.RecipientId });

            builder.Entity<Offer>()
                   .HasKey(offer => new { offer.AuthorId, offer.ProjectId });

            builder.Entity<Offer>()
                   .Property(nameof(Offer.Price))
                   .HasColumnType("decimal");

            base.OnModelCreating(builder);

        }


    }
}
