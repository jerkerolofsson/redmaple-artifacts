namespace RedMaple.Artifacts.Contracts
{
    [Flags]
    public enum VersionFlags : int
    {
        Alpha = 1,
        Beta = 2,
        Stable = 3,
        Released = 4,
        Recalled = 5,
        Obsolete = 6
    }
}
