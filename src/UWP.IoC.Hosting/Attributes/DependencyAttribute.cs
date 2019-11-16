using System;

namespace UWP.IoC.Hosting.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DependencyAttribute : Attribute
    {
    }
}
