using System.Text.RegularExpressions;

namespace NationalCodeAndCompanyCodeChecker.Validator;

public static class PersonNationalCodeValidator
{
    /// <summary>
    /// This code validates a national ID number, ensuring it is a 10-digit number
    /// that does not contain repeating digits and checks the validity of a control number
    /// based on a specific algorithm.
    /// </summary>
    /// <param name="nationalId">National code</param>
    /// <returns>return true or false</returns>
    public static bool IsIranianNationalCodeValid(this string nationalId)
    {
        // Determining the pattern of the national number (10 digits and not repeating the same number)
        const string pattern = @"^\d{10}$";

        // Use regular expression for validation
        if (!Regex.IsMatch(nationalId, pattern)) return false;

        // Check for non-duplication of numbers
        for (var i = 0; i < 10; i++)
        {
            if (Regex.IsMatch(nationalId, "^" + i + "{10}$"))
            {
                return false;
            }
        }

        // Extract the check digit (the last digit) from the national ID.
        var check = Convert.ToInt32(nationalId.Substring(9, 1)); // Get the 10th digit.
        var sum = 0; // Initialize sum for weighted calculation.

        // Calculate the weighted sum of the first 9 digits.
        for (var i = 0; i < 9; i++)
        {
            // Each digit is multiplied by a decreasing weight from 10 to 2.
            sum += Convert.ToInt32(nationalId.Substring(i, 1)) * (10 - i);
        }

        //Calculate the remainder of the weighted sum when divided by 11.
        var remainder = sum % 11;

        // Checking the correctness of the control number
        return (remainder < 2 && check == remainder) || (remainder >= 2 && check == 11 - remainder);
    }
}