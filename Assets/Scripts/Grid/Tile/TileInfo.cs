using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="Prototype/Tile/Info")]
public class TileInfo : ScriptableObject
{
    [SerializeField] private AssetReference _prefab;
    [SerializeField] private int _slotOrderLayer;

    public AssetReference Prefab { get => _prefab;}
    public int SlotOrderLayer { get => _slotOrderLayer; }
}
