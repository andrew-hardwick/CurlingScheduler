using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;

namespace CurlingScheduler.ViewModel
{
    public class Locator
    {
        public Locator()
        {
            var container = new UnityContainer();

            container.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
