using Newtonsoft.Json;

namespace EMA.Services.Models.Requests
{
    /// <summary>
    /// 商品を作成するリクエストモデル
    /// </summary>
    public class CreateProductRequest
    {
        /// <summary>
        /// 商品の名前
        /// </summary>
        /// <value></value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = null!;
    }
}
