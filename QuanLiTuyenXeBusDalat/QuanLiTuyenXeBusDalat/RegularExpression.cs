using System.Text.RegularExpressions;

namespace QuanLiTuyenXeBusDalat
{
    public class RegularExpression
    {
        public bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public bool IsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;
            return Regex.IsMatch(phone, @"(84|0[3|5|7|8|9])+([0-9]{8})\b");
        }

        public bool IsDate(string date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            return Regex.IsMatch(date, @"\b(0?[1-9]|[12]\d|3[01])[\/\-.](0?[1-9]|[12]\d|3[01])[\/\-.](\d{2}|\d{4})\b");
        }

        public bool IsCCCD(string cccd)
        {
            if (string.IsNullOrEmpty(cccd)) return false;
            return Regex.IsMatch(cccd, @"^[0]{1}[0-9]{1,2}[0-9]{1}[0-9]{1,2}[0-9]{1,6}$");
        }
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }
        public PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length >= 1 && password.Length < 8)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"(?=.*[a-z])", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"(?=.*[A-Z])(?=.*[0-9])", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"(?=.[!@#$%^&])", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }
    }
}
