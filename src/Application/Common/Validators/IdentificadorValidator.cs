using FluentValidation.Validators;

namespace ViloesApi.Application.Common.Validators;

public class IdentificadorValidator<T> : PropertyValidator<T, int>
{
    public override string Name => "IdentificadorValidator";

    public override bool IsValid(ValidationContext<T> context, int value) => value > 0;

    protected override string GetDefaultMessageTemplate(string errorCode) => "Valor do identificador inválido.";
}
