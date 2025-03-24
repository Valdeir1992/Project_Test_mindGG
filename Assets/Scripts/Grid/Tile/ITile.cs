using UnityEngine;

public interface ITile
{
    public GameObject GetTile { get; } 
    public GridController GridController { get; }  
    public bool IsVisible { get; }
    public void Setup(GridController controller);
    public void SetStartPos(Vector2Int pos);
    public void SetOrderLayer(int orderLayer); 
    public void SetColor(Color color); 
    public void Select();
    public void Drop();
    public void Hide();
    public void Show();
}
public interface IAction
{
    public void Do(ITile tile);
}
public interface ICoverTile : ITile
{
    public void Clean();
    public bool CanClean();
}
