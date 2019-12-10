using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace ProPri.WebApp.Mvc.Extensions
{
    public class AgeTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.TagName = "p";

            if (DateTime.TryParse(content.GetContent(), out var date))
            {
                var today = DateTime.Today;
                var age = today.Year - date.Year;
                if (date.Date > today.AddYears(-age)) age--;

                output.Content.SetContent($"Age: {age}");
            }
        }
    }
}