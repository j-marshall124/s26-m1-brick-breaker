using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // The object we will clone many of when making the grid
    public GameObject blockPrefab;
    // The options we have when spawning blocks (purely visual)
    public Sprite[] blockSprites;
    // Gap between blocks (in pixels)
    public int blockGap = 2;
    // Define a grid using numbers as index
    [TextArea(10, 10)] // Creates a textbox area in Unity inspector
    public string gridBlocks = "000 222 000\n22222222222\n22 333 22\n333";

    // How many blocks were created for this grid?
    public int numberOfBlocks;
    public int numberOfBlocksDestroyed;

    void Start()
    {
        CreateBlockGrid();
    }

    void CreateBlockGrid()
    {
        // Get size of block directly from the sprite renderer through prefab
        Vector2 blockSize = blockPrefab.GetComponent<SpriteRenderer>().bounds.size;

        // Split string up at the boundary of a newline
        string[] rows = gridBlocks.Split("\n");
        // Iterate over each line in the string (row)
        for (int y = 0; y < rows.Length; y++)
        {
            // Grab single row from array of rows
            string row = rows[y].Trim(); // .Trim removes white space (such as "\r")
            // Calculate the total width of the row
            float rowWidth = blockSize.x * row.Length + blockGap * (row.Length - 1);
            // Calculate spacing for block along Y ais (space each row)
            float blockY = (blockSize.y + blockGap) * -y;

            // Iterate over each character in a line
            for (int x = 0; x < row.Length; x++)
            {
                // Skip any spaces in string
                string blockStr = row[x].ToString();
                if (blockStr == " ")
                    continue;


                // (rowWidth / 2) -- means offset to left side by half the width of row; centre adjusted
                // (blockSize.x + blockGap) * x -- means offset new block by how many came before it
                // (blockSize.x / 2) -- offset by half block width moved to the right
                float blockX = (-rowWidth / 2) + (blockSize.x + blockGap) * x + (blockSize.x / 2);

                // Create new block
                Vector3 position = new Vector3(blockX, blockY, 0) + this.transform.position;
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, this.transform);
                numberOfBlocks += 1;
                // Convert string values 0, 1, 2, 3, or 4 into integer to get sprite from array of sprites and assign it
                int spriteIndex = int.Parse(blockStr);
                Sprite blockSprite = blockSprites[spriteIndex];
                block.GetComponent<SpriteRenderer>().sprite = blockSprite;
            }
        }
    }
}
