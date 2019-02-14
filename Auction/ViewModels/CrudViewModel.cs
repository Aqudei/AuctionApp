using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Auction.Events;
using Auction.Models;
using Auction.Persistence;
using AutoMapper;
using Caliburn.Micro;

namespace Auction.ViewModels
{
    abstract class CrudViewModel<T> : Screen, IHandleWithTask<CrudEvent<T>> where T : EntityBase
    {
        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;
        public Guid Id { get; set; }

        private readonly BindableCollection<T> _items = new BindableCollection<T>();
        private T _selectedItem;
        public ICollectionView Items { get; set; }

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (value == null)
                {
                    Id = Guid.Empty;

                    ClearFields();
                    return;
                }

                _mapper.Map(value, this);
            }
        }

        protected CrudViewModel(IMapper mapper, IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            Items = CollectionViewSource.GetDefaultView(_items);
            using (var db = new AuctionContext())
            {
                _items.AddRange(db.Set<T>().ToList());
            }

            _eventAggregator.Subscribe(this);
        }

        public void NewItem()
        {
            Id = Guid.Empty;
            ClearFields();
        }

        public async Task Delete()
        {
            if (Id == Guid.Empty)
                return;

            using (var db = new AuctionContext())
            {
                var item = db.Set<T>().Find(Id);
                if (item != null)
                {
                    db.Set<T>().Remove(item);
                    db.SaveChanges();
                    await _eventAggregator.PublishOnUIThreadAsync(new Events.CrudEvent<T>(EventType.Remove, item));
                }
            }
        }

        public async Task Save()
        {
            if (!PreSaveCheck())
                return;

            var item = _mapper.Map<T>(this);
            if (item.Id == Guid.Empty)
            {
                item.NewId();
                PreSaveAction(item);
                using (var db = new AuctionContext())
                {
                    db.Set<T>().Add(item);
                    db.SaveChanges();
                    await _eventAggregator.PublishOnUIThreadAsync(new Events.CrudEvent<T>(EventType.Create, item));
                }
            }
            else
            {
                using (var db = new AuctionContext())
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    await _eventAggregator.PublishOnUIThreadAsync(new Events.CrudEvent<T>(EventType.Update, item));
                }
            }
        }

        protected abstract void PreSaveAction(T item);

        protected abstract bool PreSaveCheck();

        protected abstract void ClearFields();

        public Task Handle(CrudEvent<T> message)
        {
            return Task.Run(() =>
            {
                if (_items.Contains(message.Entity))
                {
                    _items.Remove(message.Entity);
                }

                if (message.EventType == EventType.Remove)
                {
                    return;
                }

                _items.Add(message.Entity);
            });
        }
    }
}
