using System.Collections.Generic;

namespace AD
{
    public class BinarySearchTree<T> : BinaryTree<T>, IBinarySearchTree<T>
        where T : System.IComparable<T>
    {

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!
        //!! Vul deze klasse met je eigen BinarySearchTree
        //!!
        //!! LET OP!
        //!! De namespace is "AD".
        //!! Waarschijnlijk zijn je uitwerkingen van het huiswerk nog "Huiswerk5"
        //!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        public void Insert(T x)
        {
            root = Insert(root, x);
        }

        private BinaryNode<T> Insert(BinaryNode<T> node, T x)
        {
            if (node == null)
            {
                return new BinaryNode<T>(x, null, null);
            }
            else if (node.data.Equals(x))
            {
                throw new BinarySearchTreeDoubleKeyException();
            }
            else if (node.data.CompareTo(x) > 0)
            {
                node.left = Insert(node.left, x);
                return node;
            }
            else
            {
                node.right = Insert(node.right, x);
                return node;
            }
        }

        public T FindMin()
        {
            if (IsEmpty()) throw new BinarySearchTreeEmptyException();

            return FindMin(root).data;
        }

        private BinaryNode<T> FindMin(BinaryNode<T> node)
        {
            if (node.left != null)
            {
                return FindMin(node.left);
            }
            else
            {
                return node;
            }
        }

        public void RemoveMin()
        {
            if (IsEmpty())
            {
                throw new BinarySearchTreeEmptyException();
            }

            RemoveMin(root, null);
        }

        private void RemoveMin(BinaryNode<T> node, BinaryNode<T> parent)
        {
            if (node.left != null)
            {
                RemoveMin(node.left, node);
                return;
            }

            if (parent == null)
            {
                MakeEmpty();
                return;
            }

            if (node.right != null)
            {
                parent.left = node.right;
            }
            else
            {
                parent.left = null;
            }
        }

        public void RemoveNode(BinaryNode<T> rootNode, BinaryNode<T> node)
        {
            BinaryNode<T> parent = FindParent(rootNode, node.data);

            if (parent == null)
            {
                root = null;

                throw new BinarySearchTreeElementNotFoundException();
            }

            bool isLeft = node.data.CompareTo(parent.data) < 0;

            if (node.left != null && node.right != null)
            {
                T replacementValue = isLeft ? FindMax(node.right) : FindMin(node.left).data;

                Remove(node, replacementValue);
                node.data = replacementValue;

                return;
            }

            node = (node.left != null ^ node.right != null) ? node.left ?? node.right : null;

            if (isLeft) { parent.left = node; } else { parent.right = node; }
        }

        public void Remove(BinaryNode<T> rootNode, T x) => RemoveNode(rootNode, FindThisNode(root, x));
        public void Remove(T x) => Remove(root, x);

        public BinaryNode<T> FindParent(BinaryNode<T> rootNode, T value)
        {
            BinaryNode<T> node = rootNode;
            BinaryNode<T> parent = null;

            while (node != null)
            {
                if (node.data.CompareTo(value) == 0) { return parent; }

                parent = node;
                node = value.CompareTo(node.data) < 0 ? node.left : node.right;
            }

            return parent;
        }

        public T FindMax(BinaryNode<T> rootNode)
        {
            if (IsEmpty()) throw new BinarySearchTreeEmptyException();

            BinaryNode<T> node = rootNode;

            while (node.right != null) { node = node.right; }

            return node.data;
        }

        public string InOrder()
        {
            LinkedList<T> list = new LinkedList<T>();

            TraverseInOrder(node => list.AddLast(node.data));

            return string.Join(" ", list);

        }

        public override string ToString()
        {
            return IsEmpty() ? "" : InOrder();
        }

        public BinaryNode<T> FindThisNode(BinaryNode<T> node, T x)
        {
            if (node == null)
                return null;

            if (node.data.CompareTo(x) == 0)
                return node;

            return node.data.CompareTo(x) > 0 ? FindThisNode(node.left, x) : FindThisNode(node.right, x);
        }
    }
}
