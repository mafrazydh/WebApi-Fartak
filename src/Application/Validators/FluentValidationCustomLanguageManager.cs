namespace Application.Validators
{

    /// کلاس فارسی ساز
    public class FluentValidationCustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public FluentValidationCustomLanguageManager()
        {
            Culture = new System.Globalization.CultureInfo("fa-IR");
            AddTranslation("fa-IR","NotEmptyValidator",",زیزم داری اشتباه میزنی");
        }
    }
}
