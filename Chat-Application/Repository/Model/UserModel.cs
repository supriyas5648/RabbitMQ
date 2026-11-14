using System.ComponentModel.DataAnnotations;

namespace MVC;
public class UserModel
{
public int UserId { get; set; }
[Required(ErrorMessage ="Email id Is required")]
public string UserEmail { get; set; }
[Required(ErrorMessage = "Mobile Number Is required")]
public string UserMobile { get; set; }
[Required(ErrorMessage = "Password Is required")]
public string UserPassword { get; set; }
[Required(ErrorMessage = "Confirm Password Is required")]
[Compare("password", ErrorMessage = "Password and Confirmation Password must match.")]
public string ConfirmPassword { get; set; }
[Required(ErrorMessage = "Date Of Birth Is required")]
public string Dob { get; set; }
}