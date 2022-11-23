using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace QuizAPiService.Entities
{
    public partial class QuizContext : DbContext
    {
        public QuizContext()
        {
        }

        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryQuestion> CategoryQuestions { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Questiondetail> Questiondetails { get; set; } = null!;
        public virtual DbSet<Quizdetail> Quizdetails { get; set; } = null!;
        public virtual DbSet<Roledetail> Roledetails { get; set; } = null!;
        public virtual DbSet<Tenantcompany> Tenantcompanies { get; set; } = null!;
        public virtual DbSet<Tenantmaster> Tenantmasters { get; set; } = null!;
        public virtual DbSet<Userdetail> Userdetails { get; set; } = null!;
        public virtual DbSet<Userrole> Userroles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("data source=localhost;port=3306;user id=root;password=Vky9849@;initial catalog=quiz", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<CategoryQuestion>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("category_question");

                entity.Property(e => e.CategoryType).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("level");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LevelType).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Questiondetail>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("questiondetails");

                entity.HasIndex(e => e.LevelId, "FK_QuestionDetails_Level");

                entity.HasIndex(e => e.CategoryId, "FK_QuestionDetails_category_Question");

                entity.Property(e => e.CorrectOption).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OptionA).HasMaxLength(500);

                entity.Property(e => e.OptionB).HasMaxLength(500);

                entity.Property(e => e.OptionC).HasMaxLength(500);

                entity.Property(e => e.OptionD).HasMaxLength(500);

                entity.Property(e => e.QuestionDescription).HasMaxLength(500);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questiondetails)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionDetails_category_Question");

                entity.HasOne(d => d.Level)
                   .WithMany(p => p.Questiondetails)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionDetails_Level");
            });

            modelBuilder.Entity<Quizdetail>(entity =>
            {
                entity.HasKey(e => e.QuizId)
                    .HasName("PRIMARY");

                entity.ToTable("quizdetails");

                entity.HasIndex(e => e.LevelId, "FK_QuizDetails_Level");

                entity.HasIndex(e => e.CategoryId, "FK_QuizDetails_category_Question");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpiresOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Quizdetails)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizDetails_category_Question");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Quizdetails)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizDetails_Level");
            });

            modelBuilder.Entity<Roledetail>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("roledetails");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleDescription).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<Tenantcompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.ToTable("tenantcompanies");

                entity.HasIndex(e => e.TenantId, "FK_TenantCompanies_TenantMaster");

                entity.Property(e => e.Address1).HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.Address3).HasMaxLength(100);

                entity.Property(e => e.CompanyCode).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Esireg)
                    .HasMaxLength(50)
                    .HasColumnName("ESIReg");

                entity.Property(e => e.MobileNumber).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Pannumber)
                    .HasMaxLength(20)
                    .HasColumnName("PANNumber");

                entity.Property(e => e.Pfreg)
                    .HasMaxLength(50)
                    .HasColumnName("PFReg");

                entity.Property(e => e.Pincode).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.SysEndTime).HasColumnType("datetime");

                entity.Property(e => e.SysStartTime).HasColumnType("datetime");

                entity.Property(e => e.Tannumber)
                    .HasMaxLength(20)
                    .HasColumnName("TANNumber");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Tenantcompanies)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenantCompanies_TenantMaster");
            });

            modelBuilder.Entity<Tenantmaster>(entity =>
            {
                entity.HasKey(e => e.TenantId)
                    .HasName("PRIMARY");

                entity.ToTable("tenantmaster");

                entity.Property(e => e.Address1).HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.Address3).HasMaxLength(100);

                entity.Property(e => e.CompanyLo)
                    .HasMaxLength(100)
                    .HasColumnName("CompanyLo;");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Esireg)
                    .HasMaxLength(50)
                    .HasColumnName("ESIReg");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Pannumber)
                    .HasMaxLength(20)
                    .HasColumnName("PANNumber");

                entity.Property(e => e.Pfreg)
                    .HasMaxLength(50)
                    .HasColumnName("PFReg");

                entity.Property(e => e.Pincode).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.Property(e => e.Tannumber)
                    .HasMaxLength(20)
                    .HasColumnName("TANNumber");

                entity.Property(e => e.TenantName).HasMaxLength(200);
            });

            modelBuilder.Entity<Userdetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("userdetails");

                entity.HasIndex(e => e.CompanyId, "FK_UserDetails_TenantCompanies");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EmployeeName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Userdetails)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_UserDetails_TenantCompanies");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userroles");

                entity.HasIndex(e => e.RoleId, "FK_UserRoles_RoleDetails");

                entity.HasIndex(e => e.CompanyId, "FK_UserRoles_TenantCompanies");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_TenantCompanies");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userroles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_RoleDetails");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
