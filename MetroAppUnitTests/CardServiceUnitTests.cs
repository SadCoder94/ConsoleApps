using MetroApp.DataRepo;
using MetroApp.Models;
using MetroApp.Services;
using Moq;
using System;
using Xunit;

namespace MetroAppUnitTests
{
    public class CardServiceUnitTests
    {
        private Mock<ICardInfoRepo> _mockCardRepo;
        private CardService _cardService;
            
        public CardServiceUnitTests()
        {
            _mockCardRepo = new Mock<ICardInfoRepo>();
            _cardService = new CardService(_mockCardRepo.Object);
        }

        [Fact]
        public void TestGetCardBalance()
        {
            _mockCardRepo.Setup(d => d.GetCardBalanceByCardNumber("CM1")).Returns(400);

            var result = _cardService.GetCardBalanceByCardNumber("CM1");

            Assert.Equal(400, result);

        }

        [Fact]
        public void TestDeductBalance()
        {
            _mockCardRepo.Setup(d => d.LoadCardInfo(new Card() { CardNumber="CM1", CardBalance = 500}));

            _cardService.DeductCardBalance("CM1", 100);

            _mockCardRepo.Verify(m => m.DeductCardBalance("CM1", 100), Times.Once);

        }
    }
}
