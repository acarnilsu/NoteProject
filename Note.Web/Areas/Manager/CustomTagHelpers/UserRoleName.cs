using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Note.EntityLayer.Concrete;

namespace Note.Web.Areas.Manager.CustomTagHelpers
{
    public class UserRoleName
    {
        [HtmlTargetElement("td", Attributes = "user-roles")]
        public class UserRolesName : TagHelper
        {
            public UserManager<AppUser> UserManager { get; set; }

            public UserRolesName(UserManager<AppUser> userManager)
            {
                this.UserManager = userManager;
            }

            [HtmlAttributeName("user-roles")]
            public string UserId { get; set; }

            public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
            {
                AppUser user = await UserManager.FindByIdAsync(UserId);

                IList<string> roles = await UserManager.GetRolesAsync(user);

                string html = string.Empty;

                roles.ToList().ForEach(x =>
                {
                    html += $"<span class='badge bg-dark'>  {x}  </span> </br>";
                });

                output.Content.SetHtmlContent(html);
            }
        }
    }
}
