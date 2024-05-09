namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

using System;
using System.Collections.Generic;
using System.Linq;

public class NodePortCollection
{
    public int Size => _ports.Count;
    private List<NodePort> _ports = [];
    public NodePort? this[string name] => _ports.Where(port => port.Name == name).FirstOrDefault();
    public NodePort? this[int index] => _ports[index];

    public NodePortCollection() { }

    public NodePortCollection(IEnumerable<NodePort> ports)
    {
        this._ports = ports.ToList();
    }

    public int IndexOf(string port)
    {
        for (var i = 0; i < _ports.Count; i++)
        {
            if (_ports[i].Name == port)
                return i;
        }
        return -1;
    }

    public IEnumerable<NodePort> Enumerate()
    {
        foreach (var port in _ports)
            yield return port;
    }
}
