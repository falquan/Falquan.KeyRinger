using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Falquan.KeyRinger.Services
{
    public static class StringGenerator
    {
        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=_+[]\\{}|;':\",./<>?";
        private const int _rangeSize = 0x100;

        private static readonly RandomNumberGenerator rng = RNGCryptoServiceProvider.Create();

        /// <summary>
        /// Generate a cryptographically secure random string of characters
        /// </summary>
        /// <param name="length">Length of string</param>
        /// <param name="allowedCharacters">Characters allowed in string</param>
        /// <returns></returns>
        public static string GetRandomString(int length, string allowedCharacters = AllowedCharacters)
        {
            var characterSet = GetCharacterSet(length, allowedCharacters);

            var sb = new StringBuilder();
            var buffer = new byte[128];
            while (sb.Length < length)
            {
                rng.GetBytes(buffer);
                for (var i = 0; i < buffer.Length && sb.Length < length; ++i)
                {
                    var allowedCharacterCeilingValue = _rangeSize - (_rangeSize % characterSet.Length);
                    if (allowedCharacterCeilingValue <= buffer[i]) continue;
                    sb.Append(characterSet[buffer[i] % characterSet.Length]);
                }
            }
            return sb.ToString();
        }

        private static char[] GetCharacterSet(int length, string allowedCharacters)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrWhiteSpace(allowedCharacters)) throw new ArgumentException("allowedCharacters cannot be empty.");
            var allowedCharSet = allowedCharacters.ToCharArray();
            if (_rangeSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", _rangeSize));
            return allowedCharSet;
        }
    }
}
