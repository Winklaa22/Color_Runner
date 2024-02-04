using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GridSize
{
    public int width = 1;
    public int height = 1; // Changed from depth to height
}

[System.Serializable]
public class GridPosition
{
    public int x = 0;
    public int z = 0; // Changed from y to z
}

public class PropsGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct PropPrefab
    {
        public GameObject prefab;
        public GridSize gridSize;
    }

    public PropPrefab[] propPrefabs;
    public int gridSizeX = 5; // Grid size in the X-axis
    public int gridSizeZ = 5; // Grid size in the Z-axis
    public float gapBetweenGridsX = 1.0f;
    public float gapBetweenGridsZ = 1.0f;
    public float playerWidth = 1.0f; // Width of the player (in grid units)
    public int minPropsPerGrid = 1; // Minimum number of props per grid
    public int maxPropsPerGrid = 5; // Maximum number of props per grid

    private List<GridPosition> occupiedGridPositions = new List<GridPosition>();

    private void Start()
    {
        GenerateProps();
    }

    private void OnDrawGizmos()
    {
        // Visualize grid positions
        Gizmos.color = Color.cyan;
        Vector3 startPosition = transform.position - new Vector3(((gridSizeX - 1) * gapBetweenGridsX) / 2, 0, ((gridSizeZ - 1) * gapBetweenGridsZ) / 2);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 gridCenter = startPosition + new Vector3(x * gapBetweenGridsX, 0, z * gapBetweenGridsZ);
                Gizmos.DrawWireCube(gridCenter, new Vector3(gapBetweenGridsX, 0.1f, gapBetweenGridsZ));
            }
        }
    }

    private void GenerateProps()
    {
        foreach (PropPrefab propPrefab in propPrefabs)
        {
            int numProps = Random.Range(minPropsPerGrid, maxPropsPerGrid + 1); // Randomize the number of props per grid

            for (int i = 0; i < numProps; i++)
            {
                GridPosition gridPos = FindValidGridPosition(propPrefab.gridSize);

                if (gridPos != null)
                {
                    float propX = (gridPos.x * gapBetweenGridsX) - ((gridSizeX - 1) * gapBetweenGridsX) / 2;
                    float propZ = (gridPos.z * gapBetweenGridsZ) - ((gridSizeZ - 1) * gapBetweenGridsZ) / 2;
                    float propY = (propPrefab.gridSize.height / 2);

                    // Instantiate the prop prefab as a child of the platform
                    Vector3 propLocalPosition = new Vector3(propX, propY, propZ);
                    GameObject prop = Instantiate(propPrefab.prefab, transform);
                    prop.transform.localPosition = propLocalPosition;

                    // Adjust the size of the prop based on the grid size
                    prop.transform.localScale = new Vector3(propPrefab.gridSize.width, propPrefab.gridSize.height, propPrefab.gridSize.height);

                    // Mark the grid positions as occupied
                    MarkGridPositionsOccupied(gridPos, propPrefab.gridSize);
                }
            }
        }
    }

    private GridPosition FindValidGridPosition(GridSize gridSize)
    {
        // Create a list of all available grid positions
        List<GridPosition> availableGridPositions = new List<GridPosition>();
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                GridPosition gridPos = new GridPosition { x = x, z = z };

                // Check if this grid position is not occupied
                if (!IsGridPositionOccupied(gridPos, gridSize))
                {
                    // Check if there's enough space for the player to pass through
                    if (!IsPlayerObstructed(gridPos, gridSize))
                    {
                        availableGridPositions.Add(gridPos);
                    }
                }
            }
        }

        // If there are available positions, select a random one; otherwise, return null
        if (availableGridPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, availableGridPositions.Count);
            return availableGridPositions[randomIndex];
        }
        else
        {
            return null;
        }
    }

    private bool IsGridPositionOccupied(GridPosition gridPos, GridSize gridSize)
    {
        foreach (GridPosition occupied in occupiedGridPositions)
        {
            if (gridPos.x >= occupied.x && gridPos.x < occupied.x + gridSize.width
                && gridPos.z >= occupied.z && gridPos.z < occupied.z + gridSize.height)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPlayerObstructed(GridPosition gridPos, GridSize gridSize)
    {
        // Calculate the player's position in grid units
        float playerX = (gridSizeX - 1) / 2.0f;

        // Check if there's enough space for the player to pass through
        if (playerX >= gridPos.x && playerX < gridPos.x + gridSize.width)
        {
            return true;
        }

        return false;
    }

    private void MarkGridPositionsOccupied(GridPosition gridPos, GridSize gridSize)
    {
        for (int x = gridPos.x; x < gridPos.x + gridSize.width; x++)
        {
            for (int z = gridPos.z; z < gridPos.z + gridSize.height; z++)
            {
                occupiedGridPositions.Add(new GridPosition { x = x, z = z });
            }
        }
    }
}
