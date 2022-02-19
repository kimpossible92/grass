using System.Collections;
using UnityEngine;
using Gameplay.Spaceships;
using Gameplay.ShipSystems;
using System.Collections.Generic;

public class CollShip : MonoBehaviour
{
    public void setLevel(int level)
    {
        Level = level;
    }
    public int viewLevel() { return Level; }
    [SerializeField]int Level = 0;
    public enum slojnost
    {
        easy = 1, normal = 3, hard = 5, nevosmojno = 10
    }
    public slojnost OnSlojnost;
    public static bool isInvincible = false;
    public static float timeSpentInvincible;
    public Texture2D lifeIconTexture;
    public static bool dead = false;
    public static int life = 100;
    [SerializeField] LayerMask layer; public float speed2 = 0.04f;
    public float speed = 0.1f;[SerializeField]
    bool showGUI = true;
    public float limitx1 = -2, limitx = 16f, limity1 = -1, limity = 7;
    // NEED TO ADD
    public static Vector2 bombermanPosition, bombermanPositionRounded;
    Vector2 dir2; int dr = 5;
    Animator anim; Vector3 startpos1;
    [SerializeField]GameObject naturalPrefab;
    [SerializeField]
    GameObject naturalPrefab2;
    [SerializeField] GameObject[] Cubs= new GameObject[3];
    public void NewStart()
    {
        dead = false;
        life = 100;
        startpos1 = transform.position;
    }
    // Use this for initialization
    private void Start()
    {
        NewStart();
    }
    private void Update()
    {
        if (dead)
        {
            //Application.LoadLevel("SampleScene"); 
            foreach (var pi in FindObjectsOfType<Gameplay.Spawners.Spawner>())
            {
                pi.StopSpawn();
            }
            GameObject.Find("Canvas").transform.Find("New Game").gameObject.SetActive(true);
        }
        if (isInvincible)
        {
            timeSpentInvincible += Time.deltaTime;

            if (timeSpentInvincible < 3f)
            {
                float remainder = timeSpentInvincible % .3f;
                GetComponent<Renderer>().enabled = remainder > .15f;
            }

            else
            {
                GetComponent<Renderer>().enabled = true;
                isInvincible = false;
            }
        }
    }
    void DisplayLifeCount()
    {
        Rect lifeIconRect = new Rect(10, 150, 32, 32);
        GUI.DrawTexture(lifeIconRect, lifeIconTexture);

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;

        Rect labelRect = new Rect(lifeIconRect.xMax + 10, lifeIconRect.y, 60, 32);
        GUI.Label(labelRect, types.Count.ToString(), style);
    }
    void OnGUI()
    {
        if (!showGUI)
            return;
        DisplayLifeCount();
    }
    private List<int> types = new List<int>();
    public void AddList(List<int> list)
    {
        foreach(var t in types)
        {
            list.Add(t);
        }
    }
    public bool isnull()
    {
        return types.Count >= 1;
    }
    public void clearList()
    {
        types.Clear();
        //types = new List<int>();
    }
    bool ingredientFly = false;
    IEnumerator StartAnimateIngredientOther(GameObject item)
    {
        //if (ingrCountTarget[i] > 0)
        //ingrCountTarget[i]--;

        ingredientFly = true;
        GameObject ingr = Cubs[0].gameObject;//

        //if (targetBlocks > 0)
        //{
        //    ingr = blocksObject.transform.gameObject;
        //}
        AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, ingr.transform.position.x));
        AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.5f, ingr.transform.position.y));
        curveY.AddKey(0.2f, item.transform.localPosition.y + UnityEngine.Random.Range(-2, 0.5f));
        float startTime = Time.time;
        Vector3 startPos = item.transform.localPosition;
        float speed = UnityEngine.Random.Range(0.4f, 0.6f);
        float distCovered = 0;
        while (distCovered < 0.5f&&item!=null)
        {
            distCovered = (Time.time - startTime) * speed;
            item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);
            //item.transform.Rotate(Vector3.back, Time.deltaTime * 1000);
            yield return new WaitForFixedUpdate();
        }
        //     SoundBase.Instance.audio.PlayOneShot(SoundBase.Instance.getStarIngr);
        Destroy(item);
        //if (gameStatus == GameState.Playing && !IsIngredientFalling())//1.6.1
        //    CheckWinLose();
        ingredientFly = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "w")
        {
            if (types.Count < 75)
            {
                types.Add(collision.GetComponent<Block>().types);
                Destroy(collision.gameObject);
            }//life -= (int)OnSlojnost;
            //if (life <= 0)
            //{
            //    dead = true;
            //}
            //isInvincible = true;
            //timeSpentInvincible = 0;
        }
        if (collision.gameObject.tag == "bonus")
        {
            
            if (types.Count > 30) { Cubs[0].SetActive(true); }
            else { Cubs[0].SetActive(false); }
            if (types.Count > 45) { Cubs[1].SetActive(true); }
            else { Cubs[1].SetActive(false); }
            if (types.Count > 70) { Cubs[2].SetActive(true); }
            else{ Cubs[2].SetActive(false); }
            if (types.Count < 75) {
                var natural2 = Instantiate(naturalPrefab2, collision.gameObject.transform);
                StartCoroutine(StartAnimateIngredientOther(natural2));
                types.Add(collision.GetComponent<Block>().types); 
            }
            else
            {
                var natural = Instantiate(naturalPrefab, collision.gameObject.transform);
                natural.GetComponent<Block>().types = collision.gameObject.GetComponent<Block>().types;
            }
            collision.GetComponent<Block>().DestroyBlocks();
            if (collision.GetComponent<Block>().types == 0) {  }
            if (collision.GetComponent<Block>().types == 1) {  }
            if (collision.GetComponent<Block>().types == 2) {  }
            if (collision.GetComponent<Block>().types == 3) {  }
            if (collision.GetComponent<Block>().types == 4) {  }
        }
    }
}