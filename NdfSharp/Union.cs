using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdfSharp;
public class Union<T1, T2>
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
    public object Value
        => (_t1 as object) ?? (_t2 as object) ?? throw new Exception($"Error: {TypeName} has neither a {typeof(T1).Name} nor a {typeof(T2).Name} value!");
    /// <summary>
    /// T should always be either : T1 or : T2 but idk how to do that constraint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T As<T>()
        => (T)Value;
    public string TypeName => $"Union<{typeof(T1).Name}|{typeof(T2).Name}>";
} 
