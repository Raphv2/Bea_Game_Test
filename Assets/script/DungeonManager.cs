using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{

    bool instancier =true;
    [Range(50, 2000)]
    public int totalFloorCount = 500;
    [Range(5, 20)]
    public int roomSize;

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject requiredPrefab;
    public GameObject playerObject;
    public GameObject cornerPrefab;

    public Quaternion rotate;
    [HideInInspector]
    public float minX;

    [HideInInspector]
    public float maxX;

    [HideInInspector]
    public float minY;

    [HideInInspector]
    public float maxY;

    [NotNull]
    private static readonly Vector3[] Directions = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    [NotNull]
    private readonly List<Vector3> _floorList = new List<Vector3>();

    [NotNull]
    private readonly List<Vector3> _doorList = new List<Vector3>();

    private static Vector3 RandomDirection() => Directions[Random.Range(0, Directions.Length)];

    private void Start()
    {
       
        
        RoomWalker(); 
        WallBuild(40);
        FloorBuild();
        SpawnPlayer();
    }

    private void Update()
    {
        // Reload scene on hotkey.
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    private void RoomWalker()
    {
        
        
        var curPos = Vector3.zero;
        _floorList.Add(curPos);

        while (_floorList.Count < totalFloorCount)
        {
            RandomRoom(curPos);
            RandomRoom(curPos);
            //TakeLongWalk(ref curPos);


        }
    }

    private void RandomRoom(Vector3 position)
    {
        var width = Random.Range(0, roomSize);
        var height = Random.Range(0, roomSize);
        for (var x = -width; x <= width; ++x)
        {
            for (var z = -height; z <= height; ++z)
            {
                var offset = new Vector3(x, 0, z);
                var candidateTile = position + offset;

                if (_floorList.Contains(candidateTile)) continue;
                _floorList.Add(candidateTile);


                if (x == -width + 1 || x == width - 1)
                {
                    if (z == -height + 1 || z == height - 1)
                    {

                        Vector2Int prefabPos = new Vector2Int((int)candidateTile.x, (int)candidateTile.z);

                        Quaternion prefabRotation;
                        if (x == -width + 1 && z == -height + 1) // Coin supérieur gauche
                        {
                            prefabRotation = Quaternion.Euler(0, 90, 0);
                        }
                        else if (x == -width + 1 && z == height - 1) // Coin inférieur gauche
                        {
                            prefabRotation = Quaternion.Euler(0, 180, 0);
                        }
                        else if (x == width - 1 && z == height - 1) // Coin inférieur droit
                        {
                            prefabRotation = Quaternion.Euler(0, -90, 0);
                        }
                        else // Coin supérieur droit
                        {
                            prefabRotation = Quaternion.Euler(0, 0, 0);
                        }
                        PlacePrefab(prefabPos, Vector2Int.one,0, cornerPrefab, 14, 10, prefabRotation);
                    }
                }
            }
        }
    }



    private void RandomChaoticRoom(Vector3 position)
    {
        var width = 5;
        var height = 5;
        for (var x = -width; x <= width; ++x)
        {
            for (var z = -height; z <= height; ++z)
            {
                var offset = new Vector3(x, 0, z);
                var candidateTile = position + offset;

                if (_floorList.Contains(candidateTile)) continue;
                _floorList.Add(candidateTile);


            }
        }
    }

    private void TakeLongWalk(ref Vector3 curPos)
    {
        var walkDirection = RandomDirection();
        Quaternion rotate =  Quaternion.LookRotation(new Vector3(walkDirection.x, 0, walkDirection.y), Vector3.up);
        var walkLength = 18;

        Vector2Int prefabPos = new Vector2Int((int)curPos.x, (int)curPos.z);
        PlacePrefab(prefabPos, Vector2Int.one,0, doorPrefab, 14, 10, rotate);
        if (instancier)
        {
            GameObject go = Instantiate(requiredPrefab, curPos, Quaternion.identity);

            instancier = false;
        }

        for (var i = 0; i < walkLength; ++i)
        {
            curPos += new Vector3(walkDirection.x, 0, walkDirection.y);
            
            
            if (_floorList.Contains(curPos)) continue;
            _floorList.Add(curPos);
        }

        Vector2Int prefabPosEnd = new Vector2Int((int)curPos.x, (int)curPos.z);
        PlacePrefab(prefabPosEnd, Vector2Int.one,0, doorPrefab, 14, 10, rotate);
    }


    private void PlacePrefab(Vector2Int location, Vector2Int size,int plafond, GameObject prefab, int height, int xSize, Quaternion rotation)
    {
        Vector3 position = new Vector3(location.x * 10, plafond, location.y * 10); // Ajuster la conversion des coordonnées
        GameObject go = Instantiate(prefab, position, rotation);
        go.GetComponent<Transform>().localScale = new Vector3(size.x * xSize, height, size.y * 10);  
    }

    private void WallBuild(int height)
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        foreach (Vector3 position in _floorList)
        {
            Vector2Int pos = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));

            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighborPos = pos - direction;

                if (!_floorList.Contains(new Vector3(neighborPos.x, 0, neighborPos.y)))
                {
                    // Calculer la rotation en fonction de la direction du mur
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);

                    PlacePrefab(neighborPos, Vector2Int.one,0, wallPrefab, height, 10, rotation);
                }
            }
        }
    }

    private void FloorBuild()
    {
        foreach (Vector3 position in _floorList)
        {
            Vector2Int pos = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
            PlacePrefab(pos, Vector2Int.one, 0, floorPrefab, 1, 10, Quaternion.identity);
           // PlacePrefab(pos, Vector2Int.one,15, floorPrefab, 1, 10, Quaternion.identity);
        }
    }

    private void SpawnPlayer()
    {
        // Sélectionnez un point de spawn aléatoire parmi les positions de sol disponibles
        Vector3 playerSpawnPoint = _floorList[Random.Range(0, _floorList.Count)];

        // Trouvez le préfabriqué du joueur dans la scène


        // Placez le préfabriqué du joueur au point de spawn
         playerObject.GetComponent<CharacterController>().Move(transform.position + playerSpawnPoint);
        //playerObject.transform.SetPositionAndRotation(transform.position +playerSpawnPoint, Quaternion.identity);
        // Placez le préfabriqué obligatoire à côté du joueur
        Vector2Int playerPos = new Vector2Int(Mathf.FloorToInt(playerSpawnPoint.x), Mathf.FloorToInt(playerSpawnPoint.z));
        Vector2Int neighborPos = playerPos + Vector2Int.right; // Décalez d'une unité à droite du joueur
        Vector3 position = new Vector3(neighborPos.x * 10, 0, neighborPos.y * 10);

        
    }



}
