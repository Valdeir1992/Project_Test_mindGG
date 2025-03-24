using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Prototype/Tile/Item")] 
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AssetReferenceSprite _icon;
    [SerializeField] private CombineInfo _combineInfo;

    public string Name { get => _name; set => _name = value; }
    public AssetReferenceSprite Icon { get => _icon; set => _icon = value; }
    public CombineInfo CombineInfo { get => _combineInfo; set => _combineInfo = value; }
}
