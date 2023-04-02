using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUI
{
    public abstract class SelectionCollection<TCollection, TItem> : ObservableCollection<TItem>
        where TCollection : SelectionCollection<TCollection, TItem>
        where TItem : SelectionItem<TCollection, TItem>
    {
        private TItem _selectedItem;

        public TItem SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                if (_selectedItem == value)
                {
                    return;
                }
                _selectedItem = value; 
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SelectedItem)));
                if (_selectedItem != null)
                {
                    _selectedItem.IsChecked = true;
                }
                OnSelectedItemChanged();
            }
        }

        public event EventHandler SelectedItemChanged;

        protected virtual void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count > 0)
            {
                foreach (TItem item in e.OldItems)
                {
                    item.Container = null;
                    item.IsChecked = false;
                }
            }
            if (e.NewItems?.Count > 0)
            {
                foreach (TItem item in e.NewItems)
                {
                    item.Container = (TCollection)this;
                    if (item.IsChecked)
                    {
                        this.SelectedItem = item;
                    }
                }
            }

            base.OnCollectionChanged(e);
        }
    }

    public abstract class SelectionItem<TCollection, TItem> : UIVm
        where TCollection : SelectionCollection<TCollection, TItem>
        where TItem : SelectionItem<TCollection, TItem>
    {
        public TCollection Container { get; internal set; }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value)
                {
                    return;
                }
                _isChecked = value;
                OnPropertyChanged();
                if (Container != null)
                {
                    if (_isChecked)
                    {
                        Container.SelectedItem = (TItem)this;
                    }
                    else if (Container.SelectedItem == this)
                    {
                        Container.SelectedItem = null;
                    }
                }
            }
        }

    }
}
