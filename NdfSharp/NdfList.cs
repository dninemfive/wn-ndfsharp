using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSitter;

namespace NdfSharp;
internal class UntypedNdfList
{
    public bool IsRoot { get; set; }
    
}
public class NdfList<T>
    : IEnumerable<T>
    where T : NdfRow
{
    public bool IsRoot { get; set; }
    public void Add(T item) { }
}
public class NdfRow
{

}
public class NdfObject(string type) : NdfList<NdfMemberRow>
{
    public string Type { get; set; } = type;
    public void Add(Dictionary<string, object> item) { }
}
public class NdfMemberRow : NdfRow { }
public class NdfTemplate(string type, string @namespace)
    : NdfList<NdfMemberRow>
{
    string Type = type;
    string Namespace = @namespace;
    public NdfTemplate(Node node) : this(node.TryGetField("type")!.ToString(), node.TryGetField("name")!.ToString()) { }
}