using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tools.ADT
{
    class Tree<E>: AbstractTree<E>
    {
        #region class variables and properties
        protected Node<E> _root =  null;    //root of the tree
        private int _size = 0;
        protected Node<E> Root
        {
            get { return _root; }
            set { _root = value; }
        }


        public int size
        {
            get { return _size; }
            set { _size = value; }
        }

        #endregion
        /// <summary>
        /// factory function to create a new node storing element
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parent"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        protected Node<E> CreateNode(E e, Node<E> parent, List<Node<E>> children)
        {
            return new Node<E>(e,parent,children);
        }

        #region constructor

        public Tree()
        {
        }

        #endregion

        #region class functions

        private Node<E> Validate(List<E> p)
        {
            if (! (p is Node<E>) )
                throw new ArgumentException("Not a valid type");
            Node<E> node = (Node<E>) p;
            if(node.Parent == node)
                throw new ArgumentException("p is no longer in the tree");
            return node;
        }

        public List<E> Parent(List<E> p)
        {
            Node<E> node = Validate(p);
            return node.Parent;
        }

        public List<Node<E>> Children(List<E> p)
        {
            Node<E> node = Validate(p);
            return node.Children;
        }

        public List<E> AddRoot(E e)
        {
            if (!isEmpty())
                throw new InvalidOperationException("Tree isn't empty");
            Root = CreateNode(e, null, null);
            size = 1;
            return Root;
        }

        public List<E> AddChild(List<E> p, E e)
        {
            Node<E> parent = Validate(p);
            if (parent.Children != null)
                throw new ArgumentException("Parent already has a list of children");
            Node<E> child = CreateNode(e, parent, null);
            parent.Children.Add(child); ;
            size++;
            return child;
        }

        /// <summary>
        /// attaches tree t1 and t2 as left and right subtrees of external p
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public void Attach(List<E> p, Tree<E> t1, Tree<E> t2)
        {
            Node<E> node = Validate(p);
            if(isInternal(p)) 
                throw new ArgumentException("p must be a leaf!");
            size += t1.size + t2.size;

            if (!t1.isEmpty())
            {
                t1.Root.Parent = node;
                node.Children.Add(t1.Root);
                t1.Root = null;
                t1.size = 0;
            }

            if (!t2.isEmpty())
            {
                t2.Root.Parent = node;
                node.Children.Add(t2.Root);
                t2.Root = null;
                t2.size = 0;
            }
        }
        #endregion

    }

    partial class Node<E> : List<E>
    {
        #region class variables and properties
        private E element;                  //element stored at this node
        private Node<E> parent;             //parent of the node(if any)
        private List<Node<E>> children;     //List of childen of the node class
     
        public E Element
        {
            get { return element; }
            set { element = value; }
        }

        public Node<E> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public List<Node<E>> Children
        {
            get { return children; }
            set { children = value; }
        }

        #endregion
        #region class constructors
        public Node(E element, Node<E> parent, List<Node<E>> children)
        {
            this.element = element;
            this.parent = parent;
            this.children = children;
        }



        #endregion
    }
}
