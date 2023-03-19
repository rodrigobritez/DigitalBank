using DigitalBank.Domain.Entities.Base;
using FluentValidation;

namespace DigitalBank.Domain.Validators;

public abstract class BaseEntityValidator<TEntity> : AbstractValidator<TEntity>
  where TEntity : BaseEntity
{
  protected void AddBaseRuleCreate(Action action)
  {
    RuleSet(BaseEntityValidations.CREATE, action);
  }

  protected void AddBaseRuleUpdate(Action action)
  {
    RuleSet(BaseEntityValidations.UPDATE, action);
  }
}

public abstract class BaseEntityValidations
{
  public static readonly string CREATE = "CREATE";
  public static readonly string UPDATE = "UPDATE";
}