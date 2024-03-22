namespace Application.Validation.Passwords;

public static class PasswordRules
{
    public const string ValidPasswordRegex = "^(?=.*[a-zA-Z])(?=.*[0-9]).*$";
    public const int MinLength = 5;
}
