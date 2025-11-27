namespace Certify.Domain.Entities.Base
{
    [Serializable]
    public abstract class AuditableBaseEntity : BaseEntity
    {
        private bool isActive;
        private int createdBy;
        private DateTime createdDate;
        private int? updatedBy;
        private DateTime? updatedDate;

        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }

        public int CreatedBy
        {
            get => createdBy;
            set => SetProperty(ref createdBy, value);
        }

        public DateTime CreatedDate
        {
            get => createdDate;
            set => SetProperty(ref createdDate, value);
        }

        public int? UpdatedBy
        {
            get => updatedBy;
            set => SetProperty(ref updatedBy, value);
        }

        public DateTime? UpdatedDate
        {
            get => updatedDate;
            set => SetProperty(ref updatedDate, value);
        }
    }
}
