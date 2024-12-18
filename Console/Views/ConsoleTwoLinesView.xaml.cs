﻿using Console.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Console.Views
{
    /// <summary>
    /// Interaction logic for ConsoleTwoLinesView.xaml
    /// </summary>
    public partial class ConsoleTwoLinesView : UserControl
    {
        protected readonly Prism.Events.IEventAggregator _eventAggregator;

        public ConsoleTwoLinesView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this._eventAggregator = eventAggregator;
            this.DataContext = new ConsoleTwoLinesViewModel(_eventAggregator);
        }
    }
}
