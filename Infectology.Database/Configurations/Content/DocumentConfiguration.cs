using Infectology.Library.Entities.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infectology.Database.Configurations.Content;

internal class DocumentConfiguration
    : IEntityTypeConfiguration<Document>
{
    public void Configure(
        EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired();
    }
}