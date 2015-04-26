namespace uBinding.Changables
{
    public interface IBindable<TValue> : IChangable
    {
        TValue Value { get; set; }
    }
}