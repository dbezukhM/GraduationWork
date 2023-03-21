using BLL.Contracts;

namespace BLL.Services
{

    public class PasswordService : IPasswordService
    {
        private enum PasswordCharacters
        {
            UpperCaseLetters = 0,
            LowerCaseLetters = 1,
            Numbers = 2,
            NonAlphanumeric = 3,
        }

        private readonly Dictionary<PasswordCharacters, string> _chars = new Dictionary<PasswordCharacters, string>
        {
            { PasswordCharacters.UpperCaseLetters, "ABCDEFGHJKLMNOPQRSTUVWXYZ" },
            { PasswordCharacters.LowerCaseLetters, "abcdefghijkmnopqrstuvwxyz" },
            { PasswordCharacters.Numbers, "0123456789" },
            { PasswordCharacters.NonAlphanumeric, "#$%^&" }
        };

        private readonly Random _rand = new Random(Environment.TickCount);

        public string GeneratePassword(int requiredLength = 8)
        {
            var password = new List<char>
            {
                _chars[PasswordCharacters.UpperCaseLetters][_rand.Next(0, _chars[PasswordCharacters.UpperCaseLetters].Length)],
            };

            password.Insert(
                _rand.Next(0, password.Count),
                _chars[PasswordCharacters.LowerCaseLetters][_rand.Next(0, _chars[PasswordCharacters.LowerCaseLetters].Length)]);

            password.Insert(
                _rand.Next(0, password.Count),
                _chars[PasswordCharacters.Numbers][_rand.Next(0, _chars[PasswordCharacters.Numbers].Length)]);

            password.Insert(
                _rand.Next(0, password.Count),
                _chars[PasswordCharacters.NonAlphanumeric][_rand.Next(0, _chars[PasswordCharacters.NonAlphanumeric].Length)]);

            for (var i = password.Count; i < requiredLength; i++)
            {
                var randomCharacterType = _chars[(PasswordCharacters)_rand.Next(0, _chars.Count)];

                password.Insert(
                    _rand.Next(0, password.Count),
                    randomCharacterType[_rand.Next(0, randomCharacterType.Length)]);
            }

            return new string(password.ToArray());
        }
    }
}