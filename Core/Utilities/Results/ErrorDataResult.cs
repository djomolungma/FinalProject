using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErrorDataResult(T data) : base(data, false)
        {

        }

        public ErrorDataResult(string message) : base(default, false, message)//birşey döndürmek istemiyorum default döndür //örn return tipi int ise int döndürür
        {

        }
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
