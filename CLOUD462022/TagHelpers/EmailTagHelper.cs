using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CLOUD462022.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailAddress { get; set; } 
        public string Content { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + EmailAddress);
            output.Content.SetContent(Content);
        }
    }
}
