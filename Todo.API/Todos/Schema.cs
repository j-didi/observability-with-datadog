using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.API.Todos;

public class Schema: IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Description).HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.Done).HasDefaultValue(false).IsRequired();
    }
}