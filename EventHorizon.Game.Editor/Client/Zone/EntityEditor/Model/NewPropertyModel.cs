namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Model;

public class NewPropertyModel
{
    public static string FIRST_CHARACTER_IS_NOT_LETTER { get; } =
        "first_character_is_not_letter";
    public static string FIRST_CHARACTER_IS_UPPERCASE { get; } =
        "first_character_is_uppercase";
    public static string LENGTH_ZERO { get; } = "length_zero";

    public bool IsValid { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public (bool, string) Validate()
    {
        (bool isValid, string erroCode) result = (true, string.Empty);

        // Has to be greater than zero
        if (Name.Length == 0)
        {
            result = (false, LENGTH_ZERO);
        }
        // Not an upper case letter
        else if (char.IsUpper(Name[0]))
        {
            result = (false, FIRST_CHARACTER_IS_UPPERCASE);
        }
        // Must be a letter
        else if (!char.IsLetter(Name[0]))
        {
            result = (false, FIRST_CHARACTER_IS_NOT_LETTER);
        }

        IsValid = result.isValid;
        ErrorMessage = result.erroCode;

        return result;
    }
}
