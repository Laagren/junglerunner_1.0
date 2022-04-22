using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TileData[] listOfTiles;
    public List<TileData> activeTiles = new List<TileData>();
    public GameObject tWall;
    public TileData startTile;
    public Vector3 origin;
    public GameObject player;
    private GameObject currentTile;
    public Player_movement player_script;
    private TileData lastTile;
    private TileData chosenTile;
    TileData.Direction newStartDir;
    private GameObject nextTile;
    private Vector3 newPos;
    private float startTileCounter, removeOneSideTimer;
    private bool spawnCross;
    System.Random rnd = new System.Random();
    void Start()
    {
        newStartDir = TileData.Direction.South;
        lastTile = startTile;
        for (int i = 0; i < 10; i++)
        {
            GenerateTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player_script.leftSideChosen)
        {
            Debug.Log("create");
            Instantiate(tWall, player_script.triggerPosition, Quaternion.identity);          
            player_script.leftSideChosen = false;
        }
        if (player_script.rightSideChosen)
        {
            Instantiate(tWall, player_script.triggerPosition, Quaternion.identity);
            player_script.rightSideChosen = false;
        }
        if (player_script.spawnTile)
        {
            GenerateTile();
            player_script.spawnTile = false;
        }
    }

    private TileData ChooseTile()
    {
        List<TileData> possibleTiles = new List<TileData>();
        

        if (startTileCounter > 4)
        {
            switch (lastTile.exit)
            {
                case TileData.Direction.North:
                    newStartDir = TileData.Direction.South;
                    
                    break;

                case TileData.Direction.East:
                    newStartDir = TileData.Direction.West;
                    
                    break;

                case TileData.Direction.West:
                    newStartDir = TileData.Direction.East;
                    
                    break;
                default:
                    break;
            } 
        }
        else // Spawna 5 raka tiles vid start.
        {
            newStartDir = TileData.Direction.North;
            newPos += new Vector3(0, 0, lastTile.tileSize.y);
            
        }

        foreach (TileData t in listOfTiles)
        {
            if(startTileCounter <= 4 && t.name == "South-North")
            {
                possibleTiles.Add(t);
                startTileCounter++;
            }
            else if (t.entry == newStartDir && t.tileName != lastTile.tileName)
            {
                if (!(t.gameObject.tag == "Bridge" && lastTile.gameObject.tag == "Turn"))
                {
                    possibleTiles.Add(t);
                }
                if (t.gameObject.tag == "Bridge")
                {

                }
                
            }
            //----------<-<
            // z + 40, x +_ 2.501
        }
        return possibleTiles[rnd.Next(0, possibleTiles.Count)]; 
    }

    private void GenerateTile()
    {
        chosenTile = ChooseTile();
        lastTile = chosenTile;
        activeTiles.Add(chosenTile);
        switch (newStartDir)
        {
            case TileData.Direction.South:
                newPos += new Vector3(0, 0, lastTile.tileSize.y);
                break;
            case TileData.Direction.East:
                newPos += new Vector3(-lastTile.tileSize.x, 0, 0);
                break;
            case TileData.Direction.West:
                newPos += new Vector3(lastTile.tileSize.x, 0, 0);
                break;
            default:
                break;
        }
        Instantiate(chosenTile, newPos + origin, chosenTile.transform.rotation);
    }
}
