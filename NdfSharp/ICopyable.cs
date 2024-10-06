namespace NdfSharp;
public interface ICopyable<TSelf>
    where TSelf : ICopyable<TSelf>
{
    public TSelf Copy();
}
