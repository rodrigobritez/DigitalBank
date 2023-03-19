using DigitalBank.Domain.Entities;
using FluentValidation;

namespace DigitalBank.Domain.Validators;

public class AccountValidator : BaseEntityValidator<Account>
{
  public AccountValidator()
  {
    void CreateRuleSet()
    {
      RuleFor(x => x.Name).NotEmpty().WithMessage("The name of the account is required");
      RuleFor(x => x.Name).MaximumLength(50).WithMessage(
        "The name of the account must be less than 50 characters");
      RuleFor(x => x.Name).MinimumLength(3).WithMessage(
        "The name of the account must be greater than 3");
      RuleFor(x => x.Id).NotEmpty().WithMessage("The id of the account is required");
      RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("The account number is required");
      RuleFor(x => x.AccountNumber).LessThan(99999);
      RuleFor(x => x.AccountNumber).GreaterThan(10000);
      RuleFor(x => x.DocumentNumber).Length(11).WithMessage("The document number must be 11 characters");
      RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage("The document number is required");
    }

    void UpdateRuleSet()
    {
      RuleFor(x => x.Name).MaximumLength(50).WithMessage(
        "The name of the account must be less than 50 characters");
      RuleFor(x => x.Name).MinimumLength(3).WithMessage(
        "The name of the account must be greater than 3");
      RuleFor(x => x.DocumentNumber).Length(11).WithMessage("The document number must be 11 characters");
      RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("The account number is required");
      RuleFor(x => x.AccountNumber).LessThan(99999);
      RuleFor(x => x.AccountNumber).GreaterThan(10000);
      RuleFor(x => x.Id).NotEmpty().WithMessage("The id of the account is required");
    }

    AddBaseRuleCreate(CreateRuleSet);
    AddBaseRuleUpdate(UpdateRuleSet);
  }
}