using System.Diagnostics.CodeAnalysis;
using TreeSitter;

namespace NdfSharp;
public interface IConvertableFromNode
{
    public static abstract NdfNode Convert(Node node);
}