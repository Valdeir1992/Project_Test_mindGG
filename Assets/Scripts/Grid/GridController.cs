using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GridController : MonoBehaviour
{
    private Grid _grid;
    private Vector2Int _selectedTile;
    private List<ITile> _listTile = new ();
    [SerializeField] private SpriteRenderer _moveSpriteRender;
    [SerializeField] private TileInfo _itemInfo;
    [SerializeField] private TileInfo _tileInfo; 
    [SerializeField] private Transform _container;
    [SerializeField] private ItemInfo _debugItem;
    private void Awake()
    {
        _grid = new Grid(5, 5);
        _grid.SetupGrid("#FC8803", "#FCAF56", _tileInfo, _container);
    }

    private void Start()
    {
        AddTile(new Vector2Int(0,0),_debugItem);
    }
    private void Update()
    {
        if (_moveSpriteRender.enabled)
        {
            _moveSpriteRender.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    public void SelectTile(Vector2Int index)
    {
        _selectedTile = index;
    }
    
    public void StartMoveTile()
    {
        var tile =_grid._gridDictionary[_selectedTile]; 
        if(tile is IItem item)
        {
            item.Hide();
            item.Move();
            _moveSpriteRender.sprite = Addressables.LoadAssetAsync<Sprite>(item.Info.Icon).WaitForCompletion();
            _moveSpriteRender.enabled = true;
        } 
    }
    public void Deselect()
    {
        _grid._gridDictionary[_selectedTile].Show();
        _moveSpriteRender.enabled = false;
    }
    public void AddTile(Vector2Int index, ItemInfo itemInfo)
    {
        IItem newTile = Addressables
            .InstantiateAsync(_itemInfo.Prefab)
            .WaitForCompletion()
            .GetComponent<IItem>();
        newTile.SetupItem(itemInfo);
        newTile.Setup(this);
        newTile.SetStartPos(index);
        _listTile.Add(newTile);

        _grid._gridDictionary[index] = newTile;
    }
}
