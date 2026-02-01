using System.ComponentModel.DataAnnotations;

namespace BlazorApp02.Components.Pages.BizA;

[CustomValidation(typeof(EditAuthorViewModel), "NameAndPhoneCheck")]
public class EditAuthorViewModel
{
    [Display(Name = "著者ID")]
    public string AuthorId { get; set; } = "";

    [Display(Name = "著者名（名）")]
    [Required(ErrorMessage = "著者名（名）は必須入力です。")]
    [RegularExpression(@"^[\u0020-\u007E]{1,20}$",
        ErrorMessage = "著者名（名）は半角20文字以内で入力してください。")]
    public string AuthorFirstName { get; set; } = "";

    [Display(Name = "著者名（姓）")]
    [Required(ErrorMessage = "著者名（姓）は必須入力です。")]
    [RegularExpression(@"^[\u0020-\u007E]{1,40}$",
        ErrorMessage = "著者名（姓）は半角40文字以内で入力してください。")]
    public string AuthorLastName { get; set; } = "";

    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "電話番号は必須入力です。")]
    [RegularExpression(@"^\d{3} \d{3}-\d{4}$",
        ErrorMessage = "電話番号は 012 345-6789 のように入力してください。")]
    public string Phone { get; set; } = "";

    public static ValidationResult NameAndPhoneCheck(EditAuthorViewModel vm, ValidationContext ctx)
    {
        if (vm.AuthorFirstName == "Nobuyuki" && vm.AuthorLastName == "Akama")
            return new ValidationResult("Nobuyuki Akama という名前は予約済みのため登録できません。",
                new List<string>() {"AuthorFirstName", "AuthorLastName"});
        return ValidationResult.Success;
    }
}
