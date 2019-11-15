using System;

namespace AD
{
    public class BinaryTree<T> : IBinaryTree<T>
    {
        public BinaryNode<T> root;

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!
        //!! Vul deze klasse met je eigen BinaryTree
        //!!
        //!! LET OP!
        //!! De namespace is "AD".
        //!! Waarschijnlijk zijn je uitwerkingen van het huiswerk nog "Huiswerk5"
        //!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------

        public BinaryTree()
        {

        }

        public BinaryTree(T rootItem)
        {
            root = new BinaryNode<T>(rootItem, null, null);
        }


        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        public BinaryNode<T> GetRoot()
        {
            return root;
        }

        public int Size()
        {
            if (root == null) return 0;

            return SearchSize(root);
        }

        private int SearchSize(BinaryNode<T> node)
        {
            int count = 0;

            if (node.left != null)
            {
                count += SearchSize(node.left);
            }
            if (node.right != null)
            {
                count += SearchSize(node.right);
            }

            return count + 1;
        }

        public int Height()
        {
            if (root == null) return -1;

            return FindHeight(root);
        }

        private int FindHeight(BinaryNode<T> node, int d = 0)
        {
            int left = 0;
            int right = 0;

            if (node.left != null)
            {
                left = FindHeight(node.left) + 1;
            }
            if (node.right != null)
            {
                right = FindHeight(node.right) + 1;
            }

            if (left == right && left == 0)
            {
                return left;
            }

            return (left < right) ? right : left;
        }

        public void MakeEmpty()
        {
            root = null;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void Merge(T rootItem, BinaryTree<T> t1, BinaryTree<T> t2)
        {
            BinaryNode<T> root1;
            if (t1 != null)
            {
                root1 = t1.root;
            }
            else
            {
                root1 = null;
            }

            BinaryNode<T> root2;
            if (t2 != null)
            {
                root2 = t2.root;
            }
            else
            {
                root2 = null;
            }

            root = new BinaryNode<T>(rootItem, root1, root2);
        }

        public string ToPrefixString()
        {
            if (root == null) return "NIL";

            return PrefixString(root);
        }

        private string PrefixString(BinaryNode<T> node)
        {
            if (node == null) return "NIL";

            return $"[ {node.data} {PrefixString(node.left)} {PrefixString(node.right)} ]";
        }

        public string ToInfixString()
        {
            if (root == null) return "NIL";

            return InfixString(root);
        }

        private string InfixString(BinaryNode<T> node)
        {
            if (node == null) return "NIL";

            return $"[ {InfixString(node.left)} {node.data} {InfixString(node.right)} ]";
        }

        public string ToPostfixString()
        {
            if (root == null) return "NIL";

            return PostfixString(root);
        }

        private string PostfixString(BinaryNode<T> node)
        {
            if (node == null) return "NIL";

            return $"[ {PostfixString(node.left)} {PostfixString(node.right)} {node.data} ]";
        }


        //----------------------------------------------------------------------
        // Interface methods : methods that have to be implemented for homework
        //----------------------------------------------------------------------

        public int NumberOfLeaves()
        {
            if (root == null) return 0;

            return findLeaves(root);
        }

        private int findLeaves(BinaryNode<T> node)
        {
            if (node == null) return 0;

            if (node.left == null && node.right == null)
            {
                return 1;
            }

            return findLeaves(node.left) + findLeaves(node.right);
        }

        public int NumberOfNodesWithOneChild()
        {
            if (root == null) return 0;

            return FindSingleChildParents(root);
        }

        private int FindSingleChildParents(BinaryNode<T> node)
        {
            if (node == null) return 0;

            int counter = 0;
            if ((node.left == null && node.right != null) || (node.left != null && node.right == null))
            {
                counter = 1;
            }

            return counter + FindSingleChildParents(node.left) + FindSingleChildParents(node.right);
        }

        public int NumberOfNodesWithTwoChildren()
        {
            if (root == null) return 0;

            return FindDoubleChildParents(root);
        }

        private int FindDoubleChildParents(BinaryNode<T> node)
        {
            if (node == null) return 0;

            int counter = 0;
            if (node.right != null && node.left != null)
            {
                counter = 1;
            }

            return counter + FindDoubleChildParents(node.left) + FindDoubleChildParents(node.right);
        }

        public void TraverseInOrder(Action<BinaryNode<T>> callback)
        {
            void TraverseNode(BinaryNode<T> node)
            {
                if (node == null) return;

                TraverseNode(node.left);

                callback(node);

                TraverseNode(node.right);
            }

            TraverseNode(root);
        }
    }
}