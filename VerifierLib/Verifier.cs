using System.Text;
using System.Text.RegularExpressions;

namespace CaferLib.VerifierLib;

public class Verifier
{
    public static Result IsValidEmailFormat(string? email)
    {
        Result result = new ();
        if (string.IsNullOrWhiteSpace(email))
        {
            result.Errors.Add("Email cannot be null or empty.");
        }

        if (email is not null)
        {
            var emailRegex = @"^[^@\s]+(\.[^@\s]+)*[^.\s]@[^@\s]+\.[^@\s]+$";
            var domainPart = email.Split('@').Last();

            if (!Regex.IsMatch(email, emailRegex) || domainPart.Contains(".."))
            {
                result.Errors.Add("Email format is invalid.");
            }
        }

        if (result.Errors.Any())
        {
            result.IsValid = false;
        }
        return result;
    }
    
    public static Result IsValidPasswordFormat(string password,
        int minLength, int maxLength, bool requireUppercase, bool requireLowercase, bool requireNumber, bool requireSpecialCharacter)
    {
        Result result = new ();
        if (string.IsNullOrWhiteSpace(password))
        {
            result.IsValid = false;
            result.Errors.Add("Password cannot be null or empty.");
        }
        
        if (password.Length < minLength || password.Length > maxLength)
        {
            result.Errors.Add($"Password must be between {minLength} and {maxLength} characters.");
        }

        var hasUppercase = new Regex(@"[A-Z]");
        var hasLowercase = new Regex(@"[a-z]");
        var hasNumber = new Regex(@"\d");
        var hasSpecialCharacter = new Regex(@"[\W_]");

        if (!hasUppercase.IsMatch(password) && requireUppercase)
        {
            result.Errors.Add("Password must contain at least one uppercase letter.");
        }
        
        if (!hasLowercase.IsMatch(password) && requireLowercase)
        {
            result.Errors.Add("Password must contain at least one lowercase letter.");
        }
        
        if (!hasNumber.IsMatch(password) && requireNumber)
        {
            result.Errors.Add("Password must contain at least one numeric digit.");
        }
        if (!hasSpecialCharacter.IsMatch(password) && requireSpecialCharacter)
        {
            result.Errors.Add("Password must contain at least one special character.");
        }

        if (result.Errors.Any())
        {
            result.IsValid = false;
        }

        return result;
    }
}