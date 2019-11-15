using System;

namespace AD
{
    public class Edge : IComparable<Edge>
    {
        public Vertex dest;
        public double cost;

        public Edge(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }

        public int CompareTo(Edge obj)
        {
            if (obj == null) return 0;

            return cost.CompareTo(obj.cost);
        }

        public override string ToString()
        {
            return $"{dest.name} ({cost})";
        }
    }
}