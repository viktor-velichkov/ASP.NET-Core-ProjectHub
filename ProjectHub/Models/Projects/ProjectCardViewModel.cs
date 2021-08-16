﻿namespace ProjectHub.Models.Projects
{
    public class ProjectCardViewModel
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }
        public string Name { get; set; }

        public string City { get; set; }

        public int InvestorId { get; set; }

        public string Investor { get; set; }
    }
}
