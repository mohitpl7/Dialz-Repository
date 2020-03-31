using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute()
        {

        }

        public ServiceAttribute(Type serviceType)
        {
            this.ServiceType = serviceType;
        }

        public Lifetime Lifetime { get; set; } = Lifetime.Transient;
        public Type ServiceType { get; set; }
    }
}
