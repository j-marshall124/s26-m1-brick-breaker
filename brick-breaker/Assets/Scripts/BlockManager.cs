using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // The object we will clone many of when making the grid
    public GameObject blockPrefab;
    // The options we have when spawning blocks (purely visual)
    public Sprite[] blockSprites;
    // Gap between blocks (in pixels)
    public int blockGap = 2;

    void Start()
    {
        CreateBlockGrid();
    }

    void CreateBlockGrid()
    {

    }
}
