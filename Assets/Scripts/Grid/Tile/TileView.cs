using UnityEngine;

public abstract class TileView: MonoBehaviour, ITile
{ 
    public Vector2Int SlotIndex { get; private set; }
    private SpriteRenderer _slotRender; 
    protected GridController controller; 
    protected IAction OnSelect { get; set; }
    protected IAction OnDrop { get; set; }

    public GameObject GetTile =>gameObject;

    public GridController GridController => controller;

    public SpriteRenderer TileRender => _slotRender;

    public bool IsVisible => _slotRender.enabled;

    protected virtual void Awake()
    {
        _slotRender = GetComponentInChildren<SpriteRenderer>();
    } 
    public void SetColor(Color color)
    {
        _slotRender.color = color;
    }

    public void SetStartPos(Vector2Int pos)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, 0);
    }

    public void SetOrderLayer(int slotOrderLayer)
    {
        _slotRender.sortingOrder = slotOrderLayer;
    }

    public void Setup(GridController controller)
    {
        this.controller = controller;
    } 
    public void Select()
    {
        OnSelect?.Do(this);
        controller.SelectTile(SlotIndex);
    }

    public void Drop()
    {
        OnDrop?.Do(this);
        controller.Deselect();
    }

    public virtual void Hide()
    {
        _slotRender.enabled = false;
    }

    public void Show()
    {
        _slotRender.enabled = true;
    }
}
