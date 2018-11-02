using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace WebApplication1
{
    public class ObjectIdBinder : IModelBinder
    {
/*        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            
            if (result == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            
            ObjectId.Parse((string)result)
            
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result);
            
            ValueProviderResult
            
            if (result == null)
            {
                return ObjectId.Empty;
            }
            return ;
        }*/

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            
            var valueProviderResult =  
                bindingContext.ValueProvider.GetValue("zid");  
  
            if (valueProviderResult == ValueProviderResult.None)  
                return Task.CompletedTask;  
  
            bindingContext.ModelState.SetModelValue(  
                bindingContext.ModelName, valueProviderResult);  
  
            var result = ObjectId.Parse(valueProviderResult.FirstValue); 
  
            bindingContext.Result = ModelBindingResult.Success(result);  
            return Task.CompletedTask;  
            
            /*var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (result == null)
            {
                return Task.CompletedTask;
            }

            ObjectId.Parse((string) result);
            bindingContext.
           
            return Task.CompletedTask;
            
            throw new System.NotImplementedException();*/
        }
    }
}
