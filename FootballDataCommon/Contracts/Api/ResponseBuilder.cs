using System;

namespace FootballDataCommon.Contracts.Api
{
    public class ResponseBuilder
    {
        public static ApiResponse<Object> GetApiSuccessResponse(){
            return new ApiResponse<Object>{
                Error = null,
                IsSuccessful = true
            };
        }
        public static ApiResponse<R> GetApiSuccessResponseWithData<R>(R data){
            return new ApiResponse<R>{
                Data= data,
                Error = null,
                IsSuccessful = true
            };
        }

        public static ApiResponse<Exception> GetErrorApiResponse(Exception ex){
            return new ApiResponse<Exception>{
                Error = new ApiError{
                    Code = ex.HResult,
                    Message = ex.Message
                },
                IsSuccessful = false
            };
        }
    }
}