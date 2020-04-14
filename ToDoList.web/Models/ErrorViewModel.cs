using System;
using ToDoList.web.Models.Enums;

namespace ToDoList.web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorCode { get; set; }

        public bool ShowErrorCode => !(ErrorCode.Equals(0));

        public string ErrorText { get; set; }

        public bool ShowErrorText => !string.IsNullOrEmpty(ErrorText);

        public ErrorStatus ErrorStatus { get; set; } = ErrorStatus.info;




        public ErrorViewModel()
        {
            ErrorCode = 404;
            ErrorText = "The Page can't be found";
        }

        public ErrorViewModel(string requestId)
        {
            RequestId = requestId;
        }

        public ErrorViewModel(int errorCode, string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }

        public ErrorViewModel(string requestId, ErrorStatus errorStatus) : this (requestId)
        {
            ErrorStatus = errorStatus;
        }  

        public ErrorViewModel(string requestId, int errorCode, string errorText) : this(errorCode, errorText)
        {
            RequestId = requestId;
        }

        public ErrorViewModel(int errorCode, string errorText, ErrorStatus errorStatus) : this(errorCode, errorText)
        {
            ErrorStatus = errorStatus;
        } 
        
        public ErrorViewModel(string requestId, int errorCode, string errorText, ErrorStatus errorStatus) : this(errorCode, errorText, errorStatus)
        {
            RequestId = requestId;
        }
    }
}