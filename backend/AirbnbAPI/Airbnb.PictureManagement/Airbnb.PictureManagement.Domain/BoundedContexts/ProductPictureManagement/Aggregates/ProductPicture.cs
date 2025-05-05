using Airbnb.SharedKernel;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates
{
    public class ProductPicture : AggregateRoot
    {
        public Guid PictureGuid { get; private set; }
        public string Url { get; private set; }
        public int ProductId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsArchived { get; private set; }

        public ProductPicture() { }

        public void Archive()
        {
            if (IsArchived)
                return;

            IsArchived = true;
            RaiseEvent(new ProductPictureArchivedEvent(Id));
        }
        
        public ProductPicture(Guid pictureGuid, string url, int productId, DateTime createdAt)
        {
            PictureGuid = pictureGuid;
            Url = url;
            ProductId = productId;
            CreatedAt = createdAt;

            RaiseEvent(new ProductPictureCreatedEvent(Id, pictureGuid, url, productId, createdAt));
        }

        #region Aggregate Methods

        public void UpdatePicture(string url)
        {
            Url = url;
            RaiseEvent(new ProductPictureUpdatedEvent(Id, ProductId, PictureGuid, url));
        }

        public void ArchivePicture()
        {
            RaiseEvent(new ProductPictureArchivedEvent(Id));
        }

        public void DeletePicture()
        {
            RaiseEvent(new ProductPictureDeletedEvent(Id, PictureGuid));
        }

        #endregion

        #region Event Handling

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ProductPictureCreatedEvent e:
                    OnPictureCreatedEvent(e);
                    break;
                case ProductPictureUpdatedEvent e:
                    OnPictureUpdatedEvent(e);
                    break;
                case ProductPictureArchivedEvent e:
                    OnPictureArchivedEvent(e);
                    break;
                case ProductPictureDeletedEvent e:
                    OnPictureDeletedEvent(e);
                    break;
            }
        }

        private void OnPictureCreatedEvent(ProductPictureCreatedEvent @event)
        {
            Id = @event.Id;
            PictureGuid = @event.AggregateId;
            Url = @event.Url;
            ProductId = @event.ProductId;
            CreatedAt = @event.CreatedDate;
        }

        private void OnPictureUpdatedEvent(ProductPictureUpdatedEvent @event)
        {
            Id = @event.Id;
            Url = @event.Url;
        }

        private void OnPictureArchivedEvent(ProductPictureArchivedEvent @event)
        {
            Id = @event.Id;
        }

        private void OnPictureDeletedEvent(ProductPictureDeletedEvent @event)
        {
            Id = @event.Id;
        }

        #endregion
    }
}
