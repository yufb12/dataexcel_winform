using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace Feng.Utils
{ 
    public class CompareHelper
    {
        public static int Compare(int oldvalue, int newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(uint oldvalue, uint newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(short oldvalue, short newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(ushort oldvalue, ushort newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(long oldvalue, long newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(ulong oldvalue, ulong newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(byte oldvalue, byte newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(sbyte oldvalue, sbyte newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(decimal oldvalue, decimal newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(float oldvalue, float newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(double oldvalue, double newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(DateTime oldvalue, DateTime newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }
 
        public static int Compare(char oldvalue, char newvalue)
        {
            if (oldvalue > newvalue)
            {
                return 1;
            }
            else if (oldvalue < newvalue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(string oldvalue, string newvalue)
        {
            return string.Compare(oldvalue, newvalue);
        }

        public static int Compare(bool oldvalue, bool newvalue)
        {
            if (oldvalue == newvalue)
            {
                return 0;
            }
            if (oldvalue == true)
            {
                return 1;
            }
            else if (oldvalue == false)
            {
                return -1;
            }
            return 0;
        }
 
        public static int Compare(DateTime? oldvalue, DateTime? newvalue)
        {
            DateTime value1 = DateTime.MinValue;
            DateTime value2 = DateTime.MinValue;
            if (oldvalue.HasValue)
            {
                value1 = oldvalue.Value;
            }
            if (newvalue.HasValue)
            {
                value2 = newvalue.Value;
            }

            if (value1 > value2)
            {
                return 1;
            }
            else if (value1 < value2)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(int? oldvalue, int? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            { 
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(uint? oldvalue, uint? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(short? oldvalue, short? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(ushort? oldvalue, ushort? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(long? oldvalue, long? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(ulong? oldvalue, ulong? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(byte? oldvalue, byte? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(sbyte? oldvalue, sbyte? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(decimal? oldvalue, decimal? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(float? oldvalue, float? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(double? oldvalue, double? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }

        public static int Compare(char? oldvalue, char? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue > newvalue)
                {
                    return 1;
                }
                else if (oldvalue < newvalue)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }
        
        public static int Compare(bool? oldvalue, bool? newvalue)
        {
            if (oldvalue.HasValue && newvalue.HasValue)
            {
                if (oldvalue.Value && (!newvalue.Value))
                {
                    return 1;
                }
                else if ((!oldvalue.Value) && newvalue.Value)
                {
                    return -1;
                }
            }
            else if (oldvalue.HasValue && (!newvalue.HasValue))
            {
                return 1;
            }
            else if ((!oldvalue.HasValue) && newvalue.HasValue)
            {
                return -1;
            }
            return 0;
        }
    } 
}
