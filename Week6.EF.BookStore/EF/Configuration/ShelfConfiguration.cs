using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.EF.Configuration
{
    class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            
            builder.ToTable("Shelves");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Code)
                                        .IsRequired().HasMaxLength(6);
        }
    }
}
