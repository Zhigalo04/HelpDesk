using HelpDesk.Models;
using System.ComponentModel.DataAnnotations;

public class ContactTests
{
    [Fact]
    public void Email_InvalidFormat_ShouldFailValidation()
    {
        var contact = new Contact { Email = "invalid-email", Message = "Test" };
        var context = new ValidationContext(contact);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(contact, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Почта должна содержать символ @"));
    }
}