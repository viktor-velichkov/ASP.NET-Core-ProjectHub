using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Offer;

namespace ProjectHub.Services.Offers
{
    public class OfferService : IOfferService
    {
        private readonly ProjectHubDbContext data;

        public OfferService(ProjectHubDbContext data)
        {
            this.data = data;
        }

        public void AddOffer(OfferAddVIewModel model)
        {
            var offer = new Offer
            {
                AuthorId = model.AuthorId,
                ProjectId = model.ProjectId,
                Position = model.Position,
                Date = DateTime.Now,
                Price = model.Price,
                Description = model.Description
            };

            this.data.Offers.Add(offer);

            this.data.SaveChanges();
        }
        public void RemoveOffersForThisPosition(int projectId, string position)
        {
            var offers = this.data.Offers.Where(offer => offer.ProjectId.Equals(projectId)
                                                        && offer.Position.Equals(position))
                                         .ToList();            

            this.data.Offers.RemoveRange(offers);

            this.data.SaveChanges();
        }


        public List<Offer> GetProjectOffersByPosition(int projectId, string position)
        {
            var offers = this.data
                             .Offers
                             .Include(offer => offer.Author)
                             .Where(offer => offer.ProjectId.Equals(projectId));

            if (position != "All")
            {
                offers = offers.Where(offer => offer.Position.Equals(position));
            }

            return offers.ToList();                     
        }
                

        public bool IsLoggedUserAlreadySentAnOfferForThisProject(int userId, int projectId)
            => this.data.Offers.Any(offer => offer.AuthorId.Equals(userId) && offer.ProjectId.Equals(projectId));

        public bool IsLoggedUserAlreadyWasHiredForThisProject(int userId, int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
