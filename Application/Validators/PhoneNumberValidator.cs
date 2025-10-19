using Exceptions;
using PhoneNumbers;


namespace Application.Validators
{
    public static class PhoneHelper
    {
        public static bool IsValid(string? input, string defaultRegion = "RS")
        {
            
            if (string.IsNullOrWhiteSpace(input)) return false;
            try
            {
                var util = PhoneNumberUtil.GetInstance();
                var num = util.Parse(input, defaultRegion);
                return util.IsValidNumber(num);
            }
            catch { return false; }
        }

        public static string? NormalizeToE164(string? input, string defaultRegion = "RS")
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            var util = PhoneNumberUtil.GetInstance();
            var num = util.Parse(input, defaultRegion);
            if (!util.IsValidNumber(num)) return null;
            return util.Format(num, PhoneNumberFormat.E164); 
        }

        
    }
}
