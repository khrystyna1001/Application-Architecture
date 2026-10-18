using Xunit;
using EShop.Application;

namespace EShop.Application.Tests
{
    public class CardValidatorServiceTests
    {
        private readonly CardValidatorService _service;

        public CardValidatorServiceTests()
        {
            _service = new CardValidatorService();
        }

        [Theory]
        [InlineData("3497 7965 8312 797")]      // Amex з пробілами
        [InlineData("345-470-784-783-010")]     // Amex з дефісами
        [InlineData("378523393817437")]         // Amex тільки цифри
        [InlineData("4024-0071-6540-1778")]     // Visa з дефісами
        [InlineData("4532 2080 2150 4434")]     // Visa з пробілами
        [InlineData("4532289052809181")]         // Visa тільки цифри
        [InlineData("5530016454538418")]         // MasterCard
        [InlineData("5551561443896215")]         // MasterCard
        [InlineData("5131208517986691")]         // MasterCard
        public void ValidateCard_ValidNumbers_ReturnsTrue(string cardNumber)
        {
            // Act
            bool isValid = _service.ValidateCard(cardNumber);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1234-5678-9012")]
        [InlineData("12345678901234567890")]     
        [InlineData("4532 2080 2150 443A")]
        [InlineData("4532-2080-2150-4435")]   
        public void ValidateCard_InvalidNumbers_ReturnsFalse(string cardNumber)
        {
            // Act
            bool isValid = _service.ValidateCard(cardNumber);

            // Assert
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("3497 7965 8312 797", "American Express")]
        [InlineData("4532 2080 2150 4434", "Visa")]
        [InlineData("5530016454538418", "MasterCard")]
        [InlineData("1111222233334444", "Unknown")]
        public void GetCardType_ShouldReturnCorrectIssuer(string cardNumber, string expectedType)
        {
            // Act
            string actualType = _service.GetCardType(cardNumber);

            // Assert
            Assert.Equal(expectedType, actualType);
        }
    }
}
