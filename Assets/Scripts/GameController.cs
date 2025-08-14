using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> chips;
    [SerializeField] private List<GameObject> cells;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    public void Generator(int gridCols, int gridRows)
    {
        Vector3 startPos = new Vector3(-8f, 3.5f, 0);
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                GameObject anotherChip = null;
                GameObject anotherCell = null;
                if (i == 0 && j == 0)
                {
                    anotherChip = chips[0];
                    anotherCell = cells[0];
                }
                else
                {
                    anotherChip = Instantiate(chips[UnityEngine.Random.Range(0, chips.Count)]);
                    anotherCell = Instantiate(cells[UnityEngine.Random.Range(0, cells.Count)]);
                }
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                anotherChip.transform.position = new Vector3(posX, posY, startPos.z);
                anotherCell.transform.position = new Vector3(posX, posY, startPos.z);
            }

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Generator(9, 4);
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
