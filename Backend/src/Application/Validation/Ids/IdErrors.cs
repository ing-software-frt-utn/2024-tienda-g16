using ErrorOr;

namespace Application.Validation.Ids;

public static class IdErrors
{
    public static Error IdInvalido => Error.Validation(
        "Id.Invalido",
        "El Id no es valido");
}
