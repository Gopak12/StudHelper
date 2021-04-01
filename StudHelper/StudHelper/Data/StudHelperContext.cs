using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudHelper.Models;

#nullable disable

namespace StudHelper.Data
{
    public partial class StudHelperContext : IdentityDbContext<User>
    {
        public StudHelperContext()
        {
        }

        public StudHelperContext(DbContextOptions<StudHelperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doc> Docs { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskSkill> TaskSkills { get; set; }
        public virtual DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Doc>(entity =>
            {
                entity.ToTable("docs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocPath)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("doc_path");

                entity.Property(e => e.TaskId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("task_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Docs)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_docs_tasks");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("offers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("employee_id");

                entity.Property(e => e.ProposedPrice)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("proposed_price");

                entity.Property(e => e.TaskId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("task_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_offers_AspNetUsers");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_offers_tasks");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skills");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("skill_name");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("employee_id");

                entity.Property(e => e.EmployeeRating).HasColumnName("employee_rating");

                entity.Property(e => e.EmployeeReviewDescr)
                    .HasColumnType("text")
                    .HasColumnName("employee_review_descr");

                entity.Property(e => e.EmployerId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("employer_id");

                entity.Property(e => e.EmployerRating).HasColumnName("employer_rating");

                entity.Property(e => e.EmployerReviewDescr)
                    .HasColumnType("text")
                    .HasColumnName("employer_review_descr");

                entity.Property(e => e.MaxPrice)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("max_price");

                entity.Property(e => e.MinPrice)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("min_price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaskEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_AspNetUsers");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.TaskEmployers)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tasks_AspNetUsers1");
            });

            modelBuilder.Entity<TaskSkill>(entity =>
            {
                entity.ToTable("task_skills");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SkillId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("skill_id");

                entity.Property(e => e.TaskId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("task_id");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.TaskSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_task_skills_skills");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskSkills)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_task_skills_tasks");
            });

            modelBuilder.Entity<UserSkill>(entity =>
            {
                entity.ToTable("user_skills");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SkillId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("skill_id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_skills_skills1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_skills_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
