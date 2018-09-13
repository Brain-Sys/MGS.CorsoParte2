using GalaSoft.MvvmLight.Messaging;
using MGS.CorsoParte2.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MGS.CorsoParte2.WindowsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Messenger.Default.Register<OpenNewViewMessage>(this, openNewView);
        }

        private void openNewView(OpenNewViewMessage msg)
        {
            var ns = Assembly.GetExecutingAssembly()
                .GetModules().First().Name;
            ns = ns.Replace(".exe", string.Empty);
            string typeName = string.Concat(ns, ".", msg.ViewName, "Window");
            Type type = Type.GetType(typeName);

            if (type != null)
            {
                var wnd = Activator.CreateInstance(type) as Window;

                if (msg.Maximized)
                {
                    wnd.WindowState = WindowState.Maximized;
                }

                if (msg.Modal)
                {
                    wnd.ShowDialog();
                }
                else
                {
                    wnd.Show();
                }
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string[] parametri = e.Args;
            LoginWindow wnd = new LoginWindow();
            wnd.ShowDialog();
        }
    }
}