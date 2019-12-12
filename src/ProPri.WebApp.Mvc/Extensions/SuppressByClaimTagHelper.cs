using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProPri.Users.Application.Queries;
using System;

namespace ProPri.WebApp.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "suppress-by-claim")]
    public class SuppressByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsersQueries _usersQueries;

        public SuppressByClaimTagHelper(IHttpContextAccessor contextAccessor,
                                        IUsersQueries usersQueries)
        {
            _contextAccessor = contextAccessor;
            _usersQueries = usersQueries;
        }

        [HtmlAttributeName("suppress-by-claim")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var userId = AuthorizationExtensions.GetLoggedUserId(_contextAccessor.HttpContext.User);
            var hasAccess = _usersQueries.IsAuthorized(userId, IdentityClaimValue).Result;

            if (hasAccess) return;

            output.SuppressOutput();
        }
    }
}