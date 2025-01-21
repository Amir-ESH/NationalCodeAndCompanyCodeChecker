namespace NationalCodeAndCompanyCodeChecker.Validator;

public static class CompanyNationalCodeValidator
{
    // This is the coefficients of the company national code
    private static readonly int[] Coefficients = { 29, 27, 23, 19, 17, 29, 27, 23, 19, 17 };

    /// <summary>
    /// Verifying the accuracy of the National ID number for the company (11 digits).
    /// </summary>
    /// <param name="nationalCode">Company code</param>
    /// <returns>return true or false</returns>
    public static bool IsCompanyNationalCodeValid(this string? nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode))
        {
            return true;
        }

        nationalCode = nationalCode.Trim();

        // The National ID number for the company must consist of exactly 11 digits and be entirely numerical.
        if (nationalCode.Length != 11 || !long.TryParse(nationalCode, out _))
        {
            return false;
        }

        // If the entire number consists of zeros, it is considered invalid.
        if (nationalCode == "00000000000")
        {
            return false;
        }

        // The tens digit is used in the addition process with 2.
        // According to the explanation: Digit at index=9 => tensDigit
        var tensDigit = nationalCode[9] - '0';

        // According to the algorithm, the value of the tens digit is added to 2.
        var d = tensDigit + 2;

        // Calculating the sum of the products.
        var sum = 0;
        for (var i = 0; i < 10; i++)
        {
            // Each digit from the first 10 digits.
            var digit = nationalCode[i] - '0';
            // Adding d (tensDigit + 2) and multiplying by the corresponding coefficient.
            sum += (digit + d) * Coefficients[i];
        }

        // Calculating the remainder when divided by 11.
        var remainder = sum % 11;

        // If the remainder is 10, according to the instructions, it should be considered as 0.
        if (remainder == 10)
            remainder = 0;

        // The control digit refers to the last digit.
        var checkDigit = nationalCode[10] - '0';

        // Comparing with the control digit.
        return checkDigit == remainder;
    }
}