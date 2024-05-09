namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

using System;
using System.Collections.Generic;
using System.Linq;

public class Graph<Node> : Graph<Node, object> { }

public class Graph<Node, Edge>
    where Edge : class
{
    private record EdgeLink
    {
        public required int EndpointIndex;
        public required Edge Data;
    }

    private List<Node> _nodes = [];
    private List<List<EdgeLink>> _edges = [];

    public void Clear()
    {
        _nodes = [];
        _edges = [];
    }

    public void AddNode(Node node)
    {
        // Add the node
        _nodes.Add(node);
        // Add an empty list of edges
        _edges.Add([]);
    }

    public void RemoveNode(Node node)
    {
        var index = _nodes.IndexOf(node);
        if (index < 0)
        {
            return;
        }

        // Delete this node and its outgoing edges
        _nodes.RemoveAt(index);
        _edges.RemoveAt(index);

        // Delete all inbound edges from other nodes (updating the indexes of subsequent connections)
        for (var startNodeIndex = 0; startNodeIndex < _edges.Count; startNodeIndex++)
        {
            var outboundEdges = _edges[startNodeIndex];
            outboundEdges.RemoveAll(edge => edge.EndpointIndex == index);
            foreach (var edge in outboundEdges)
            {
                if (edge.EndpointIndex > index)
                {
                    edge.EndpointIndex--;
                }
            }
        }
    }

    public IEnumerable<T> GetNodesOfType<T>()
    {
        return _nodes.OfType<T>();
    }

    public IEnumerable<Node> GetNodes()
    {
        return _nodes.AsReadOnly();
    }

    public IEnumerable<Node> GetNodesFromOutgoingEdges(Node node)
    {
        var index = _nodes.IndexOf(node);
        if (index >= 0)
        {
            return _edges[index].Select(edge => _nodes[edge.EndpointIndex]);
        }
        return [];
    }

    public IEnumerable<Node> GetNodesFromIncomingEdges(Node node)
    {
        var index = _nodes.IndexOf(node);
        if (index >= 0)
        {
            return _edges.SelectMany(
                (outbound, outboundIndex) =>
                    outbound
                        .Where(edge => edge.EndpointIndex == index)
                        .Select(edge => _nodes[outboundIndex])
            );
        }
        return [];
    }

    public virtual bool Connect(Node start, Node end, Edge data)
    {
        var startIndex = _nodes.IndexOf(start);
        var endIndex = _nodes.IndexOf(end);
        if (startIndex >= 0 && endIndex >= 0)
        {
            var edge = new EdgeLink { EndpointIndex = endIndex, Data = data, };
            _edges[startIndex].Add(edge);
            return true;
        }
        return false;
    }

    public bool Disconnect(Node start, Node end)
    {
        var startIndex = _nodes.IndexOf(start);
        var endIndex = _nodes.IndexOf(end);
        if (startIndex >= 0 && endIndex >= 0)
        {
            var removed = _edges[startIndex].RemoveAll(edge => edge.EndpointIndex == endIndex);
            return removed > 0;
        }
        else
        {
            return false;
        }
    }

    public void DisconnectAll(Func<Node, Node, Edge, bool> matcher)
    {
        for (var i = 0; i < _edges.Count; i++)
        {
            _edges[i]
                .RemoveAll((link) => matcher(_nodes[i], _nodes[link.EndpointIndex], link.Data));
        }
    }

    public Edge? GetEdgeData(Node start, Node end)
    {
        return GetAllEdgeData(start, end).FirstOrDefault();
    }

    public IEnumerable<Edge> GetAllEdgeData(Node start, Node end)
    {
        var startIndex = _nodes.IndexOf(start);
        var endIndex = _nodes.IndexOf(end);
        if (startIndex >= 0 && endIndex >= 0)
        {
            var edges = _edges[startIndex]
                ?.Where(edge => edge.EndpointIndex == endIndex)
                .Select(edge => edge.Data);
            if (edges != null)
            {
                return edges;
            }
        }

        return [];
    }
}
