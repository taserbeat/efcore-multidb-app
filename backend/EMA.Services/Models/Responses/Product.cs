using Newtonsoft.Json;

namespace EMA.Services.Models.Responses
{
    public class Product
    {
        /// <summary>
        /// 商品のID
        /// </summary>
        /// <value></value>
        [JsonProperty("productId")]
        public Guid Id { get; set; }

        /// <summary>
        /// 商品の名前
        /// </summary>
        /// <value></value>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
    }
}
