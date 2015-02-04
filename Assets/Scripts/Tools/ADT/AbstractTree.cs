using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Assets.Scripts.Tools.ADT
{
    abstract class AbstractTree<E>: ITree<E>
    {
        public int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

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

   //     public int size()
     //   {
       //     return _size;
       // }

        public bool isEmpty()
        {
            return size == 0;
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

        List<E> ITree<E>.Root()
        {
            throw new NotImplementedException();
        }

        List<E> ITree<E>.Parent(List<E> p)
        {
            throw new NotImplementedException();
        }

        IEnumerable<List<E>> ITree<E>.Chilcren(List<E> children)
        {
            throw new NotImplementedException();
        }

        int ITree<E>.numChildren(List<E> childrenList)
        {
            throw new NotImplementedException();
        }

        bool ITree<E>.isInternal(List<E> childrenList)
        {
            throw new NotImplementedException();
        }

        bool ITree<E>.isExternal(List<E> childrenList)
        {
            throw new NotImplementedException();
        }

        bool ITree<E>.isRoot(List<E> childrenList)
        {
            throw new NotImplementedException();
        }

        int ITree<E>.getSize()
        {
            throw new NotImplementedException();
        }

        bool ITree<E>.isEmpty()
        {
            throw new NotImplementedException();
        }

        List<E> ITree<E>.iterator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<E> IEnumerable<E>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
