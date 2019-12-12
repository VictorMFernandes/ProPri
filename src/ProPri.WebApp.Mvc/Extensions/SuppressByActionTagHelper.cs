using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace ProPri.WebApp.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "suppress-by-action")]
    public class SuppressByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SuppressByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("suppress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString().ToLowerInvariant();

            if (ActionName.ToLowerInvariant().Contains(action))
                output.SuppressOutput();
        }
    }
}