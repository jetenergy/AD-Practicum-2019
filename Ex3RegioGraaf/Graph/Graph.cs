using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AD
{
    public class Graph : IGraph
    {
        public static readonly double INFINITY = System.Double.MaxValue;

        protected Dictionary<string, Vertex> vertexMap;


        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!
        //!! Vul deze klasse met je eigen Graph en vul aan
        //!!
        //!! LET OP!
        //!! De namespace is "AD".
        //!! Waarschijnlijk zijn je uitwerkingen van het huiswerk nog "Huiswerk6"
        //!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------

        public Graph()
        {
            vertexMap = new Dictionary<string, Vertex>();
        }


        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        public Vertex GetVertex(string name)
        {
            if (!vertexMap.ContainsKey(name))
            {
                vertexMap.Add(name, new Vertex(name));
            }

            return vertexMap[name];
        }

        public void AddEdge(string source, string dest, double cost)
        {
            Vertex srcVertex = GetVertex(source);
            Vertex destVertex = GetVertex(dest);

            srcVertex.AddEdge(new Edge(destVertex, cost));
        }

        public void ClearAll()
        {
            foreach (var pair in vertexMap)
            {
                pair.Value.Reset();
            }
        }


        //----------------------------------------------------------------------
        // ToString that has to be implemented for exam
        //----------------------------------------------------------------------

        public override string ToString()
        {
            string retstring = "";

            foreach (var pair in vertexMap)
            {
                retstring += pair.Value;
            }

            return retstring;
        }


        //----------------------------------------------------------------------
        // Interface methods : methods that have to be implemented for homework
        //----------------------------------------------------------------------

        public void Unweighted(string name)
        {
            ClearAll();

            Vertex startVertex = GetVertex(name);

            Queue<Vertex> q = new Queue<Vertex>();

            startVertex.SetCost(0, null);
            q.Enqueue(startVertex);

            while (q.Count != 0)
            {
                Vertex vertex = q.Dequeue();

                foreach (var edge in vertex.neighbors)
                {
                    Vertex v = edge.dest;

                    if (v.currentCost == INFINITY)
                    {
                        v.SetCost(vertex.currentCost + 1, vertex);

                        q.Enqueue(v);
                    }
                }
            }
        }

        public void Dijkstra(string name)
        {
            PriorityQueue<Edge> queue = new PriorityQueue<Edge>();

            Vertex startVertex = GetVertex(name);

            ClearAll();

            startVertex.currentCost = 0;
            startVertex.isSeen = true;
            queue.Add(new Edge(startVertex, 0));

            while (queue.Size() != 0)
            {
                Edge edge = queue.Remove();

                Vertex vertex = edge.dest;

                if (vertex.isSeen && vertex.currentCost != 0) continue;

                foreach (var neighbor in vertex.neighbors)
                {
                    Vertex w = neighbor.dest;
                    double ec = neighbor.cost;

                    if (vertex.currentCost + ec < w.currentCost)
                    {
                        var a = vertex.bezochteRegios.Contains(w.GetRegio());

                        if (!vertex.bezochteRegios.Contains(w.GetRegio()) || vertex.GetRegio() == w.GetRegio())
                        {
                            w.SetCost(vertex.currentCost + ec, vertex);
                            queue.Add(neighbor);
                        }
                    }
                }
            }
        }

        public bool IsConnected()
        {
            foreach (var vName in vertexMap.Keys)
            {
                Unweighted(vName);

                foreach (var vertex in vertexMap.Values)
                {
                    if (vertex.currentCost == INFINITY)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        //----------------------------------------------------------------------
        // Interface methods : methods that have to be implemented during exam
        //----------------------------------------------------------------------

        public string AllPaths()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pair in vertexMap)
            {
                var node = pair.Value;

                sb.Append(node.GetName());

                node = node.bestVertex;

                while (node != null)
                {
                    sb.Append($"<-{node.name}");

                    node = node.bestVertex;
                }
                sb.Append(";");
            }


            return sb.ToString();
        }

        public void AddUndirectedEdge(string source, string dest, double cost)
        {
            AddEdge(source, dest, cost);
            AddEdge(dest, source, cost);
        }

        public void AddVertex(string name, string regio)
        {
            if (!vertexMap.ContainsKey(name))
            {
                vertexMap.Add(name, new Vertex(name, regio));
            }
        }

    }



    public class PriorityQueue<T> where T : IComparable<T>
    {
        public static int DEFAULT_CAPACITY = 100;
        public int size;   // Number of elements in heap
        public T[] array;  // The heap array

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public PriorityQueue()
        {
            array = new T[DEFAULT_CAPACITY + 1];
        }

        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------
        public int Size()
        {
            return size;
        }

        public void Clear()
        {
            size = 0;
        }

        public void Add(T x)
        {
            AddFreely(x);

            int hole = Size();

            while (x.CompareTo(array[hole / 2]) < 0)
            {
                Swap(hole, hole / 2);
                hole /= 2;
            }
        }



        // Removes the smallest item in the priority queue
        public T Remove()
        {
            if (Size() == 0) return default(T);

            var returnVal = array[1];
            array[1] = array[Size()];

            PercolateDown(1);

            size--;

            return returnVal;
        }

        public override string ToString()
        {
            return string.Join(" ", array.Where((comparable, i) => i > 0 && i <= Size()));
        }

        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for homework
        //----------------------------------------------------------------------

        public void AddFreely(T x)
        {
            if (Size() + 1 >= array.Length)
            {
                int newCapacity = this.array.Length << 1 >= 0 ? this.array.Length << 1 : int.MaxValue;

                Array.Resize(ref this.array, newCapacity);
            }

            size++;
            array[size] = x;
        }

        public void BuildHeap()
        {
            PercolateDown(Size() / 2);
        }

        private void PercolateDown(int v)
        {
            for (int i = v; i > 0; i--)
            {
                for (int x = i; i < Size();)
                {
                    int leftIndex = x * 2;
                    int rightIndex = leftIndex + 1;

                    if (leftIndex > Size()) { break; }

                    int smallestChildIndex = rightIndex;

                    if (rightIndex > Size() || array[rightIndex].CompareTo(array[leftIndex]) > 0)
                    {
                        smallestChildIndex = leftIndex;
                    }

                    if (array[x].CompareTo(array[smallestChildIndex]) <= 0)
                    {
                        break;
                    }

                    Swap(x, smallestChildIndex);
                    x = smallestChildIndex;
                }
            }
        }

        private void Swap(int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;

        }
    }
}