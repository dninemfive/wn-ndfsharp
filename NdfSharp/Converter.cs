using d9.utl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TreeSitter;

namespace NdfSharp;
internal delegate Dictionary<string, dynamic> Converter(Node node);
internal static class Converters
{
    public static NdfNode Convert(this Node node)
    {
        foreach (Type type in ReflectionUtils.AllLoadedTypesWithAttribute<NdfNodeTypeAttribute>(AppDomain.CurrentDomain))
        {
            NdfNodeTypeAttribute attr = type.GetCustomAttribute<NdfNodeTypeAttribute>()!;
            if (attr.Matches(node) && type is IConvertableFromNode converter)
                return converter.Convert(node);
        }
        return null;
    }
    private static Converter IdkWhatToCallThisRn(string field1, string field2key, string field2)
        => delegate (Node node)
        {
            Dictionary<string, dynamic> result = node.TryGetField(field1)!.Convert();
            result[field2key] = node.TryGetField(field2)!.ToString();
            return result;
        };
    /// <summary>
    /// <see href="https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/converter.py#L50-L53"/>
    /// </summary>
    internal static Converter Visibility
        => IdkWhatToCallThisRn("item", "visibility", "type");
    /// <summary>
    /// <see href="https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/converter.py#L56-L59"/>
    /// </summary>
    internal static Converter Assignment
        => IdkWhatToCallThisRn("value", "namespace", "name");
    internal static Converter Object
        => delegate (Node node)
        {
            NdfObject result = new(node.TryGetField("type")!.ToString());
            if (node.TryGetField("members") is Node members)
            {
                foreach (Node child in members.UnignoredChildren())
                {
                    result.Add(child.Convert());
                }
            }
            return new()
            {
                { "value", result }
            };
        };
    internal static Converter Template
        => delegate (Node node)
        {
            Node obj = node.TryGetField("value")!;
            NdfTemplate result = new(obj);

        };
}
