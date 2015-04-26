namespace uBinding.Core
{
    public enum BindingMode
    {
        /// <summary>
        ///     Source and target are both in sync.
        /// </summary>
        TwoWay = 0,

        /// <summary>
        ///     Only target is updated.
        /// </summary>
        OneWay,

        /// <summary>
        ///     Target updated only once.
        /// </summary>
        OneTime,

        /// <summary>
        ///     Only source is updated.
        /// </summary>
        OneWayToSource
    }
}