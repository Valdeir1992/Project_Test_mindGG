using UnityEngine;

[CreateAssetMenu(menuName = "Prototype/Tile/Combine")]
public class CombineInfo : ScriptableObject
{
    [SerializeField] private CombinePath[] _path;

    public CombinePath[] Path { get => _path;}
}
