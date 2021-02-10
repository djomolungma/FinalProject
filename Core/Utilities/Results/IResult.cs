using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç
    //Sadece işlem sonucu ve mesajı döndürecek
    public interface IResult
    {
        bool Success { get; }//Sadece okunabilir
        string Message { get; }
    }
}
