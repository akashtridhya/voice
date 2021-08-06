using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using voice.api.Controllers;
using voice.logs;

namespace voice.api.Filters
{
    public class ExceptionFilters : ExceptionFilterAttribute
    {
        #region Object Declarations And Constructor

        protected ILogManager LogManager { get; set; }

        protected IStringLocalizer<BaseController> Localizer { get; }

        public ExceptionFilters(
            ILogManager LogManager,
            IStringLocalizer<BaseController> Localizer)
        {
            this.LogManager = LogManager;
            this.Localizer = Localizer;
        }

        #endregion Object Declarations And Constructor

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                var exception = context.Exception as ApiException;
                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, exception.Meta.statusCode);
            }
            else if (context.Exception is SqlException)
            {
                var exception = context.Exception as SqlException;

                var statusCode = 400;

                if (!string.IsNullOrWhiteSpace(exception.Message) && (exception.Message.Equals("error_access_token_expired") || exception.Message.Equals("sql_error_user_deactivated")))
                    statusCode = 401;

                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, statusCode);
            }
            else
            {
                var exception = context.Exception as Exception;
                LogManager.LogError(exception.Message, exception);
                context.Result = new CustomResultFilters(Localizer[exception.Message].Value, 500);
            }
        }
    }

    #region API Exception class declaration.

    public class ApiException : Exception
    {
        public dynamic Meta { get; set; }

        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; }

        public ApiException(string message, int statusCode = 400, IEnumerable<dynamic> Errors = null) : base(message)
        {
            var finalMessage = message.Split("; ");
            Meta = new { message = finalMessage, statusCode };
        }

        public ApiException(Exception ex, int statusCode = 400) : base(ex.Message)
        {
            Meta = new { StatusCode = statusCode };
        }

    }
    #endregion API Exception class declaration.
}