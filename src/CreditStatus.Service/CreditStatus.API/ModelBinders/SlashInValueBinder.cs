using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace CreditStatus.API.ModelBinders
{
    class SlashInValueBinder : IModelBinder
    {
        /// <summary>
        /// Remove the Slash at the end if slash is appaneded at the End of the URL
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            // For now we have used this  for bool type parameters
            // If used for other types params we need to add those cases here 
            if (bindingContext.ModelType == typeof(bool))
            {
                bindingContext.Model = Convert.ToBoolean(value.RawValue.ToString().TrimEnd('/'));
            }
            return true;
        }
    }
}
