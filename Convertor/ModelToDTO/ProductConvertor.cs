using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.ModelToDTO
{
    public static class ProductConvertor<T,K>
    {
        public static IEnumerable<T> ConvertFromModelToDTO(IEnumerable<K> model)
        {
            List<T> dto = new List<T>();
            Type modelsType = typeof(K);
            Type destType = typeof(T);
            var modProps = modelsType.GetProperties();
            var destProps = destType.GetProperties();
            string modelName = modelsType.Name;
            string destName = destType.Name;
            PropertyInfo Id = destType.GetProperty("Id");
            if (Id == null)
                Id = destType.GetProperty(destName + "Id");
            PropertyInfo Name = destType.GetProperty("Name");
            PropertyInfo Price = destType.GetProperty("Price");
            PropertyInfo Description = destType.GetProperty("Description");
            PropertyInfo CatId = destType.GetProperty("CategoryId");
            //PropertyInfo Some = destType.GetProperty("SomeProp");

            PropertyInfo ModId = modelsType.GetProperty("Id");
            if (ModId == null)
            {
                ModId = modelsType.GetProperty(modelName + "Id");
            }
            PropertyInfo ModName = modelsType.GetProperty("Name");
            PropertyInfo ModDescription = modelsType.GetProperty("Description");
            PropertyInfo ModCatId = modelsType.GetProperty("CategoryId");
            PropertyInfo ModPrice = modelsType.GetProperty("Price");
            //PropertyInfo ModSome = modelsType.GetProperty("Some");

            foreach (var products in model)
            {
                var t = Activator.CreateInstance<T>();
                Id?.SetValue(t, ModId?.GetValue(products));
                Name?.SetValue(t, ModName?.GetValue(products));
                CatId?.SetValue(t, ModCatId?.GetValue(products));
                Description?.SetValue(t, ModDescription?.GetValue(products));
                Price?.SetValue(t, ModPrice?.GetValue(products));
                //CatId?.SetValue(t, ModSome?.GetValue(products));
                dto.Add(t);
            }

            return dto;
        }
    }
}
