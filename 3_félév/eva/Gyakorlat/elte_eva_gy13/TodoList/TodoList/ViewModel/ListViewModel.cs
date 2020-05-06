using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELTE.Windows.TodoList.Model;

namespace ELTE.Windows.TodoList.ViewModel
{
    public class ListViewModel : ViewModelBase
    {
        private List list;

        public List Entity
        {
            get { return list; }
        }

        [Required]
        [StringLength(50)]
        public String Name
        {
            get { return list.Name; }
            set { list.Name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return new ObservableCollection<ItemViewModel>(
                    list.Items.Select(item => new ItemViewModel(item))
                );
            }
        }

        public ListViewModel(List list)
        {
            this.list = list;
        }
    }
}
