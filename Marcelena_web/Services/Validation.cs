using System.Text.RegularExpressions;

namespace Marcelena_web.Services
{
    public class Validation
    {
        public static bool isNumberValid(string number)
        {
            Regex rgx = new Regex("^[0-9]+([\\.,][0-9][0-9]?)?$");
            return rgx.IsMatch(number);
        }
    }
}
