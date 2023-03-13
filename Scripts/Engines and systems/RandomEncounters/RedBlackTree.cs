//------------------------------------------------------------------------------
// Author: Joannes Vermorel (joannes@vermorel.com)
// Last Modified: 2002-07-13
// Acknowledgement: Brian Berns (code improvements)
// Copyright © 2003 Joannes Vermorel
//
//  This software is provided 'as-is', without any express or implied warranty. 
//  In no event will the authors be held liable for any damages arising from the 
//  use of this software.
//  
//  Permission is granted to anyone to use this software for any purpose, including 
//  commercial applications, and to alter it and redistribute it freely, subject to the 
//  following restrictions:
//  
//  1. The origin of this software must not be misrepresented; you must not claim that 
//  you wrote the original software. If you use this software in a product, an acknowledgment 
//  (see the following) in the product documentation is required.
//  
//  Portions Copyright © 2003 Joannes Vermorel
//  
//  2. Altered source versions must be plainly marked as such, and must not be 
//  misrepresented as being the original software.
//  
//  3. This notice may not be removed or altered from any source distribution.
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Threading;
//------------------------------------------------------------------------------
namespace System.Collections
{
    internal delegate void RedBlackTreeModifiedHandler();
    //--------------------------------------------------------------------------
    /// <remarks>
    /// RedBlackTree is implemented using black-red binary trees. The
    /// algorithms follows the indications given in the textbook
    /// "Introduction to Algorithms" Thomas H. Cormen, Charles E. 
    /// Leiserson, Ronald L. Rivest
    /// </remarks>
    //--------------------------------------------------------------------------
    public class RedBlackTree : ICollection, IEnumerable
    {
        /// <summary>
        /// Store the number of elements in the RedBlackTree.
        /// </summary>
        private int count;

        /// <summary>
        /// Store the root node of the RedBlackTree.
        /// </summary>
        internal RedBlackTreeNode root;

        /// <summary>
        /// Store the IComparer that allows to compare the node keys.
        /// </summary>
        private IComparer comparer;

        /// <summary>
        /// Store the lock for multiple-reader access and single-writer access.
        /// </summary>
        private ReaderWriterLock rwLock;

        /// <summary>
        /// Store the RedBlackTreeEnumerator which will be called if the
        /// RedBlackTree is modified
        /// </summary>
        private event RedBlackTreeModifiedHandler RedBlackTreeModified;

        /// <summary>
        /// Initializes an new instance of Collections.System.RedBlackTree
        /// class that is empty. A default comparer will be used to
        /// compare the elements added to the RedBlackTree.
        /// </summary>
        public RedBlackTree()
        {
            comparer = System.Collections.Comparer.Default;
            Initialize();
        }

        /// <summary>
        /// Initializes an new instance of Collections.System.RedBlackTree
        /// class that is empty.
        /// </summary>
        /// <param name="comp">
        /// comp represents the IComparer elements which will be used to
        /// sort the elements in RedBlackTree.
        /// </param>
        public RedBlackTree(IComparer comp)
        {
            comparer = comp;
            Initialize();
        }

        /// <summary>
        /// Perform the common initialization taks to all the constructors.
        /// </summary>
        private void Initialize()
        {
            count = 0;
            root = null;
            rwLock = new ReaderWriterLock();
        }

        /// <summary>
        /// Gets the number of elements stored in RedBlackTree.
        /// </summary>
        public int Count
        {
            get 
            {
                return count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the access to RedBlackTree is
        /// synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access
        /// to RedBlackTree
        /// </summary>
        public object SyncRoot
        {
            get 
            {
                return this;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the RedBlackTree has
        /// a fixed size.
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the RedBlackTree is
        /// read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get 
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the highest element stored in the RedBlackTree. The operation
        /// is performed in a guaranteed logarithmic time of the size of RedBlackTree.
        /// </summary>
        public object Max
        {
            get
            {
                RedBlackTreeNode node;

                rwLock.AcquireReaderLock(Timeout.Infinite);

                try 
                {
                    if(root == null) 
                        throw new InvalidOperationException("Unable to return Max because the RedBlackTree is empty.");

                    node = root;
                    while(node.Right != null)
                        node = node.Right;
                }
                finally
                {
                    rwLock.ReleaseReaderLock();
                }

                return node.Key;
            }
        }

        /// <summary>
        /// Gets the lowest element stored in the RedBlackTree. The operation
        /// is performed in a guaranteed logarithmic time of the size of RedBlackTree.
        /// </summary>
        public object Min
        {
            get 
            {
                RedBlackTreeNode node;

                rwLock.AcquireReaderLock(Timeout.Infinite);

                try
                {
                    if(root == null) 
                        throw new InvalidOperationException("Unable to return Min because the RedBlackTree is empty.");

                    node = root;
                    while(node.Left != null)
                        node = node.Left;
                }
                finally
                {
                    rwLock.ReleaseReaderLock();
                }

                return node.Key;
            }
        }

        /// <summary>
        /// Adds an elements to the RedBlackTree. The operation is performed
        /// in a guaranteed logarithmic time of the RedBlackTree size.
        /// </summary>
        public void Add(object x)
        {
            rwLock.AcquireReaderLock(Timeout.Infinite);

            try
            {
                OnRedBlackTreeModified();
                //if(comparer == null) 
                //    throw new ArgumentException("RedBlackTree : not able to compare the elements");

                if(root == null) root = new RedBlackTreeNode(x, null);
                else 
                {
                    // First step : a naive insertion of the element
                    RedBlackTreeNode node1 = root, node2 = null;

                    while(node1 != null)
                    {
                        node2 = node1;
                        if(comparer.Compare(x,node1.Key) < 0) node1 = node1.Left;
                        else node1 = node1.Right;
                    }

                    node1 = new RedBlackTreeNode(x, node2);

                    if(comparer.Compare(x,node2.Key) < 0) node2.Left = node1;
                    else node2.Right = node1;

                    node1.Color = true;

                    // Then : correct the structure of the tree
                    while(node1 != root && node1.Father.Color)
                    {
                        if(node1.Father == node1.Father.Father.Left)
                        {
                            node2 = node1.Father.Father.Right;
                            if(node2 != null && node2.Color)
                            {
                                node1.Father.Color = false;
                                node2.Color = false;
                                node1.Father.Father.Color = true;
                                node1 = node1.Father.Father;
                            }
                            else 
                            {
                                if(node1 == node1.Father.Right)
                                {
                                    node1 = node1.Father;
                                    RotateLeft(node1);
                                }
                                node1.Father.Color = false;
                                node1.Father.Father.Color = true;
                                RotateRight(node1.Father.Father);
                            }
                        } 
                        else 
                        {
                            node2 = node1.Father.Father.Left;
                            if(node2 != null && node2.Color)
                            {
                                node1.Father.Color = false;
                                node2.Color = false;
                                node1.Father.Father.Color = true;
                                node1 = node1.Father.Father;
                            }
                            else 
                            {
                                if(node1 == node1.Father.Left)
                                {
                                    node1 = node1.Father;
                                    RotateRight(node1);
                                }
                                node1.Father.Color = false;
                                node1.Father.Father.Color = true;
                                RotateLeft(node1.Father.Father);
                            }
                        }
                    }
                }

                root.Color = false;

                count++;
            }
            finally 
            {
                rwLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Removes of the elements from the RedBlackTree.
        /// </summary>
        public void Clear()
        {
            rwLock.AcquireWriterLock(Timeout.Infinite);

            try
            {
                OnRedBlackTreeModified();
                root = null;
                count = 0;
            }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Determines whether the RedBlackTree contains a specific object.
        /// The RedBlackTree could contain several identical object. The operation
        /// is performed in a guaranteed logarithmic time of the RedBlackTree size.
        /// </summary>
        public bool Contains(object x)
        {
            // null is always contained in a tree
            if(x == null) return true;

            bool isContained;

            rwLock.AcquireReaderLock(Timeout.Infinite);

            try 
            {
                isContained = (RecContains(root, x) != null);
            }
            finally 
            {
                rwLock.ReleaseReaderLock();
            }

            return isContained;
        }

        /// <summary>
        /// Copies the elements of RedBlackTree to a one dimensional
        /// System.Array at the specified index.
        /// </summary>
        public void CopyTo(Array array, int index)
        {
            // Check the validity of the arguments
            if(array == null) throw new ArgumentNullException();
            if(index < 0) throw new ArgumentOutOfRangeException();
            if(array.Rank > 1 || (array.Length - index) < count)
                throw new ArgumentException();

            rwLock.AcquireReaderLock(Timeout.Infinite);

            try
            {
                RecCopyTo(root, array, index);
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Returns an System.Collection.IEnumerator that can iterate
        /// through the RedBlackTree.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            RedBlackTreeEnumerator tEnum;

            rwLock.AcquireReaderLock(Timeout.Infinite);

            try 
            {
                tEnum = new RedBlackTreeEnumerator(this);
                RedBlackTreeModified += new RedBlackTreeModifiedHandler(tEnum.Invalidate);
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }

            return tEnum;
        }


        /// <summary>
        /// Removes the first occurrence of the element in the RedBlackTree.
        /// The operation is performed in a guaranteed logarithmic time
        /// of the RedBlackTree size.
        /// </summary>
        public void Remove(object x)
        {
            RedBlackTreeNode node;

            rwLock.AcquireWriterLock(Timeout.Infinite);

            try 
            {
                node = RecContains(root, x);
                if(node != null) RemoveRedBlackTreeNode(node);
            }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }

// XXX Added, jk; Find
        public object Find(object x)
        {
            RedBlackTreeNode node;

            rwLock.AcquireWriterLock(Timeout.Infinite);

            try 
            {
                node = RecContains(root, x);
                if(node != null) return node.Key;
                else return null;
            }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// Invalidates the System.Collections.IEnumerator linked
        /// with the RedBlackTree.
        /// </summary>
        private void OnRedBlackTreeModified()
        {
            if(RedBlackTreeModified != null)
            {
                RedBlackTreeModified();
                RedBlackTreeModified = null;
            }
        }

        /// <summary>
        /// Removes a specific node of the RedBlackTree.
        /// </summary>
        /// <param name="node">
        /// node must be contained by RedBlackTree.</param>
        private void RemoveRedBlackTreeNode(RedBlackTreeNode node)
        {
            RedBlackTreeNode nodeX, nodeY, fatherX, fatherY;

            if(node.Left == null || node.Right == null) nodeY = node;
            else nodeY = Successor(node);
            if(nodeY.Left != null) nodeX = nodeY.Left;
            else nodeX = nodeY.Right;

            fatherY = nodeY.Father;
            fatherX = fatherY;
            if(nodeX != null) nodeX.Father = nodeY.Father;

            if(fatherY == null) root = nodeX;
            else 
            {
                if(nodeY == fatherY.Left) fatherY.Left = nodeX;
                else fatherY.Right = nodeX;
            }

            if(nodeY != node) node.Key = nodeY.Key;

            // Remove Correction of the colors
            if(nodeY == null || !nodeY.Color)
            {
                while(nodeX != root && (nodeX == null || !nodeX.Color))
                {
                    if(nodeX == fatherX.Left /*&& nodeX != fatherX.Right*/)
                    {
                        fatherY = fatherX;
                        nodeY = fatherX.Right;
                        if(/*nodeY != null && */nodeY.Color)
                        {
                            nodeY.Color = false;
                            fatherX.Color = true;
                            RotateLeft(fatherX);
                            nodeY = fatherX.Right;
                        }

                        if((nodeY.Left == null || !nodeY.Left.Color) 
                            && (nodeY.Right == null || !nodeY.Right.Color)) 
                        {
                            nodeY.Color = true;
                            nodeX = fatherX;
                            fatherX = fatherX.Father;
                        }
                        else 
                        {
                            if(nodeY.Right == null || !nodeY.Right.Color)
                            {
                                nodeY.Left.Color = false;
                                nodeY.Color = true;
                                RotateRight(nodeY);
                                nodeY = fatherX.Right;
                            }

                            nodeY.Color = fatherX.Color;
                            fatherX.Color = false;
                            nodeY.Right.Color = false;
                            RotateLeft(fatherX);
                            nodeX = root;
                        }
                    } 
                    else
                    {
                        fatherY = fatherX;
                        nodeY = fatherX.Left;
                        if(/*nodeY != null &&*/ nodeY.Color)
                        {
                            nodeY.Color = false;
                            fatherX.Color = true;
                            RotateRight(fatherX);
                            nodeY = fatherX.Left;
                        }

                        if((nodeY.Right == null || !nodeY.Right.Color) 
                            && (nodeY.Left == null || !nodeY.Left.Color))
                        {
                            nodeY.Color = true;
                            nodeX = fatherX;
                            fatherX = fatherX.Father;
                        }
                        else 
                        {
                            if(nodeY.Left == null || !nodeY.Left.Color)
                            {
                                nodeY.Right.Color = false;
                                nodeY.Color = true;
                                RotateLeft(nodeY);
                                nodeY = fatherX.Left;
                            }

                            nodeY.Color = fatherX.Color;
                            fatherX.Color = false;
                            nodeY.Left.Color = false;
                            RotateRight(fatherX);
                            nodeX = root;
                        }
                    }
                } // End While

                if(nodeX != null) nodeX.Color = false;
            } // End Correction

            count--;
        }


        /// <summary>
        /// Returns the node that contains the successor of node.Key.
        /// If such node does not exist then null is returned.
        /// </summary>
        /// <param name="node">
        /// node must be contained by RedBlackTree.</param>
        private RedBlackTreeNode Successor(RedBlackTreeNode node)
        {
            RedBlackTreeNode node1, node2;

            if(node.Right != null)
            {
                // We find the Min
                node1 = node.Right;
                while(node1.Left != null)
                    node1 = node1.Left;
                return node1;
            }

            node1 = node;
            node2 = node.Father;
            while(node2 != null && node1 == node2.Right)
            {
                node1 = node2;
                node2 = node2.Father;
            }
            return node2;
        }


        /// <summary>
        /// Performs a left tree rotation.
        /// </summary>
        /// <param name="node">
        /// node is considered as the root of the tree.</param>
        private void RotateLeft(RedBlackTreeNode node)
        {
            RedBlackTreeNode nodeX = node, nodeY = node.Right;
            nodeX.Right = nodeY.Left;

            if(nodeY.Left != null) nodeY.Left.Father = nodeX;
            nodeY.Father = nodeX.Father;

            if(nodeX.Father == null) root = nodeY;
            else 
            {
                if(nodeX == nodeX.Father.Left)
                    nodeX.Father.Left = nodeY;
                else nodeX.Father.Right = nodeY;
            }

            nodeY.Left = nodeX;
            nodeX.Father = nodeY;
        }


        /// <summary>
        /// Performs a right tree rotation.
        /// </summary>
        /// <param name="node">
        /// node is considered as the root of the tree.</param>
        private void RotateRight(RedBlackTreeNode node)
        {
            RedBlackTreeNode nodeX = node, nodeY = node.Left;
            nodeX.Left = nodeY.Right;

            if(nodeY.Right != null) nodeY.Right.Father = nodeX;
            nodeY.Father = nodeX.Father;

            if(nodeX.Father == null) root = nodeY;
            else 
            {
                if(nodeX == nodeX.Father.Right)
                    nodeX.Father.Right = nodeY;
                else nodeX.Father.Left = nodeY;
            }

            nodeY.Right = nodeX;
            nodeX.Father = nodeY;
        }


        /// <summary>
        /// Copies the element of the tree into a one dimensional
        /// System.Array starting at index.
        /// </summary>
        /// <param name="currentRedBlackTreeNode">The root of the tree.</param>
        /// <param name="array">The System.Array where the elements will be copied.</param>
        /// <param name="index">The index where the copy will start.</param>
        /// <returns>
        /// The new index after the copy of the elements of the tree.
        /// </returns>
        private int RecCopyTo(RedBlackTreeNode currentRedBlackTreeNode, Array array, int index)
        {
            if(currentRedBlackTreeNode != null) 
            {
                array.SetValue(currentRedBlackTreeNode.Key, index);
                return RecCopyTo(currentRedBlackTreeNode.Right, array,
                    RecCopyTo(currentRedBlackTreeNode.Left, array, index + 1));
            }
            else return index;
        }


        /// <summary>
        /// Returns a node of the tree which contains the object
        /// as Key. If the tree does not contain such node, then
        /// null is returned.
        /// </summary>
        /// <param name="node">The root of the tree.</param>
        /// <param name="x">The researched object.</param>
        private RedBlackTreeNode RecContains(RedBlackTreeNode node, object x)
        {
            if(node == null) return null;

            int c = comparer.Compare(x, node.Key);

            if(c == 0) return node;
            if(c < 0) return RecContains(node.Left, x);
            else return RecContains(node.Right, x);
        }


        /// <summary>
        /// For debugging only. Checks whether the RedBlackTree is conform
        /// to the definition of the a red-black tree. If not an
        /// exception is thrown.
        /// </summary>
        /// <param name="node">The root of the tree.</param>
        private int RecConform(RedBlackTreeNode node)
        {
            if(node == null) return 1;

            if(node.Father == null) 
            {
                if(node.Color) throw new ArgumentException("RedBlackTree : the root is not black.");
            } 
            else 
            {
                if(node.Father.Color && node.Color)
                    throw new ArgumentException("RedBlackTree : father and son are red.");
            }

            if(node.Left != null && comparer.Compare(node.Key, node.Left.Key) < 0) 
                throw new ArgumentException("RedBlackTree : order not respected in tree.");
            if(node.Right != null && comparer.Compare(node.Key, node.Right.Key) > 0)
                throw new ArgumentException("RedBlackTree : order not respected in tree.");

            int a = RecConform(node.Left),
                b = RecConform(node.Right);

            if(a < 0 || b < 0) return -1;

            if(a != b) throw new ArgumentException("RedBlackTree : the paths do have not the  same number of black nodes.");

            if(!node.Color) return (a+1);
            else return a;
        }

    }



    /// <remarks>
    /// RedBlackTreeEnumerator could be instancied only through the
    /// RedBlackTree.GetEnumerator method. If the RedBlackTree is modified
    /// after the instanciation of RedBlackTreeEnumerator, then
    /// RedBlackTreeEnumerator become invalid. Any attempt to read or
    /// iterate will throw an exception. The elements contained
    /// in the RedBlackTree are iterated following the order provided
    /// to the RedBlackTree (ascending order).
    /// </remarks>
    public class RedBlackTreeEnumerator : IEnumerator
    {
        /// <summary>
        /// The current node (or null if none)
        /// </summary>
        RedBlackTreeNode current;

        /// <summary>
        /// Reference to the RedBlackTree which has instanciated the
        /// RedBlackTreeEnumerator.
        /// </summary>
        RedBlackTree tree;

        /// <summary>
        /// Store the state of the RedBlackTreeEnumerator. If 
        /// <c>!started</c> then the current position is
        /// before the first element of the RedBlackTree.
        /// </summary>
        bool started;

        /// <summary>
        /// Store the the state of the RedBlackTreeEnumerator. If
        /// <c>!isValid</c>, any attempt to read or iterate 
        /// will throw an exception.
        /// </summary>
        bool isValid;

        /// <summary>
        /// Initializes an new instance of Collections.System.RedBlackTreeEnumerator
        /// class. The current position is before the first element.
        /// </summary>
        /// <param name="t">The RedBlackTree which will be enumerate.</param>
        internal RedBlackTreeEnumerator(RedBlackTree t)
        {
            tree = t;
            started = false;
            isValid = true;
            current = tree.root;
            if(current != null)
            {
                while(current.Left != null)
                    current = current.Left;
            }
        }

        /// <summary>
        /// Gets the current element in the RedBlackTree.
        /// </summary>
        public object Current
        {
            get 
            {
                if(!isValid) throw 
                                 new InvalidOperationException("The RedBlackTree was modified after the enumerator was created");
                if(!started) throw
                                 new InvalidOperationException("Before first element");
                if(current == null) throw
                                        new InvalidOperationException("After last element");
                return current.Key;
            }
        }

        /// <summary>
        /// Advances the RedBlackTreeEnumerator the next element of the RedBlackTree.
        /// Returns whether the move was possible.
        /// </summary>
        public bool MoveNext()
        {
            if(!isValid) throw 
                             new InvalidOperationException("The RedBlackTree was modified after the enumerator was created");
            if(!started) 
            {
                started = true;
                return current != null;
            }
            if(current == null)
                return false;
            if(current.Right == null)
            {
                RedBlackTreeNode prev;
                do
                {
                    prev = current;
                    current = current.Father;
                } while((current != null) && (current.Right == prev));
            }
            else
            {
                current = current.Right;
                while(current.Left != null)
                    current = current.Left;
            }
            return current != null;
        }

        /// <summary>
        /// Sets the enumerator the its initial position which is before
        /// the first element of the RedBlackTree.
        /// </summary>
        public void Reset()
        {
            if(!isValid) throw 
                             new InvalidOperationException("The RedBlackTree was modified after the enumerator was created");
            started = false;
            current = tree.root;
            if(current != null)
            {
                while(current.Left != null)
                    current = current.Left;
            }
        }

        /// <summary>
        /// Invalidates the RedBlackTreeEnumerator.
        /// </summary>
        internal void Invalidate() 
        {
            isValid = false;
        }
    }

    /// <remarks>
    /// RedBlackTreeNode is simple colored binary tree node which
    /// contains a key.
    /// </remarks>
    internal class RedBlackTreeNode
    {
        /// <summary>
        /// References to the other elements of the RedBlackTree.
        /// </summary>
        RedBlackTreeNode father, left, right;

        /// <summary>
        /// Reference to the object contained by the RedBlackTreeNode.
        /// </summary>
        object key;

        /// <summary>
        /// The color of the node (red = true, black = false).
        /// </summary>
        bool color;

        /// <summary>
        /// Initializes an new instance of Collections.System.RedBlackTreeNode
        /// class. All references are set to null.
        /// </summary>
        internal RedBlackTreeNode()
        {
            key = null;
            father = null;
            left = null;
            right = null;
            color = true;
        }

        /// <summary>
        /// Initializes an new instance of Collections.System.RedBlackTreeNode
        /// class and partially insert the RedBlackTreeNode into a tree.
        /// </summary>
        /// <param name="k">Key of the RedBlackTreeNode</param>
        /// <param name="fatherRedBlackTreeNode">The father node of the instanciated RedBlackTreeNode.</param>
        internal RedBlackTreeNode(object k, RedBlackTreeNode fatherRedBlackTreeNode)
        {
            key = k;
            father = fatherRedBlackTreeNode;
            left = null;
            right = null;
            color = true;
        }

        /// <summary>
        /// Gets or sets the key of the RedBlackTreeNode.
        /// </summary>
        internal object Key 
        {
            get
            {
                return key;
            }
            set 
            {
                key = value;
            }
        }

        /// <summary>
        /// Gets or sets the father of the RedBlackTreeNode.
        /// </summary>
        internal RedBlackTreeNode Father
        {
            get 
            {
                return father;
            }
            set 
            {
                father = value;
            }
        }

        /// <summary>
        /// Gets or sets the left children of the RedBlackTreeNode.
        /// </summary>
        internal RedBlackTreeNode Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        /// <summary>
        /// Gets or sets the right children of the RedBlackTreeNode.
        /// </summary>
        internal RedBlackTreeNode Right
        {
            get
            {
                return right;
            }
            set 
            {
                right = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the RedBlackTreeNode.
        /// </summary>
        internal bool Color
        {
            get 
            {
                return color;
            }
            set 
            {
                color = value;
            }
        }
    }
}
