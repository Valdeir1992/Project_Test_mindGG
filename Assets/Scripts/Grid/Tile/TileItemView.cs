using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class TileItemView : TileView, IItem
{
    private ItemInfo _item;
    private IAction OnMove;
    [SerializeField] private SpriteRenderer _iconRender;

    public SpriteRenderer IconRender => _iconRender;

    public ItemInfo Info { get => _item; set => _item = value; }

    protected override void Awake()
    {
        OnMove = new HideTile();
        base.Awake();
    }

    public void SetupItem(ItemInfo itemInfo)
    {
        _item = itemInfo;
        _iconRender.sprite = Addressables.LoadAssetAsync<Sprite>(itemInfo.Icon).WaitForCompletion();
        SetColor(new Color(0, 0, 0,0));
    }

    public void ChangeIcon(ItemInfo itemInfo)
    {
        _item = itemInfo;
        _iconRender.sprite = Addressables.LoadAssetAsync<Sprite>(itemInfo.Icon).WaitForCompletion();

    } 
    public bool CanCombine(ItemInfo itemInfo)
    {
        return itemInfo.CombineInfo.Path.Any(x => x.Name == itemInfo.Name);
    }

    public void Move()
    {
        OnMove?.Do(this);
    }
    public override void Hide()
    {
        _iconRender.enabled = false;
        base.Hide();
    }
}

public class HideTile : IAction
{
    public void Do(ITile tile)
    {
        if (tile.IsVisible)
        {
            tile.Hide();
        }
    }
}
