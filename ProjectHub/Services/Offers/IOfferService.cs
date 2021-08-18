using ProjectHub.Data.Models;
using ProjectHub.Models.Offer;
using System.Collections.Generic;

namespace ProjectHub.Services.Offers
{
    public interface IOfferService
    {
        public void AddOffer(OfferAddVIewModel model);
        public void RemoveOffersForThisPosition(int projectId, string position);

        public bool IsOfferAlreadyExists(int userId, int projectId);
        public bool IsLoggedUserAlreadyWasHiredForThisProject(int userId, int projectId);

        public List<Offer> GetProjectOffersByPosition(int projectId, string position);
           
    }
}
