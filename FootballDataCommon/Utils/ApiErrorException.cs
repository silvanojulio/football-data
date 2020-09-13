using System;

namespace FootballDataCommon.Utils
{
    public class ApiErrorException : Exception{
        public string message{get;set;}
        public int errorCode{get;set;}
    }
}
