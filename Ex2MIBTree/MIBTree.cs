using System.Collections.Generic;
using System.Linq;

namespace AD
{
    public class MIBTree : BinarySearchTree<MIBNode>
    {
        public MIBTree()
        {
            Insert(new MIBNode("1.3.6.1.4.1.9", "cisco"));
            Insert(new MIBNode("1.3.6.1.1.1.1", "system"));
            Insert(new MIBNode("1.3.6", "dod"));
            Insert(new MIBNode("1.3.6.1.1.1.4", "ip"));
            Insert(new MIBNode("1.3.6.1.3", "experimental"));
            Insert(new MIBNode("1.3.6.1.4.1", "enterprise"));
            Insert(new MIBNode("1.3.6.1.1.1.2", "interfaces"));
            Insert(new MIBNode("1.3.6.1.1", "directory"));
            Insert(new MIBNode("1.3", "org"));
            Insert(new MIBNode("1.3.6.1.4.1.2636", "juniperMIB"));
            Insert(new MIBNode("1.3.6.1.4.1.311", "microsoft"));
            Insert(new MIBNode("1.3.6.1", "internet"));
            Insert(new MIBNode("1", "iso"));
            Insert(new MIBNode("1.3.6.1.4", "private"));
            Insert(new MIBNode("1.3.6.1.1.1", "mib-2"));
            Insert(new MIBNode("1.3.6.1.2", "mgmt"));
        }

        public MIBNode FindNode(string oid)
        {
            BinaryNode<MIBNode> node = FindThisNode(root, new MIBNode(oid, null));
            return (node != null) ? node.data : null ;
        }

        public bool AllNodesAvailable(string oid)
        {
            return AllNodesAvailable(oid.Split(".").ToList());
        }

        private bool AllNodesAvailable(List<string> oid)
        {
            if (oid.Count == 0)
            {
                return true;
            }

            var newOid =  oid.Aggregate((first, second) => first + "." + second);

            var node = FindNode(newOid);

            oid.RemoveAt(oid.Count - 1);

            return (node != null) ? AllNodesAvailable(oid) : false;
        }
    }
}
