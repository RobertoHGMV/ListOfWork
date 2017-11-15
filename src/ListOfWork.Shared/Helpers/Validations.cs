using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfWork.Shared.Helpers
{
    public class Validations
    {
        public static bool IsEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsGreaterThanZero(decimal value)
        {
            return value > decimal.Zero;
        }

        public static bool ValidDate(DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }
    }
}
