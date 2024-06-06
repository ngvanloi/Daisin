﻿using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            builder.HasData(new Portfolio
            {
                Id = 1,
                CategoryId = 1,
                FileName = "Test",
                FileType = "test",
                Title = "Test picture",
            },
            new Portfolio
            {
                Id = 2,
                CategoryId = 1,
                FileName = "Test 2",
                FileType = "test 2",
                Title = "Test picture 2",
            },
            new Portfolio
            {
                Id = 3,
                CategoryId = 2,
                FileName = "Test 3",
                FileType = "test 3",
                Title = "Test picture 3",
            },
            new Portfolio
            {
                Id = 4,
                CategoryId = 2,
                FileName = "Test 4",
                FileType = "test 4",
                Title = "Test picture 4",
            });
        }
    }
}
