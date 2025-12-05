using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Certify.Domain.Entities
{
    [Serializable]
    public abstract class BaseEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected BaseEntity()
        {
            State = ChangeState.New;
        }

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

        public void MarkModified() => State = ChangeState.Modified;

        public void AcceptChanges() => State = ChangeState.Unchanged;

        public virtual void RejectChanges() => State = ChangeState.Unchanged;

        public virtual bool IsValid() => true;
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
