using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTileGenerator : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>(); //List of prefabs of tiles to generate. Elements of the list can be left empty if you want empty slots. The more times a tile is in the list, the higher the chances of spawning it
    public Vector2 tileSize; //2D Size of the prefab
    public Vector2 sectionSize; //2D Size of the section to fill
    public Vector3 rotationEuler = new Vector3(0f, -90f, 0f); //Rotation of the tiles
    public int randomSeed = 0; //Leave at 0 for a random seed every time

    List<GameObject> generatedTiles = new List<GameObject>();
    int rows = 1;
    float xScale = 1f;
    int columns = 1;
    float yScale = 1f;

    //Generates the tiles with selected settings
    public void GenerateTiles()
    {
        DeleteTiles(); //Auto-Delete Tiles
        //Rows
        float x = sectionSize.x / tileSize.x;
        float decX = x - Mathf.Floor(x);
        if(decX >= 0.5f || decX == 0f) //Scale down
            rows = (int)Mathf.CeilToInt(x);
        else //Scale up
            rows = (int)Mathf.Floor(x);
        xScale = x / rows;

        //Columns
        float y = sectionSize.y / tileSize.y;
        float decY = y - Mathf.Floor(y);
        if (decY >= 0.5f || decY == 0f) //Scale down
            columns = (int)Mathf.CeilToInt(y);
        else //Scale up
            columns = (int)Mathf.Floor(y);
        yScale = y / columns;

        if(randomSeed > 0)
            Random.InitState(randomSeed);
        for(int i = 0; i < rows; i ++)
        {
            for(int j = 0; j < columns; j++)
            {
                int rnd = Random.Range(0, tiles.Count);
                if (tiles[rnd] != null)
                {
                    GameObject tile = GameObject.Instantiate(tiles[rnd]);
                    tile.transform.parent = transform;
                    tile.transform.localRotation = Quaternion.Euler(rotationEuler);
                    tile.transform.localPosition = new Vector3(i * tileSize.x * xScale, 0f, j * tileSize.y * yScale);
                    tile.transform.localScale = new Vector3(xScale, yScale, 1f);
                    generatedTiles.Add(tile);
                }
            }
        }
    }

    //Deletes the generated tiles
    public void DeleteTiles()
    {
        for(int i = 0; i < generatedTiles.Count; i++)
        {
            GameObject.DestroyImmediate(generatedTiles[i]);
        }
        generatedTiles.Clear();
        while(transform.childCount > 0)
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
    }

    //Draws the section to be filled
    private void OnDrawGizmosSelected()
    {
        if (transform.childCount == 0)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(new Vector3(sectionSize.x / 2f, -0.01f, sectionSize.y / 2f), new Vector3(sectionSize.x, 0.001f, sectionSize.y));
        }
    }
}
