using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {        
        public Result(bool success,string message):this(success)//this class ın kendisi demek yani: "this(success) == Result(bool success)"
        {
            //Readonly ler sadece constructerda set edilebilir (sadece getteri var ise)
            Message = message;            
        }

        public Result(bool success)
        {
            //Readonly ler sadece constructerda set edilebilir (sadece getteri var ise)            
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
