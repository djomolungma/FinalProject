using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)//base veri göndermek için :base()
        {

        }
        public SuccessResult() : base(true)
        {

        }
    }
}
