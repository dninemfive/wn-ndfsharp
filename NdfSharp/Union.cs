using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
internal class Union<T1, T2>
    where T1 : class
    where T2 : class
{
    private T1? _t1 = null;
    private T2? _t2 = null;

    public Union(T1 t1)
    {
        _t1 = t1;
    }
    public Union(T2 t2)
    {
        _t2 = t2;
    }
}
