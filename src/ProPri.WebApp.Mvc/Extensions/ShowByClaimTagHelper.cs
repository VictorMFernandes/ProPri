using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProPri.Users.Application.Queries;
using System;

namespace ProPri.WebApp.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "show-by-claim")]
    public class ShowByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsersQueries _usersQueries;

        public ShowByClaimTagHelper(IHttpContextAccessor contextAccessor,
                                        IUsersQueries usersQueries)
        {
            _contextAccessor = contextAccessor;
            _usersQueries = usersQueries;
        }

        [HtmlAttributeName("show-by-claim")]
        public string ShowClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (string.IsNullOrEmpty(ShowClaimValue)) return;

            var userId = AuthorizationExtensions.GetLoggedUserId(_contextAccessor.HttpContext.User);
            var hasAccess = _usersQueries.IsAuthorized(userId, ShowClaimValue).Result;

            if (hasAccess)
                output.SuppressOutput();
        }
    }
}