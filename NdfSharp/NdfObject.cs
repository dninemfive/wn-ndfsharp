using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSitter;

namespace NdfSharp;
[NdfNodeType("object")]
public class NdfObject(string type) : NdfList<NdfMemberRow>, INdfNode, IConvertableFromNode
{
    public static INdfNode Convert(Node node)
    {
        NdfObject result = new(node.TryGetField("type")!.ToString());
        if (node.TryGetField("members") is Node members)
        {
            foreach (Node child in members.UnignoredChildren())
            {
                result.Add(child.Convert());
            }
        }
        return result;
    }
    private readonly Dictionary<string, INdfNode> _members = new();
    public string Type { get; set; } = type;
    public void Add(INdfNode child)
    {
        _members = 
    }
}