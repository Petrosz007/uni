using System;
using System.ComponentModel;
using TodoList.Persistence.DTO;

namespace TodoList.Desktop.ViewModel
{
    public class ItemViewModel : ViewModelBase, IEditableObject, IDataErrorInfo
    {
        private Int32 _id;
        private String _name;
        private String _description;
        private DateTime _deadline;
        private byte[] _image;
        private Int32 _listId;

        private ItemViewModel _backup;

        public String this[string columnName]
        {
            get
            {
                String error = String.Empty;
                switch(columnName)
                {
                    case nameof(Name):
                        if (String.IsNullOrEmpty(Name))
                            error = "Name cannot be empty.";
                        else if (Name.Length > 30)
                            error = "Name cannot be longer than 30 characters.";
                        break;
                }
                return error;
            }
        }

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

        public String Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged();
            }
        }

        public byte[] Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public Int32 ListId
        {
            get => _listId;
            set
            {
                _listId = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsDirty { get; private set; } = false;

        public String Error => String.Empty;

        public void BeginEdit()
        {
            if (!IsDirty)
            {
                _backup = (ItemViewModel)this.MemberwiseClone();
                IsDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (IsDirty)
            {
                Id = _backup.Id;
                Name = _backup.Name;
                Description = _backup.Description;
                Deadline = _backup.Deadline;
                Image = _backup.Image;
                ListId = _backup.ListId;

                IsDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (IsDirty)
            {
                IsDirty = false;
                _backup = null;
            }
        }

        public static explicit operator ItemViewModel(ItemDto dto) => new ItemViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Deadline = dto.Deadline,
            Image = dto.Image,
            ListId = dto.ListId
        };

        public static explicit operator ItemDto(ItemViewModel vm) => new ItemDto
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Deadline = vm.Deadline,
            Image = vm.Image,
            ListId = vm.ListId
        };
    }
}
