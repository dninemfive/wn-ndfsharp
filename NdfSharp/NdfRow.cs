using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
public abstract class NdfRow
{
    public NdfList? Parent { get; internal set; }
}
