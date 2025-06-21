using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;

    public class Advantage : AggregateRoot
    {
        public int ProductId { get; private set; }
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public Advantage() { }
        
        public Advantage(int productId, string title, string description)
        {
            Create(productId, title, description);
        }

        #region Aggregate Methods

        public void Create(int productId, string title, string description)
        {
            ProductId = productId;
            Title = title;
            Description = description;

            RaiseEvent(new ProductAdvantageCreatedEvent(Id, productId, title, description));
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;

            RaiseEvent(new ProductAdvantageUpdatedEvent(Id, title, description));
        }

        public void Delete()
        {
            RaiseEvent(new ProductAdvantageDeletedEvent(Id));
        }

        #endregion

        #region Event Handling

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ProductAdvantageCreatedEvent e:
                    OnProductAdvantageCreatedEvent(e);
                    break;
                case ProductAdvantageUpdatedEvent e:
                    OnProductAdvantageUpdatedEvent(e);
                    break;
                case ProductAdvantageDeletedEvent e:
                    OnProductAdvantageDeletedEvent(e);
                    break;
            }
        }

        private void OnProductAdvantageCreatedEvent(ProductAdvantageCreatedEvent e)
        {
            Id = e.AggregateId;
            ProductId = e.ProductId;
            Title = e.Title;
            Description = e.Description;
        }

        private void OnProductAdvantageUpdatedEvent(ProductAdvantageUpdatedEvent e)
        {
            Title = e.Title;
            Description = e.Description;
        }

        private void OnProductAdvantageDeletedEvent(ProductAdvantageDeletedEvent e)
        {
            Id = e.AggregateId;
        }

        #endregion
    }