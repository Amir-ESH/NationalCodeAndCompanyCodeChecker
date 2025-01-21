// This code validates either an Iranian national code or a company national code 
// based on the length of the input code. It checks if the code is valid and outputs the result.

// Importing the validator for national codes.
using NationalCodeAndCompanyCodeChecker.Validator;

Console.WriteLine("Press 'Esc' to exit the program.");

Console.WriteLine("Enter your code: "); // Prompt the user to enter a code.
var code = Console.ReadLine(); // Read the input code from the user.

// Check if the input code is null or empty.
if (string.IsNullOrWhiteSpace(code))
{
    // Inform the user that the code is invalid.
    Console.WriteLine("Code can't be null or empty");

    return; // Exit the program if the code is invalid.
}

// Determine if the code is valid based on its length.
var isValid = code.Length switch
{
    11 => code.IsCompanyNationalCodeValid(), // If the code is 11 characters long, validate as a company national code.
    10 => code.IsIranianNationalCodeValid(), // If the code is 10 characters long, validate as an Iranian national code.
    _ => false // If the code length is neither 10 nor 11, it's invalid.
};

// Output the validation result.
Console.WriteLine($"The code ' {code} ' is {(isValid ? "VALID" : "NOT VALID")}"); // Inform the user whether the code is valid or not.
