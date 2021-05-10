using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SportServiceApp.Filters
{
    public class ValidationFilter : IResultFilter// //IResourceFilter, IActionFilter //: IAsyncActionFilter
    {


        private ILogger _logger;
        public ValidationFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValidationFilter>();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(
                headerName, new string[] { "ResultExecutingSuccessfully" });
            _logger.LogInformation("Header added: {HeaderName}", headerName);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Can't add to headers here because response has started.
            _logger.LogInformation("AddHeaderResultServiceFilter.OnResultExecuted");
        }
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // Do something before the action executes.
        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //       logger.Info(MethodBase.GetCurrentMethod().ToString(), context.HttpContext.Request.Path);
        //}

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    // Do something after the action executes.
        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //       logger.Info(MethodBase.GetCurrentMethod().ToString(), context.HttpContext.Request.Path);
        //}
        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    logger.Info($"{context.HttpContext.Request.Headers?.ToString()}");
        //    //if (!context.ModelState.IsValid)
        //    //{
        //    //    var errorsInModelState = context.ModelState
        //    //        .Where(x => x.Value.Errors.Count > 0)
        //    //        .ToDictionary(pair => pair.Key, pair => pair.Value.Errors.Select(x => x.ErrorMessage))
        //    //        .ToArray();

        //    //    var errorResponse = new ErrorResponse();

        //    //    foreach (var (key, value) in errorsInModelState)
        //    //    {
        //    //        foreach (var subError in value)
        //    //        {
        //    //            errorResponse.Errors.Add(new ErrorModel
        //    //            {
        //    //                FieldName = key,
        //    //                Message = subError
        //    //            });
        //    //        }
        //    //    }

        //    //    context.Result = new BadRequestObjectResult(errorResponse);
        //    //    return;
        //    //}
        //    await next();
        //}


        //void IResourceFilter.OnResourceExecuted(ResourceExecutedContext context)
        //{

        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    logger.Info($"{context.HttpContext.Request.RouteValues?.ToString()}");
        //}

        //void IResourceFilter.OnResourceExecuting(ResourceExecutingContext context)
        //{

        //    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //    logger.Info($"{context.HttpContext.Request.Query?.ToString()}");
        //}
    }
}
