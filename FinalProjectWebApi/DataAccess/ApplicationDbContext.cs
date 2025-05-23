﻿using Microsoft.EntityFrameworkCore;
using FinalProjectWebApi.Entities.Concrete;
using FinalProjectWebApi.DataAccess.Configurations;

namespace FinalProjectWebApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<ResearchRequirement> ResearchRequirements { get; set; }
        public DbSet<Views> Views { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ParticipantInfo> ParticipantInfos { get; set; }
        public DbSet<TemporaryUser> TemporaryUsers { get; set; }
        





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tüm yapılandırmaları yükle
            modelBuilder.ApplyConfiguration(new ViewsConfiguration());
            modelBuilder.ApplyConfiguration(new ResearchRequirementConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ResearchConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new OptionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantInfoConfiguration());
            modelBuilder.ApplyConfiguration(new TemporaryUserConfiguration());
    



        }
    }

    
}

    



