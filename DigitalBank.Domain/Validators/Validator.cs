using DigitalBank.Domain.Entities.Base;
using FluentValidation;
using FluentValidation.Results;

namespace DigitalBank.Domain.Validators;

public static class Validator
{
  public static async Task<ValidationResult> ValidateAsync<TValidator, TEntity>(
    TEntity instance,
    string ruleSet,
    CancellationToken cancellationToken)
    where TValidator : BaseEntityValidator<TEntity>, new()
    where TEntity : BaseEntity
  {
    return await ValidateAsync<TValidator, TEntity>(instance, new[] { ruleSet }, cancellationToken);
  }
  
  public static async Task<ValidationResult> ValidateAsync<TValidator, TEntity>(
    TEntity instance,
    IEnumerable<string> ruleSets,
    CancellationToken cancellationToken)
    where TValidator : BaseEntityValidator<TEntity>, new()
    where TEntity : BaseEntity
  {
    var validator = new TValidator();
    return await validator.ValidateAsync(instance, options => options.IncludeRuleSets(ruleSets.ToArray()), cancellationToken);
  }
}