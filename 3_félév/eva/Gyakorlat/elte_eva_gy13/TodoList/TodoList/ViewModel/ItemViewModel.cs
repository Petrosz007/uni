using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELTE.Windows.TodoList.Model;

namespace ELTE.Windows.TodoList.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private Item item;

        public Item Entity
        {
            get { return item; }
        } 

        public String Name
        {
            get { return item.Name; }
            set { item.Name = value; OnPropertyChanged(); }
        }

        public String Description
        {
            get { return item.Description; }
            set { item.Description = value; OnPropertyChanged(); }
        }

        public DateTime Deadline
        {
            get { return item.Deadline; }
            set { item.Deadline = value; OnPropertyChanged(); }
        }

        public ListViewModel List
        {
            get
            {
                return new ListViewModel(item.List);
            }
        }

        public ItemViewModel(Item item)
        {
            this.item = item;
        }

        public ItemViewModel()
        {
            item = new Item()
            {
                Name = "Új elem",
                Deadline = DateTime.Now.AddDays(1)
            };
        }
    }
}
