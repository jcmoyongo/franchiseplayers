using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FP.Models
{
    public class RadioOption : INotifyPropertyChanged
    {
        public RadioCategory Category { get; }
        public string Title { get; }

        private bool IsSelected { get; set; }

        public bool GetIsSelected()
        {
            return IsSelected;
        }

        public void SetIsSelected(bool value)
        {
            if (value != IsSelected)
            {
                this.IsSelected = value;
                NotifyPropertyChanged();
            }
        }

        public RadioOption(RadioCategory category, string title, bool isSelected = false)
        {
            this.Category = category;
            this.Title = title;
            this.SetIsSelected(isSelected);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum RadioCategory
    {
        CategoryA,
        CategoryB,
        CategoryC,
        CategoryD
    }

}
