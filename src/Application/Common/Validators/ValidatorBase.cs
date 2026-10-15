namespace ViloesApi.Application.Common.Validators;

public abstract class ValidatorBase<T>(IUnitOfWork unitOfWork) : AbstractValidator<T>
{
    protected IUnitOfWork UnitOfWork { get; } = unitOfWork;
}
