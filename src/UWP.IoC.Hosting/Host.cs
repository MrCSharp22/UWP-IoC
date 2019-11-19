using System;
using Windows.UI.Xaml.Controls;

namespace UWP.IoC.Hosting
{
    public sealed class Host
    {
        public IServiceProvider ServicesContainer { get; }

        public IoCFrame RootFrame { get; private set; }

        public Host(IServiceProvider servicesContainer)
        {
            this.ServicesContainer = servicesContainer ?? throw new ArgumentNullException(nameof(servicesContainer));
        }

        public Frame CreateNewHostedUwpFrame()
        {
            this.RootFrame = new IoCFrame(this.ServicesContainer);
            return this.RootFrame;
        }
    }
}
