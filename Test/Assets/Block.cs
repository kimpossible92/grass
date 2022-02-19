using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //public HitCandy candy;
    public int row;
    public int col;
    public int types;
    public List<GameObject> block = new List<GameObject>();
    private int ccc;
    public bool emptyes = false;
    [SerializeField] Sprite GetSprite1;
    [HideInInspector] public int modelvlsquare;
    public int addScore;
    public GameObject prefab;
    [SerializeField] Vector2 position;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestroyBlocks()
    {
        prefab.SetActive(false);
        Invoke("Spawn", 10.0f);
    }
    public void Spawn()
    {
        //Instantiate(prefab, position, prefab.transform.rotation);
        prefab.SetActive(true);
    }
}
public class SquareBlocks
{
    public int blck;
    public void Changeblck(int bl) { blck = bl; }
    public int block() { return blck; }
    public int obstacle;
}

