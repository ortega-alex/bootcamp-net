
using InternationalApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IStringLocalizer<PostsController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResouceLocalizer;

        public PostsController(IStringLocalizer<PostsController> stringLocalizer, IStringLocalizer<SharedResource> sharedResouceLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _sharedResouceLocalizer = sharedResouceLocalizer;
        }

        [HttpGet]
        [Route("PostControllerResource")]
        public IActionResult GetUsingPostControllerResource()
        {
            // Find texto
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName
            });
        }

        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResources()
        {
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;
            var todayIs = string.Format(_sharedResouceLocalizer.GetString("TodayIs"), DateTime.Now.ToLongDateString());

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName,
                TodayIs = todayIs
            });
        }
    }
}
