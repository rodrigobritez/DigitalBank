using DigitalBank.Domain.Entities;
using DigitalBank.Shared.Constants.Enums;
using FluentValidation;

namespace DigitalBank.Domain.Validators;

public class TransactionValidator : BaseEntityValidator<Transaction>
{
  public TransactionValidator()
  {
    void UpsertRuleSet()
    {
      RuleFor(x => x.Amount).NotEmpty().WithMessage("The amount of the transaction is required");
      RuleFor(x => x.Amount).GreaterThan(0).WithMessage("The amount of the transaction must be greater than 0");
      RuleFor(x => x.Type).NotEmpty().WithMessage("The type of the transaction is required");
      RuleFor(x => x.Id).NotEmpty().WithMessage("The id of the account is required");
    }

    AddBaseRuleCreate(UpsertRuleSet);
    AddBaseRuleUpdate(UpsertRuleSet);
  }
}