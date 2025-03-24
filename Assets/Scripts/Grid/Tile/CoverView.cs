using UnityEngine.AddressableAssets;

public sealed class CoverView : TileView, ICoverTile
{ 
    public bool CanClean()
    {
        return true;
    }

    public void Clean()
    {
        Addressables.ReleaseInstance(gameObject);
    } 
}
