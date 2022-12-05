﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NewTablesLibrary
{
    public class ManyToOne<T, ParentT> where T : Cell where ParentT : Cell
    {
        private readonly ParentT _parent;
        private T _value;

        public ManyToOne(ParentT parent)
        {
            _parent = parent;
        }

        public T Value => _value;

        internal void InnerSetValue(T value)
        {
            _value = value;
        }

        private OneToManyCollection<ParentT, T> GetOneToManyField(T value)
        {
            Type type = value.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance | BindingFlags.NonPublic);

            IEnumerable<FieldInfo> interFields = fields.Where(x =>
                x.GetValue(value).GetType() == typeof(OneToManyCollection<ParentT, T>));

            FieldInfo interField = interFields.First();
            return interField.GetValue(value) as OneToManyCollection<ParentT, T>;
        }


        /*
        public void SetValue(T value)
        {
            RemoveFromPrevious();



        }

        private void RemoveFromPrevious()
        {
            if (_value != null)
            {
                OneToManyCollection<ParentT, T> collection = GetOneToManyField(_value);
                collection.InnerRemove(_parent);
            }
        }
        */
    }
}