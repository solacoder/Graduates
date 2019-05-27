using System;

namespace Graduates.Utility.Security
{
    public class PasswordGenerator
    {
        private static readonly string[] SmallAlphabets = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o"
                                                    , "p", "q", "r", "s", "t", "u","v", "w","x", "y", "z"};
        private static readonly string[] BigAlphabets = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"
                                                    , "P", "Q", "R", "S", "T", "U","V", "W","X", "Y", "Z"};
        private static readonly string[] SpecialAlphabets = { "!", "@", "#", "$", "%", "&", "?", "*", "~", "^" };

        private static string GetSmallAlphabet()
        {
            int position = new Random().Next(0, 25);
            return SmallAlphabets[position];
        }

        private static string GetBigAlphabet()
        {
            int position = new Random().Next(0, 25);
            return BigAlphabets[position];
        }

        private static string GetSpecialCharacter()
        {
            int position = new Random().Next(0, 9);
            return SpecialAlphabets[position];
        }

        private static string GenerateRandomNo()
        {
            Random _random = new Random();
            return _random.Next(0, 9999).ToString("D4");
        }

        public static string GeneratePassword()
        {
            return GenerateRandomNo() + GetBigAlphabet() + GetSmallAlphabet() + GetSpecialCharacter();
        }

        public static string GeneratePassword(string FirstName, string LastName)
        {
            string password = string.Empty;

            if(!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                password = FirstName.ToUpper().Substring(0, 1) + LastName.ToUpper().Substring(0, 1) + GeneratePassword();
            }
            return password;
        }
    }
}
