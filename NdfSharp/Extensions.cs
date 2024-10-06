using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSitter;

namespace NdfSharp;
internal static class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <remarks><see href="https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/converter.py#L20-L21"/></remarks>
    public static Node? TryGetField(this Node node, string name)
    {
        try
        {
            return node.ChildByFieldName(name);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// <remarks><see href="https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/converter.py#L24-L25"/></remarks>
    public static bool IsIgnored(this Node node)
        => node.Kind.StartsWith("comment_");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// <remarks><see href="https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/converter.py#L28-L31"/></remarks>
    public static IEnumerable<Node> UnignoredChildren(this Node node)
        => node.Children.Where(x => !x.IsIgnored());
}
