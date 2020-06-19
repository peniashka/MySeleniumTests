using SeleniunAdvancedTest.Models;

namespace SeleniunAdvancedTest.Factories
{
    public static class PracticeFormFactory
    {
        public static PracticeFormModel Create()
        {
            return new PracticeFormModel
            {
                Email = "",
                FirstName = "UserFirstName",
                LastName = "UserLastName",
                Phone = "0898123456",
                Password = "pass123",
                Address = "str1, 3, 23",
                City = "city1",
                PostCode = "12345"
            };
        }
    }
}
