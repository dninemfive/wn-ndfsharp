namespace NdfSharp;
public interface INdfNode
{
    public INdfNode Copy();
    public T? CopyAs<T>() where T : class, INdfNode
        => Copy() as T;
}
