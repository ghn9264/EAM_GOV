using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAM.Data.Comm
{
    public class ShallowCopyHelper
    {
        public static void CopyPropertiesValue(object objFrom, object objTo)
        {
            if (null == objFrom)
            {
                return;
            }

            if (null == objTo)
            {
                return;
            }

            Type typeFrom = objFrom.GetType();
            Type typeTo = objTo.GetType();

            if (objFrom is IList)
            {
                try
                {
                    int count = (objFrom as IList).Count;
                    for (int i = 0; i < count; i++)
                    {
                        CopyPropertiesValue((objFrom as IList)[i], (objTo as IList)[i]);
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
            else
            {
                foreach (System.Reflection.PropertyInfo pi in
                   typeFrom.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    try
                    {
                        object valueFrom = typeFrom.GetProperty(pi.Name).GetValue(objFrom, null);
                        object valueTo = typeTo.GetProperty(pi.Name).GetValue(objTo, null);

                        if (typeFrom.GetProperty(pi.Name).PropertyType.IsClass
                            && !typeFrom.GetProperty(pi.Name).PropertyType.IsPrimitive
                            && !(valueFrom is String))
                        {
                            CopyPropertiesValue(valueFrom, valueTo);
                        }
                        else
                        {
                            if (valueFrom == null || !valueFrom.Equals(valueTo))
                            {
                                //Set value to latest data
                                typeTo.GetProperty(pi.Name).SetValue(objTo, valueFrom, null);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }

                }
            }
        }
    }
}
