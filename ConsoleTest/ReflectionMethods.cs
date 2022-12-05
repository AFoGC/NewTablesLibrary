using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public static class ReflectionMethods
    {
        public static void TestReflection()
        {
            //Inter<string> inter = new Inter<string>();
            HObject hObject = new HObject();
            object obj = hObject;

            ReflectionMethods.SetInterValue(obj, "H");
            ReflectionMethods.SetInterValue(obj, 23);

            Console.WriteLine(hObject.SInter.Value);
            Console.WriteLine(hObject.IInter.Value);
        }

        public static void SetInterValue<T>(object obj, T value)
        {
            Inter<T> inter = GetInterField<T>(obj);
            inter.SetValue(value);
        }

        public static Inter<T> GetInterField<T>(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.GetValue(obj).GetType() == typeof(Inter<T>));

            FieldInfo interField = interFields.First();
            return interField.GetValue(obj) as Inter<T>;
        }

        public class HObject
        {
            private readonly Inter<string> _sinter = new Inter<string>();
            private readonly Inter<int> _iinter = new Inter<int>();
            public Inter<string> SInter => _sinter;
            public Inter<int> IInter => _iinter;
        }

        public class Inter<T>
        {
            private T _value;

            public T Value => _value;

            internal void SetValue(T value)
            {
                _value = value;
            }
        }
    }
}
