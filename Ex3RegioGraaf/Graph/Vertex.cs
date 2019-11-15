using System;
using System.Collections.Generic;

namespace AD
{
    public class Vertex : IVertex, IComparable<Vertex>
    {
        public string name;
        public string regio;
        public double currentCost;
        public List<Edge> neighbors;

        public Vertex bestVertex;
        public bool isSeen;

        public HashSet<string> bezochteRegios;

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!
        //!! Vul deze klasse met je eigen Vertex en vul aan
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

        public Vertex(string name) : this(name, "")
        {

        }

        //----------------------------------------------------------------------
        // Constructor that has to be implemented during exam
        //----------------------------------------------------------------------

        public Vertex(string name, string regio)
        {
            this.name = name;
            this.regio = regio;
            neighbors = new List<Edge>();
        }

        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        public void Reset()
        {
            currentCost = Graph.INFINITY;
            bestVertex = null;
            isSeen = false;

            bezochteRegios = new HashSet<string>();
        }


        //----------------------------------------------------------------------
        // Interface methods : methods that have to be implemented during exam
        //----------------------------------------------------------------------

        public string GetName()
        {
            return name;
        }


        public string GetRegio()
        {
            return regio;
        }


        //----------------------------------------------------------------------
        // ToString that has to be implemented for exam
        //----------------------------------------------------------------------

        public override string ToString()
        {
            string cost = (currentCost > 0 || isSeen) ? $"({currentCost})" : "";

            string edges = "";
            foreach (var edge in neighbors)
            {
                edges += edge;
            }

            edges = edges.Length > 0 ? $"[{edges}]" : "";

            return $"{name} {cost} {edges} \n";
        }

        public void AddEdge(Edge edge)
        {
            neighbors.Add(edge);
        }

        public void SetCost(double cost, Vertex pref)
        {
            bestVertex = pref;
            currentCost = cost;

            if (pref != null)
            {
                bezochteRegios = pref.bezochteRegios;
                bezochteRegios.Add(pref.regio);
            }
        }

        public int CompareTo(Vertex other)
        {
            throw new NotImplementedException();
        }
    }
}