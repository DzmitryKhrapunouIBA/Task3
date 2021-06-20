using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace IBA.Task3
{
    public class ResultBase
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("msg")]
        public string[] Messages { get; set; }

        [JsonProperty("code")]
        public int ResponceCode { get; set; }

        public static ResultBase Failure(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, params string[] messages)
        {
            return new ResultBase() { Success = false, ResponceCode = (int)statusCode, Messages = messages };
        }

        public static ResultBase Failure(params string [] messages) => Failure(HttpStatusCode.InternalServerError, messages);

        public static ResultBase NotFound(params string[] messages) => Failure(HttpStatusCode.NotFound, messages);

        public static ResultBase Ok(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ResultBase() { Success = true, ResponceCode = (int)httpStatusCode };
        }

        public static ResultBase BadRequest(params string[] messages) 
            => Failure(HttpStatusCode.BadRequest, messages);

        public static ResultBase BadRequest(ModelStateDictionary modelState) 
            => BadRequest(modelState.Values.SelectMany(x => x.Errors.Select(t => t.ErrorMessage)).ToArray());
    }

    public class ResultModel<T> : ResultBase
    {
        [JsonProperty("data")]
        public T Model { get; set; }

        public static ResultModel<T> Ok(T model, HttpStatusCode code = HttpStatusCode.OK)
        {
            return new ResultModel<T>() { Success = true, Model = model, ResponceCode = (int)code };
        }
    }

    public class Result<T> : ResultBase
    {
        [JsonProperty("items")]
        public System.Collections.Generic.IEnumerable<T> Items { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        public static Result<T> Ok(System.Collections.Generic.IEnumerable<T> items, int? count = default(int?), HttpStatusCode code = HttpStatusCode.OK)
        {
            return new Result<T>() { Success = true, Items = items, Count = count, ResponceCode = (int)code };
        }
    }
}