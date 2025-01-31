﻿using Assist.MVVM.ViewModel;
using Assist.Settings;
using Serilog;
using System.Windows;


namespace Assist.MVVM.View.InitPage
{
    /// <summary>
    /// Interaction logic for InitPage.xaml
    /// </summary>
    public partial class InitPage : Window
    {
        private InitPageViewModel _viewModel;

        public InitPage()
        {
            Log.Information("InitPage Constructor Called");
            DataContext = _viewModel = new InitPageViewModel();
            InitializeComponent();
        }

        private async void InitWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Information("InitWindow_Loaded Called");

            if (AssistSettings.Current.bNewUser)
            {
                Log.Information("New User Flag is True, Running First Time Setup.");
                await _viewModel.FirstTimeSetup();
            }
            else
            {
                Log.Information("Returning User, Running Default Startup");
                await _viewModel.DefaultStartup();
            }
        }
    }
}
