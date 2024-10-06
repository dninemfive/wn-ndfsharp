using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using d9.utl;

namespace NdfSharp;
public class NdfRowValue : Union<NdfList, string>
{
    public NdfRowValue(NdfList list) : base(list) { }
    public NdfRowValue(string str) : base(str) { }
}
public class NdfRow : ICopyable<NdfRow>
{
    // important insight: NdfRow values will _always_ be either NdfList or a literal type (which we'll just call a string for now)
    //                    similarly, NdfLists will _always_ have items of type NdfRow
    //                    NdfRows will _always_ have a parent which is either NdfList or null
    //                    NdfLists will _always_ have a parent which is either NdfRow or null
    private readonly Dictionary<string, NdfRowValue> _dict = new();
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L115-L118
    /// </summary>
    public NdfList? Parent { get; internal set; } = null;
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L94-L113
    /// </summary>
    public int? Index
    {
        get
        {
            if(Parent is not null)
            {
                foreach ((NdfRow other, int i) in Parent.WithIndices())
                {
                    // intentionally compares object *instance* rather than an override here
                    if (ReferenceEquals(this, other))
                        return i;
                }
            }
            return null;
        }
    }
    public NdfRow() { }
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L120-L125
    /// </summary>
    /// <returns></returns>
    public virtual NdfRow Copy()
    {
        NdfRow result = (NdfRow)MemberwiseClone();
        result.Parent = null;
        return result;
    }
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L127-L161
    /// </summary>
    /// <param name="changes"></param>
    /// <param name="opName"></param>
    /// <param name="caller"></param>
    private void Edit(Dictionary<string, NdfRowValue> changes)
    {
        // i'm not adding aliases so this can be a lot simpler
        foreach((string k, NdfRowValue v) in changes)
            _dict[k] = v;
    }
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L363-L368
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => $"{GetType().Name}[{(Index is int i ? i : "DANGLING")}]({_dict.Select(x => $"{x.Key}={x.Value}").ListNotation(brackets: null)})";
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L265-L351
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if(obj is NdfRow other)
        {
            HashSet<string> allKeys = _dict.Keys.Union(other._dict.Keys).ToHashSet();
            foreach (string key in allKeys)
                if (!(_dict.TryGetValue(key, out NdfRowValue? thisVal)
                    && other._dict.TryGetValue(key, out NdfRowValue? otherVal)
                    && thisVal == otherVal))
                    return false;
            return true;
        }
        return false;
    }
    public NdfRowValue this[string key]
    {
        get => _dict[key];
        set
        {
            if(value.As<NdfList>() is NdfList list)
            {
                if (list.Parent is not null && list.Parent != this)
                    list = list.Copy();
                list.Parent = this;
            }
        }
    }
}
