using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity.Lifetime;

namespace ToDoList.Common.IoC.Unity.Lifetime
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object key = new object();
        private ISubscriptionToken disposeToken;

        public override void RemoveValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Remove(key);
                if (disposeToken != null)
                {
                    disposeToken.Unsubscribe();
                    disposeToken = null;
                }
            }
        }

        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[key] = newValue;

            if (newValue as IDisposable != null)
                disposeToken = HttpContext.Current.DisposeOnPipelineCompleted(newValue as IDisposable);
        }

        public override object GetValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Items.Contains(key))
                return HttpContext.Current.Items[key];
            else
                return null;
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return new PerRequestLifetimeManager();
        }
    }
}
