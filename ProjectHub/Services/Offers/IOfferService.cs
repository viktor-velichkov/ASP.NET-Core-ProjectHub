using ProjectHub.Data.Models;
using ProjectHub.Models.Offer;
using System.Collections.Generic;

namespace ProjectHub.Services.Offers
{
    public interface IOfferService
    {
        public void AddOffer(OfferAddVIewModel model);

        public bool IsLoggedUserAlreadySentAnOfferForThisProject(int userId, int projectId);

        public List<Offer> GetProjectOffersByPosition(int projectId, string position);
           
    }
}
