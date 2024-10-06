using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
public class NdfNodeTypeAttribute(params string[] types): Attribute
{
    public readonly string[] Types = types;
    public bool Matches(string type) => Types.Contains(type);
}
