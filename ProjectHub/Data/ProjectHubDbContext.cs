using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;

namespace ProjectHub.Data
{
    public class ProjectHubDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
       public DbSet<Activity> Activities { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<ProjectPosition> ProjectPositions { get; set; }

        public DbSet<Investor> Investors { get; set; }
        public DbSet<ProjectInvestor> ProjectInvestors { get; set; }

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ProjectContractor> ProjectContractors { get; set; }

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
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>()
            //       .HasDiscriminator<string>(au => au.UserType)
            //       .HasValue<Contractor>(nameof(Contractor))
            //       .HasValue<Designer>(nameof(Designer))
            //       .HasValue<Investor>(nameof(Investor))
            //       .HasValue<Manager>(nameof(Manager));

            builder.Entity<Rate>()
                   .HasOne(nameof(Rate.Recipient))
                   .WithMany(nameof(ApplicationUser.RatesReceived))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                   .HasOne(nameof(Rate.Recipient))
                   .WithMany(nameof(ApplicationUser.ReviewsReceived))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Offer>()
                   .HasOne(nameof(Offer.Project))
                   .WithMany(nameof(Project.Offers))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectContractor>()
                   .HasOne(nameof(ProjectContractor.Project))
                   .WithMany(nameof(Project.Contractors))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectInvestor>()
                   .HasOne(nameof(ProjectInvestor.Project))
                   .WithMany(nameof(Project.Investors))
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProjectDesigner>()
                   .HasOne(nameof(ProjectDesigner.Project))
                   .WithMany(nameof(Project.Designers))
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<UserProject>().HasKey(up => new { up.UserId, up.ProjectId });

            builder.Entity<UserDiscussion>()
                   .HasKey(ud => new { ud.UserId, ud.DiscussionId });

            builder.Entity<ProjectPosition>()
                   .HasKey(pp => new { pp.ProjectId, pp.PositionId });

            builder.Entity<ProjectInvestor>()
                   .HasKey(pi => new { pi.ProjectId, pi.InvestorId });

            builder.Entity<ProjectContractor>()
                   .HasKey(pc => new { pc.ProjectId, pc.ContractorId });

            builder.Entity<ProjectDesigner>()
                   .HasKey(pd => new { pd.ProjectId, pd.DesignerId });

            builder.Entity<Project>()
                   .Property(nameof(Project.Budget))
                   .HasColumnType("decimal");

            builder.Entity<Offer>()
                   .Property(nameof(Offer.Price))
                   .HasColumnType("decimal");
        }


    }
}
