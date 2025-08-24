namespace CaferLib.VerifierLib;

/// <summary>
/// Represents the result of a validation or operation.
/// </summary>
/// <remarks>
/// If <see cref="IsValid"/> is <c>false</c>, then <see cref="Errors"/> will not be <c>null</c>
/// and should contain at least one error message. If <see cref="IsValid"/> is <c>true</c>, <see cref="Errors"/> is empty.
/// </remarks>
public class Result
{
    /// <summary>
    /// Indicates whether the operation was successful or valid.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// A list of errors describing why the operation was not valid.
    /// Will not be <c>null</c> if <see cref="IsValid"/> is <c>false</c>.
    /// </summary>
    public List<string> Errors { get; set; }

    /// <summary>
    /// Creates a successful result with no errors.
    /// </summary>
    public Result()
    {
        IsValid = true;
        Errors = new List<string>();
    }
}