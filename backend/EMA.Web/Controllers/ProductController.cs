using EMA.DB.Entities;
using EMA.DB.Repositories;
using EMA.Services.Models.Requests;
using EMA.Services.Models.Responses;
using EMA.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EMA.Web.Controllers
{
    [ApiController]
    [Route("/api")]
    [SwaggerTag("商品")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IGenericRepository<ProductEm> _productRepository;

        public ProductController(ILogger<ProductController> logger, IGenericRepository<ProductEm> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// 商品の作成
        /// </summary>
        /// <param name="request">商品情報</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">リクエストが不正</response>
        /// <response code="500">サーバー上の処理が失敗しました</response>
        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var productEm = new ProductEm()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };

            try
            {
                await _productRepository.CreateAsync(productEm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商品の作成に失敗しました。");
                return this.InternalServerError("商品の作成に失敗しました。");
            }

            return Ok();
        }

        /// <summary>
        /// 商品の一覧を取得する
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="500">サーバー上の処理が失敗しました</response>
        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<Product[]>> GetProducts()
        {
            try
            {
                var productEms = await _productRepository.GetAllAsync();
                var products = productEms.Select(x => new Product() { Id = x.Id, Name = x.Name, });

                return products.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "商品の一覧取得に失敗しました。");
                return this.InternalServerError<Product[]>("商品の一覧取得に失敗しました。");
            }
        }
    }
}
