using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Certify.Domain.Entities.Base
{
    [Serializable]
    public abstract class BaseEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected BaseEntity()
        {
            State = ChangeState.New;
        }

        #region State 

        public ChangeState State { get; private set; }

        protected bool SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;

            if (State == ChangeState.Unchanged)
                State = ChangeState.Modified;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public void MarkDeleted() => State = ChangeState.Deleted;

        public void AcceptChanges() => State = ChangeState.Unchanged;

        public virtual void RejectChanges()
        {
            // Override in derived classes if you keep original values
            State = ChangeState.Unchanged;
        }

        public virtual bool IsValid() => true;

        #endregion

        #region Audit fields

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

        #endregion
    }

    public enum ChangeState
    {
        Unchanged = 0,
        New = 1,
        Modified = 2,
        Deleted = 3,
        Excluded = 4
    }
}
