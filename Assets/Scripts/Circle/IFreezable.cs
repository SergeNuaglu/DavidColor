public interface IFreezable
{
    public bool IsFreezed { get; }

    void Freeze();

    void Unfreeze(bool isMadeMoveForAd = false);
}