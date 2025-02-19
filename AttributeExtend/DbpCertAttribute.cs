
using System.Net;
using BaseApi.Configuaration;
using BaseApi.Enums;
using BaseApi.Models.BaseRequest;
using BaseApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseApi.AttributeExtend
{
    public class DbpCertAttribute : ActionFilterAttribute, IOperationFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!filterContext.ModelState.IsValid)
                    throw new Exception();

                var t0 = filterContext.ActionArguments.Values.ElementAt(0) as DpsParamBase;
                if (t0 == null)
                {
                    throw new ErrorException()
                    {
                        Code = ErrorCode.INVALID_CERT,
                        Message = "Invalid Certificate"
                    };
                }

                //TODO: Mở lại sau
                /*string s = $"{GlobalSettings.AppSettings.DPS_CERT}{t0.Time}";
                string key = MD5Util.Encrypt(s);
                if (t0.KeyCert != key)
                {
                    throw new ErrorException()
                    {
                        Code = ErrorCode.INVALID_CERT,
                        Message = "Invalid Certificate"
                    };
                }*/
            }
            catch (ErrorException e)
            {
                filterContext.Result = new ContentResult() { StatusCode = (int?)e.Code, Content = e.Message };
                return;
            }
            catch (Exception e)
            {
                filterContext.Result = new ContentResult() { StatusCode = (int?)HttpStatusCode.Unauthorized, Content = e.Message };
                return;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

        }
    }
}