using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Feng.Forms.EventHandlers
{
   
        public delegate void CloseHandler();
        public delegate void ObjectNullEventEventHandler();
        public delegate void ObjectEventHandler<T>(T sender); 
        public delegate void ObjectEventHandler<T, T1>(T sender, T1 value1);
        public delegate void ObjectEventHandler<T, T1, T2>(T sender, T1 value1, T2 value2);
        public delegate void ObjectEventHandler<T, T1, T2, T3>(T sender, T1 value1, T2 value2, T3 value3);
        public delegate void ObjectEventHandler<T, T1, T2, T3, T4>(T sender, T1 value1, T2 value2, T3 value3, T4 value4);
        public delegate void ObjectEventHandler<T, T1, T2, T3, T4, T5>(T sender, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5);
        public delegate void ObjectEventHandler<T, T1, T2, T3, T4, T5, T6>(T sender, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6);
        public delegate void ObjectEventHandler<T, T1, T2, T3, T4, T5, T6, T7>(T sender, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7);
        public delegate void ObjectEventHandler(object sender);
        public delegate void ObjectValueEventHandler(object sender,object value);
        public delegate void ObjectIntEventEventHandler(object sender, int value);
        public delegate void ObjectStringEventEventHandler(object sender, string value);
        public delegate void ObjectDatetimeEventEventHandler(object sender, DateTime value);
        public delegate void ObjectBoolEventEventHandler(object sender, bool value);
        public delegate void ObjectDecimalEventEventHandler(object sender, decimal value);
        public delegate void ObjectFloatEventEventHandler(object sender, float value);

        public delegate void IntEventEventHandler(int value);
        public delegate void StringEventEventHandler(string value);
        public delegate void StringEventEventHandler2(string value1, string value2);
        public delegate void DatetimeEventEventHandler(DateTime value);
        public delegate void BoolEventEventHandler(bool value);
        public delegate void DecimalEventEventHandler(decimal value);
        public delegate void FloatEventEventHandler(float value);
        public delegate void CancelEventHandler(object sender, CancelEventArgs e);
        public delegate void CancelValueEventHandler(object sender, object value, CancelEventArgs e);
        public delegate void CancelEventHandler<T>(object sender, T value, CancelEventArgs e);
        public delegate void ValueChangedEventHandler(object sender, object value);
  

}
