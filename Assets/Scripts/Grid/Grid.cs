using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Grid
{
    public int Columns;
    public int Rows;
    public Dictionary<Vector2Int, ITile> _gridDictionary;

    public Grid(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
        _gridDictionary = new(); 
    }
    public async void SetupGrid(Color one, Color two,TileInfo slotInfo, Transform container)
    {
        List<Task<ITile>> listTask = GenerateGrid(one, two, slotInfo);
        await Task.WhenAll(listTask);
        foreach (var slot in listTask.Select(x => x.Result))
        {
            slot.GetTile.transform.SetParent(container);
        }
    }
    public async void SetupGrid(string one, string two, TileInfo slotInfo, Transform container)
    {
        Color colorOne = default;
        Color colorTwo = default;

        try
        { 
            if (ColorUtility.TryParseHtmlString(one, out colorOne) && ColorUtility.TryParseHtmlString(two, out colorTwo))
            {
                List<Task<ITile>> listTask = GenerateGrid(colorOne, colorTwo, slotInfo);
                await Task.WhenAll(listTask);
                foreach (var slot in listTask.Select(x => x.Result))
                {
                    slot.GetTile.transform.SetParent(container);
                }
            }
        }
        catch (Exception ex) 
        {
            Debug.LogError(ex.Message);
        } 
    }

    private List<Task<ITile>> GenerateGrid(Color one, Color two, TileInfo slotInfo)
    {
        var listTask = new List<Task<ITile>>(); 
        try
        {  
            for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
                {
                    Task<ITile> tileTask = default; 
                    if ((rowIndex+columnIndex) % 2 == 0)
                    {
                        tileTask = GenerateTile(one, new Vector2Int(rowIndex, columnIndex), slotInfo);
                    }
                    else
                    {
                        tileTask = GenerateTile(two, new Vector2Int(rowIndex, columnIndex), slotInfo);
                    }
                    listTask.Add(tileTask);
                    _gridDictionary[new Vector2Int(rowIndex, columnIndex)] = null;
                }
            } 
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        return listTask;
    }

    private async Task<ITile> GenerateTile(Color color,Vector2Int pos, TileInfo info)
    {
        ITile slot = default;
        try
        { 
            var result = await Addressables.InstantiateAsync(info.Prefab).Task;
            result.name = $"Row:{pos.x} - Column:{pos.y}";
            slot = result.GetComponent<ITile>();
            slot.SetOrderLayer(info.SlotOrderLayer);
            slot.SetColor(color);
            slot.SetStartPos(pos);
            return slot;
        }
        catch (Exception ex) 
        {
            Debug.LogError(ex.Message);
        }
        return slot;
    }
}
