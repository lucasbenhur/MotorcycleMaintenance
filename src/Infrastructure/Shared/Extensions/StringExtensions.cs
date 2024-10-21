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

        public static bool IsValidCnpj(this string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            cnpj = Regex.Replace(cnpj, @"\D", "");
            if (cnpj.Length != 14) return false;

            string[] invalidCnpjs = {
                "00000000000000", "11111111111111", "22222222222222",
                "33333333333333", "44444444444444", "55555555555555",
                "66666666666666", "77777777777777", "88888888888888",
                "99999999999999"
            };

            if (invalidCnpjs.Contains(cnpj)) return false;

            // Cálculo dos dígitos verificadores
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpjSemDigitos = cnpj.Substring(0, 12);
            int primeiroDigito = CalcularDigito(cnpjSemDigitos, multiplicadores1);
            int segundoDigito = CalcularDigito(cnpjSemDigitos + primeiroDigito, multiplicadores2);

            string cnpjCalculado = cnpjSemDigitos + primeiroDigito + segundoDigito;
            return cnpj == cnpjCalculado;
        }

        private static int CalcularDigito(string cnpjParcial, int[] multiplicadores)
        {
            int soma = 0;
            for (int i = 0; i < multiplicadores.Length; i++)
            {
                soma += (cnpjParcial[i] - '0') * multiplicadores[i];
            }

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
