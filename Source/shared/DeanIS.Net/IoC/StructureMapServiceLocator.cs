using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace DeanIS.Net.IoC
{
    public class StructureMapServiceLocator : IServiceLocator
    {
        public object GetService(Type serviceType)
        {
            return ObjectFactory.GetInstance(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return ObjectFactory.GetAllInstances<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return (IEnumerable<object>) ObjectFactory.GetAllInstances(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return ObjectFactory.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return ObjectFactory.GetNamedInstance<TService>(key);
        }

        public object GetInstance(Type serviceType)
        {
            return ObjectFactory.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return ObjectFactory.GetNamedInstance(serviceType, key);
        }
    }
}
