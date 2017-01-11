using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace CustomerInformation.API.ModelBinders
{
    class SlashInValueBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.Model = value.RawValue.ToString().TrimEnd('/');
            return true;
        }
    }
}