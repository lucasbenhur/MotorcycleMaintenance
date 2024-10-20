using System.Text.RegularExpressions;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex OldPlatePattern = new Regex(@"^[A-Z]{3}-\d{4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex MercosulPlatePattern = new Regex(@"^[A-Z]{3}\d[A-Z]\d{2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsPlate(this string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
            {
                return false;
            }

            return OldPlatePattern.IsMatch(plate) || MercosulPlatePattern.IsMatch(plate);

        }
    }
}
