using UnityEngine; 

public interface IItem : ITile
{ 
    public ItemInfo Info { get; set; }
    public void SetupItem(ItemInfo icon);
    public void ChangeIcon(ItemInfo itemInfo);
    public bool CanCombine(ItemInfo itemInfo); 
    public void Move();
}
