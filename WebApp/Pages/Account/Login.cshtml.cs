using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            // verify the credential 
            if (Credential.UserName == "admin" && Credential.Password == "monkey")
            {
                // creating the security context here 

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin" ),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                var principle = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync("MyCookieAuth", principle);


                return RedirectToPage("/index");
            }
            
            
            return Page();

        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "user name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
