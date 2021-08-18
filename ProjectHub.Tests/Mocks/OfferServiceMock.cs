using Moq;
using ProjectHub.Services.Offers;

namespace ProjectHub.Tests.Mocks
{
    public static class OfferServiceMock
    {
        public static IOfferService Instance
        {
            get
            {
                var offerService = new Mock<IOfferService>();

                offerService
                    .Setup(offer => offer.IsOfferAlreadyExists(3, 1))
                    .Returns(true);

                return offerService.Object;
            }
        }
    }
}
