using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMSTYS.Model;

namespace GMSTYS.Web.Models
{
    public class GamesToysViewModel
    {
        public GamesToysViewModel()
        {

        }
        public GamesToysViewModel(GamesModel model )
        {
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            AgeRestriction = model.AgeRestriction;
            Company = model.Company;
            Price = model.Price;
            IsNew = model.IsNew;
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }

        public static explicit operator GamesModel(GamesToysViewModel viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }
            var model = new GamesModel() {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                AgeRestriction = viewModel.AgeRestriction,
                Company = viewModel.Company,
                Price = viewModel.Price,
                IsNew = viewModel.IsNew
            };
            return model;
        }
    }
}