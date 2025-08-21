using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private Transform _cellsHolder;
    [SerializeField] private Transform _chipsHolder;
    [SerializeField] private List<GameObject> _chips;
    [SerializeField] private List<GameObject> _cells;
    [SerializeField] Vector3 startPos = new Vector3(-8f, 3.5f, 0);
    private int gridCols = 9;
    private int gridRows = 5;
    private GameObject[,] chipsGrid;


    void Awake()
    {
        Generator(gridCols, gridRows);
    }
    [ContextMenu("Generate")]
    public void GenerateFromInspector()
    {
        ClearField();
        Generator(gridCols, gridRows);
    }


    private void LateUpdate()
    {
        MoveDown();
    }

    [ContextMenu("Clear")]
    public void ClearField()
    {
        var objectsToDestroy = new List<GameObject>();
        foreach (Transform child in _cellsHolder)
        {
            objectsToDestroy.Add(child.gameObject);
        }
        foreach (Transform child in _chipsHolder)
        {
            objectsToDestroy.Add(child.gameObject);
        }
        foreach (var objectToDestroy in objectsToDestroy)
        {
            DestroyImmediate(objectToDestroy);
        }
    }
    public void Generator(int gridCols, int gridRows)
    {
        chipsGrid = new GameObject[gridCols, gridRows];
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                Vector3 pos = new Vector3(_offsetX * i + startPos.x, -_offsetY * j + startPos.y, startPos.z);
                var anotherCell = Instantiate(_cells[UnityEngine.Random.Range(0, _cells.Count)], pos, Quaternion.identity, _cellsHolder);
                var anotherChip = Instantiate(_chips[UnityEngine.Random.Range(0, _chips.Count)], pos, Quaternion.identity, _chipsHolder);
                chipsGrid[i, j] = anotherChip;
            }
        }
    }
    public void MoveDown()
    {
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = gridRows - 1; j >= 0; j--)
            {
                if (chipsGrid[i, j] == null)
                {
                    int k = j - 1;
                    while (k >= 0 && chipsGrid[i, k] == null)
                        k--;
                    if (k >= 0)
                    {
                        chipsGrid[i, j] = chipsGrid[i, k];
                        chipsGrid[i, k] = null;
                        chipsGrid[i, j].transform.position = new Vector3(_offsetX * i + startPos.x, -_offsetY * j + startPos.y, startPos.z);
                    }
                    else
                    {
                        chipsGrid[i, j] = Instantiate(_chips[UnityEngine.Random.Range(0, _chips.Count)], new Vector3(_offsetX * i + startPos.x, -_offsetY * j + startPos.y, startPos.z), Quaternion.identity, _chipsHolder);
                    }
                }
            }
        }
    }
}
