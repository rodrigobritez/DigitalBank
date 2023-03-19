using System.Linq.Expressions;
using DigitalBank.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalBank.Data.Mappings.Base;

public abstract class BaseEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
  public virtual void Configure(EntityTypeBuilder<TEntity> builder)
  {
    ApplyDefaultConfiguration(builder);
  }

  protected void ApplyDefaultConfiguration(EntityTypeBuilder<TEntity> builder)
  {
    builder.HasKey(t => t.Id);
    builder.HasQueryFilter(t => !t.Deleted);
  }
}