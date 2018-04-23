﻿using System.Windows;
using HyberShift_CSharp.Utilities;

namespace HyberShift_CSharp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var socketAPI = SocketAPI.GetInstance();
            socketAPI.Connect();
        }
    }
}