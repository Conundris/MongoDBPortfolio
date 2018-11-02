using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace WebApplication1
{
    public class ObjectIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            //if (context.Metadata.IsComplexType) return null;  
  
            var propName = context.Metadata.PropertyName;  
            if (propName == null) return null;  
  
            var propInfo = context.Metadata.ContainerType.GetProperty(propName);  
            if (propInfo == null) return null;  
  
            var attribute = propInfo.GetCustomAttributes(  
                typeof(MongoObjectID), false).FirstOrDefault();

            var test = propInfo.GetGetMethod();
            
            return attribute == null ? null : new BinderTypeModelBinder(typeof(ObjectIdBinder));
        }
    }
}