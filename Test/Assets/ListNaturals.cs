using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListNaturals : MonoBehaviour
{
    [SerializeField] CollShip collShip;
    [SerializeField] TextMesh text;
    private List<int> types = new List<int>();
    bool ingredientFly = false;
    [SerializeField] GameObject naturalPrefab;
    IEnumerator StartAnimateIngredientOther(GameObject item)
    {
        //if (ingrCountTarget[i] > 0)
        //ingrCountTarget[i]--;

        ingredientFly = true;
        GameObject ingr = text.gameObject;//

        //if (targetBlocks > 0)
        //{
        //    ingr = blocksObject.transform.gameObject;
        //}
        AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, ingr.transform.position.x));
        AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.5f, ingr.transform.position.y));
        curveY.AddKey(0.2f, item.transform.localPosition.y + UnityEngine.Random.Range(-2, 0.5f));
        float startTime = Time.time;
        Vector3 startPos = item.transform.localPosition;
        float speed = UnityEngine.Random.Range(0.2f, 0.4f);
        float distCovered = 0;
        while (distCovered < 0.5f)
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //text.text = types.Count.ToString();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Cube" && collShip.isnull())
        {
            //text.GetComponent<Animator>().SetBool("animate", true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Cube" && collShip.isnull())
        {
            text.GetComponent<Animator>().SetBool("animate", false);
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Cube" && collShip.isnull())
        {
            var ingredient = Instantiate(naturalPrefab, collision.gameObject.transform).gameObject;
            StartCoroutine(StartAnimateIngredientOther(ingredient));
            collShip.AddList(types);
            collShip.clearList();
        }
    }
}
