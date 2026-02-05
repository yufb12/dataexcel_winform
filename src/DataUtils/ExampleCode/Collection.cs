//public class CharViewCollection : IList<CharView>
//{
//    public List<CharView> list = new List<CharView>();

//    public int IndexOf(CharView item)
//    {
//        return list.IndexOf(item);
//    }

//    public void Insert(int index, CharView item)
//    {
//        list.Insert(index, item);
//    }

//    public void RemoveAt(int index)
//    {
//        list.RemoveAt(index);
//    }

//    public CharView this[int index]
//    {
//        get
//        {
//            return this.list[index];
//        }
//        set
//        {
//            list[index] = value;
//        }
//    }

//    public void Add(CharView item)
//    {
//        list.Add(item);
//    }

//    public void Clear()
//    {
//        list.Clear();
//    }

//    public virtual bool Contains(CharView item)
//    {
//        return list.Contains(item);
//    }

//    public void CopyTo(CharView[] array, int arrayIndex)
//    {
//        this.list.CopyTo(array, arrayIndex);
//    }

//    public int Count
//    {
//        get { return this.list.Count; }
//    }

//    public virtual bool IsReadOnly
//    {
//        get { return false; }
//    }

//    public virtual bool Remove(CharView item)
//    {
//        return this.list.Remove(item);
//    }

//    public IEnumerator<CharView> GetEnumerator()
//    {
//        return this.list.GetEnumerator();
//    }

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//    {
//        return this.list.GetEnumerator();
//    }

//    public void AddRange(params CharView[] ts)
//    {
//        foreach (CharView c in ts)
//        {
//            this.Add(c);
//        }
//    }
//}