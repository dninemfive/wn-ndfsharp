using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
public abstract class NdfRow<TSelf>
    where TSelf : NdfRow<TSelf>
{
    public NdfList<TSelf>? Parent { get; internal set; }
    public abstract TSelf Copy();
}
