using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.Models;
using AutoMapper;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    sealed class ProductsViewModel : CrudViewModel<Product>
    {
        private string _title;
        private double _initialPrice;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public double InitialPrice
        {
            get => _initialPrice;
            set => Set(ref _initialPrice, value);
        }

        public ProductsViewModel(IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
            DisplayName = "Manage Products";
        }

        protected override void PreSaveAction(Product item)
        {

        }

        protected override bool PreSaveCheck()
        {
            return true;
        }

        protected override void ClearFields()
        {

        }
    }
}
