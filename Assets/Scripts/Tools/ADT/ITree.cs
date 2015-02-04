using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tools.ADT
{
    public interface ITree<T>: IEnumerable<T>
    {
        List<T> Root();
        List<T> Parent(List<T> p);
        IEnumerable<List<T>> Chilcren(List<T> children);
        int numChildren(List<T> childrenList);
        bool isInternal(List<T> childrenList);
        bool isExternal(List<T> childrenList);
        bool isRoot(List<T> childrenList);
        int getSize();
        bool isEmpty();
        List<T> iterator();


    }
}
