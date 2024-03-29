﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FP.Models
{
    class BetterListView: ListView
    {
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create(nameof(ItemClickCommandProperty), typeof(ICommand), typeof(BetterListView), null);

        public ICommand ItemClickCommand
        {
            get 
            { 
                return (ICommand)this.GetValue(ItemClickCommandProperty); 
            }
            set
            {
                this.SetValue(ItemClickCommandProperty, value);
            }
        }

        public BetterListView()
        {
            this.ItemTapped += OnItemTapped;
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                //ItemClickCommand?.Execute(e.Item);
                //SelectedItem = null;
            }
        }
    }
}
