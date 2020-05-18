using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using TodoList.Persistence.DTO;

namespace TodoList.Desktop.ViewModel
{
    public class ListViewModel : ViewModelBase, IEditableObject
    {

        private Int32 _id;
        private String _name;

        private Boolean _isDirty = false;
        private ListViewModel _backup;

        public Int32 Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler EditEnded;

        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (ListViewModel)this.MemberwiseClone();
                _isDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (_isDirty)
            {
                Id = _backup.Id;
                Name = _backup.Name;
                _isDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (_isDirty)
            {
                EditEnded?.Invoke(this, EventArgs.Empty);
                _isDirty = false;
                _backup = null;
            }
        }

        public static explicit operator ListViewModel(ListDto dto) => new ListViewModel
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static explicit operator ListDto(ListViewModel vm) => new ListDto
        {
            Id = vm.Id,
            Name = vm.Name
        };
    }

    public class ListValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ListViewModel list = (value as BindingGroup).Items[0] as ListViewModel;
            if (String.IsNullOrEmpty(list.Name))
            {
                return new ValidationResult(false,
                    "Name cannot be empty.");
            }
            else if (list.Name.Length > 30)
            {
                return new ValidationResult(false,
                    "Name cannot be longer than 30 characters.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
