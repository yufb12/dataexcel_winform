using System; 
namespace Feng.Data
{
    public class DataTag
    {
        private object _obj1;
        private object _obj2;
        private object _obj3;
        private object _obj4;
        public DataTag(object obj1, object obj2, object obj3, object obj4)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _obj3 = obj3;
            _obj4 = obj4;
        }

        public DataTag(object obj1, object obj2, object obj3)
        {
            _obj1 = obj1;
            _obj2 = obj2;
            _obj3 = obj3;
        }

        public DataTag(object obj1, object obj2)
        {
            _obj1 = obj1;
            _obj2 = obj2;
        }

        public object Obj1
        {
            get {
                return _obj1;
            }
            set {
                _obj1 = value;
            }

        }
        public object Obj2
        {
            get
            {
                return _obj2;
            }
            set
            {
                _obj2 = value;
            }

        }
        public object Obj3
        {
            get
            {
                return _obj3;
            }
            set
            {
                _obj3 = value;
            } 
        }
        public object Obj4
        {
            get
            {
                return _obj4;
            }
            set
            {
                _obj4 = value;
            }
        }
    }
}
