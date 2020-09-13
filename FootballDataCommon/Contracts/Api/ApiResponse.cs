using System;

namespace FootballDataCommon.Contracts.Api
{
    public class ApiResponse<T>
    {
        public ApiError Error{ get; set;}
        public bool IsSuccessful {get;set;}
        public T Data{get; set;}
    }

    public class ApiError
    {
        public int Code {get;set;}
        public string Message {get;set;}
    }
}