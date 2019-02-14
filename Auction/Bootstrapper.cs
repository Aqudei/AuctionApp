using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auction.Models;
using Auction.ViewModels;
using Auction.ViewModels.AuctionDetail;
using AutoMapper;
using Caliburn.Micro;

namespace Auction
{
    class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Account, RegistrationViewModel>().ReverseMap();
                cfg.CreateMap<Product, ProductsViewModel>().ReverseMap();
            });


            _container.Instance(Mapper.Instance);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<LoginViewModel>();
            _container.Singleton<ShellViewModel>();

            _container.PerRequest<AuctionDetailViewModel>();
            _container.PerRequest<RegistrationViewModel>();
            _container.PerRequest<ProductsViewModel>();
            _container.PerRequest<AuctionMainViewModel>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
