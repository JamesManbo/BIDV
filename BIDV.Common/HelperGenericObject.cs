using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDV.Common
{
    public class HelperGenericObject
    {
        /// <summary>
        /// Get Value' Field by Name in Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Value</returns>
        public static object GetValueByName<T>(T obj, string fieldName)
        {
            var t = obj.GetType();

            var prop = t.GetProperty(fieldName);

            return prop.GetValue(obj);
        }
        /// <summary>
        /// Get Value' Field by Name in Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns>Value</returns>
        public static object GetValueByName(object obj, string fieldName)
        {
            var t = obj.GetType();

            var prop = t.GetProperty(fieldName);

            return prop.GetValue(obj);
        }
        /// <summary>
        /// Copy data từ object cũ sang object mới
        /// Áp dụng cho trường hợp Custom Model
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="objSource"></param>
        /// <param name="objTarget"></param>
        public static void CopyDataObj2Obj<T1, T2>(T1 objSource, T2 objTarget)
        {
            // Lấy danh sách properties từ object
            var lstpropSourceName = GetPropertiesNameOfClass(objSource);
            foreach (var propName in lstpropSourceName)
            {
                var val = GetValueByName(objSource, propName);
                var prop = objSource.GetType().GetProperty(propName);
                if (prop == null)
                {
                    continue;
                }
                try
                {
                    prop.SetValue(objTarget, val, null);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// Lấy list các thuộc tính trong object
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public static List<string> GetPropertiesNameOfClass<T>(T pObject)
        {
            var propertyList = new List<string>();
            if (pObject != null)
            {
                propertyList.AddRange(pObject.GetType().GetProperties().Select(prop => prop.Name));
            }
            return propertyList;
        }
    }
}

