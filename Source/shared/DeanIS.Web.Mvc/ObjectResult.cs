using System;
using System.Text;
using System.Web.Mvc;
using DeanIS.Net.Serialization;

namespace DeanIS.Web.Mvc
{
    public class ObjectResult<T> : ActionResult
        where T : class
    {
        private static UTF8Encoding UTF8 = new UTF8Encoding(false);

        private Serializer<T> _serializer = new Serializer<T>();

        public T Data { get; set; }

        public Type[] KnownTypes = new [] { typeof(object) };

        public ObjectResult(T data)
        {
            this.Data = data;
        }

        public ObjectResult(T data, Type[] knownTypes)
        {
            Data = data;
            KnownTypes = knownTypes;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var contentType = context.HttpContext.Request.Headers["Content-Type"];

            if (contentType != null && contentType.Contains("application/json"))
            {
                new JsonResult { Data = Data }.ExecuteResult(context);
            }
            else
            {
                new ContentResult
                    {
                        ContentType = "text/xml",
                        Content = _serializer.ToString(Data),
                        ContentEncoding = UTF8
                    }.ExecuteResult(context);
            }
        }
    }
}
