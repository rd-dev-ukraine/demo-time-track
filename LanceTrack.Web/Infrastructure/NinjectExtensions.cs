using System;
using Ninject;
using Ninject.Syntax;

namespace LanceTrack.Web.Infrastructure
{
    /// <summary>
    ///     Exposes set of methods extends Ninject bindings syntax.
    /// </summary>
    public static class NinjectExtensions
    {
        public static IBindingWhenInNamedWithOrOnSyntax<T1> ConstructUsing<TService1, T1>(this IBindingToSyntax<T1> binding, Func<TService1, T1> method)
        {
            if (binding == null)
                throw new ArgumentNullException("binding");
            if (method == null)
                throw new ArgumentNullException("method");

            return binding.ToMethod(context =>
            {
                var service1 = context.Kernel.Get<TService1>();
                return method(service1);
            });
        }
    }
}