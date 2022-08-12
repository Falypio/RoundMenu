using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using RoundMenu.Views;
using System.Windows;

namespace RoundMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void OnInitialized()
        {
            /*var service = App.Current.MainView.DataContext as IConfigureService;
            if (service != null)
                service.Configure();
            */
            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*containerRegistry.GetContainer();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();*/
        }
    }
}
