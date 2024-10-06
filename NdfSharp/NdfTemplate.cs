using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
public class NdfTemplate(string type, string @namespace)
    : NdfList<NdfMemberRow>
{
    string Type = type;
    string Namespace = @namespace;
    public NdfTemplate(Node node) : this(node.TryGetField("type")!.ToString(), node.TryGetField("name")!.ToString()) { }
}