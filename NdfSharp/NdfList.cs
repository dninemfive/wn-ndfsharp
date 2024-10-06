using TreeSitter;

namespace NdfSharp;
public class NdfList<T>
    where T : NdfRow<T>
{
    public bool IsRoot { get; set; }
    private readonly List<T> _rows = new();
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L883-L884
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        SetSingleRow(_rows.Count, item, false);
    }
    /// <summary>
    /// https://github.com/Ulibos/ndf-parse/blob/main/ndf_parse/model/abc.py#L1141-L1167
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <param name="reuse"></param>
    private void SetSingleRow(int index, T item, bool reuse = false)
    {
        if (reuse && _rows[index] == item)
            return;
        T old = _rows[index];
        _rows[index] = item;
        old.Parent = null;
        item.Parent = this;
    }
}