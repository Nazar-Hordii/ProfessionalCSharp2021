﻿using BooksApp.Services;
using BooksApp.ViewModels;
using BooksLib.Models;
using BooksLib.Services;
using BooksLib.ViewModels;
using GenericViewModels.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BooksApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            RegisterServices();

            m_window = new MainWindow();
            m_window.Activate();
        }

        private void RegisterServices()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IBooksRepository, BooksSampleRepository>()
                    .AddScoped<BooksViewModel>()
                    .AddScoped<BookDetailViewModel>()
                    .AddScoped<MainWindowViewModel>()
                    .AddSingleton<IItemsService<Book>, BooksService>()
                    .AddSingleton<IDialogService, WinUIDialogService>()
                    .AddSingleton<INavigationService, WinUINavigationService>()
                    .AddSingleton<WinUIInitializeNavigationService>()
                    .AddLogging(builder =>
                    {
                        builder.AddDebug();
                    }).BuildServiceProvider());

        }

        private Window? m_window;
    }
}