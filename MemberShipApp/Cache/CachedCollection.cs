using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Cache
{
    public class CachedCollection<T> : IEnumerable<T>
    {
        public delegate Dictionary<int, T> GetData();

        private Dictionary<int, T> data = null;
        private readonly GetData getData;

        public CachedCollection(GetData getData)
        {
            this.getData = getData;

            data = getData();
        }

        public T this[int id]
        {
            get
            {
                try
                {
                    return data[id];
                }
                catch
                {
                    // May have hit a null reference exception or a key not found exception, in either case we try again... if THIS throws, just let it throw to the caller
                    data = getData();

                    return data[id];
                }
            }
        }

        public void Invalidate()
        {
            data = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (data == null)
            {
                data = getData();
            }

            return data.Select(kvp => kvp.Value).GetEnumerator();
        }



        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (data == null)
            {
                data = getData();
            }

            return data.Select(kvp => kvp.Value).GetEnumerator();
        }
    }

}
