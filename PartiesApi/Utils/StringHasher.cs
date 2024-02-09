using System.Security.Cryptography;
using System.Text;

namespace PartiesApi.Utils;

internal static class StringHasher
{
    public static string GetSha256Hash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);

        var stringBuilder = new StringBuilder();
        using (var hasher = SHA256.Create())
        {
            var resultBytes = hasher.ComputeHash(inputBytes);

            foreach (var resultByte in resultBytes)
                stringBuilder.Append(resultByte.ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}
