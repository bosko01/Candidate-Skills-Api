using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class BussinessRuleValidationExeption : CustomException
    {
        public BussinessRuleValidationExeption() : base()
        {
        }
        public BussinessRuleValidationExeption(string message) : base(message)
        {
        }
        public BussinessRuleValidationExeption(string message, System.Exception exception) : base(message, exception)
        {
        }
    }
}
