using Newtonsoft.Json;

namespace ProPri.WebApp.Api.Helpers
{
    public class ApiResponse
    {
        #region Propriedades

        [JsonProperty("success")]
        public bool Success { get; private set; }
        [JsonProperty("result")]
        public object Result { get; private set; }
        [JsonProperty("errors")]
        public object[] Errors { get; private set; }

        #endregion

        #region Construtores

        public ApiResponse(bool success, object result, object[] errors)
        {
            Success = success;
            Result = result;
            Errors = errors;
        }

        public ApiResponse(bool success, object result, object erro)
        {
            Success = success;
            Result = result;
            Errors = new[] { erro };
        }

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}