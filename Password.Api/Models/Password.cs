using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Password.Api
{
    public static class Password
    {
        public static int MinLength { get; set; }
        public static int MaxLength { get; set; }
        public static int MinNumber { get; set; }
        public static int MinUpperCase { get; set; }
        public static int MinLowerCase { get; set; }
        public static int MinSymbol { get; set; }
        public static string CharSymbol { get; set; }
        public static bool RepeatChar { get; set; }

        public static bool Validate (string password, ILogger _logger) 
        {
            try {
                if (password is null || password == "") 
                {
                    _logger.LogWarning("Password: {password} - Missing ", password);
                    return false;
                }

                if (password.Length < MinLength) 
                {
                    _logger.LogWarning("Password: {password} - MinLength: {minlength} ", password, MinLength);
                    return false;
                }

                if (MaxLength > 0 && password.Length > MaxLength) 
                {
                    _logger.LogWarning("Password: {password} - MaxLength: {maxLength} ", password, MaxLength);
                    return false;
                }

                if (password.Contains(" ")) 
                {
                    _logger.LogWarning("Password: {password} - Must not contain spaces ", password);
                    return false;
                }

                if (!StringRegexMinValue(password, @"[0-9]+", MinNumber)) 
                {
                    _logger.LogWarning("Password: {password} - MinNumber: {minNumber} ", password, MinNumber);
                    return false;
                }

                if (!StringRegexMinValue(password, @"[A-Z]+", MinUpperCase)) 
                {
                    _logger.LogWarning("Password: {password} - MinUpperCase: {minUpperCase} ", password, MinUpperCase);
                    return false;
                }

                if (!StringRegexMinValue(password, @"[a-z]+", MinLowerCase)) 
                {
                    _logger.LogWarning("Password: {password} - MinLowerCase: {minLowerCase} ", password, MinLowerCase);
                    return false;
                }

                if (!StringRegexMinValue(password, @"[" + CharSymbol + "]+", MinSymbol)) 
                {
                    _logger.LogWarning("Password: {password} - MinSymbol: {minSymbol} - CharSymbol: {charSymbol} ", password, MinSymbol, CharSymbol);
                    return false;
                }

                if (!RepeatChar) 
                {
                    int index = 0;
                    foreach (var item in password) 
                    {
                        if (password.IndexOf(item) != index) {
                            _logger.LogWarning("Password: {password} - Must not contain repeated characters ", password);
                            return false;
                        }

                        index++;
                    }
                }

                return true;
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        private static bool StringRegexMinValue(string input, string pattern, int minValue) {
            if (minValue < 1) 
            {
                return true;
            }

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            if (matches.Count == 0) 
            {
                return false;
            } 

            foreach (Match match in matches)
            {
                if (match.Length < minValue) 
                {
                    return false;
                }
            }

            return true;
        }
    }
}
