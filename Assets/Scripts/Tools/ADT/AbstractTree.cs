using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Assets.Scripts.Tools.ADT
{
    abstract class AbstractTree<E>: ITree<E>
    {
        public List<E> Root()
        {
            throw new NotImplementedException();
        }

        public List<E> Parent(List<E> p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List<E>> Chilcren(List<E> children)
        {
            throw new NotImplementedException();
        }

        public int numChildren(List<E> childrenList)
        {
            throw new NotImplementedException();
        }

        public bool isInternal(List<E> childrenList)
        {
            return numChildren(childrenList) >0;
        }

        public bool isExternal(List<E> childrenList)
        {
            return numChildren(childrenList) == 0;
        }

        public bool isRoot(List<E> element)
        {
            return element == Root();
        }

        public int size()
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public List<E> iterator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<E> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int Depth(List<E> p)
        {
            if (isRoot(p))
                return 0;
            else
                return 1 + Depth((Parent(p)));

        }

        public int Height(List<E> p)
        {
            int h = 0;
            foreach (List<E> c in Chilcren(p))
                h = Math.Max(h, 1 + Height(c));
            return h;

        }
    }
}
