using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] StreetTiles;
    public GameObject[] SidewalkRight;
    public GameObject[] SidewalkLeft;
    public List<GameObject> ActiveTiles = new();

    public Transform Player;

    public float ZSpawn = -9;
    public float TileLenght = 3;
    

    public int NumberOfTIles =20    ;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NumberOfTIles; i++)
            SpawnTile(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.position.z - TileLenght *3 > ZSpawn - (NumberOfTIles * TileLenght))
        {
            SpawnTile(Random.Range(0, StreetTiles.Length), Random.Range(0, SidewalkLeft.Length), Random.Range(0, SidewalkRight.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndexStreet, int tileIndexSidewalkLeft, int tileIndexSidewalkRight)
    {
        //Prototype code
        ActiveTiles.Add(Instantiate(StreetTiles[tileIndexStreet], transform.forward * ZSpawn, transform.rotation, transform));
        ActiveTiles.Add(Instantiate(SidewalkLeft[tileIndexSidewalkLeft], transform.forward * ZSpawn + transform.right *3, transform.rotation,transform));
        ActiveTiles.Add(Instantiate(SidewalkRight[tileIndexSidewalkRight], transform.forward * ZSpawn - transform.right * 3, transform.rotation, transform));

        ZSpawn += TileLenght;
    }

    private void DeleteTile()
    {
        for(int i = 0; i <3; i++)
        {
            Destroy(ActiveTiles[0]);
            ActiveTiles.RemoveAt(0);
        }
    }

    private void OnDrawGizmos()
    {
        if(Player != null)
        {
            Gizmos.DrawLine(transform.position, Player.position);
        }


        Vector3 pos = new Vector3(0, 0.5f, ((NumberOfTIles * 3) / 2)-9);
        Vector3 size = new Vector3(9, 1, NumberOfTIles * TileLenght);
        Gizmos.DrawWireCube(pos, size);
    }



}
