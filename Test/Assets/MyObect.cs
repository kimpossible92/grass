using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObect : MonoBehaviour
{
    public void Rotations(Vector3 v)
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(v.x, v.y, 0));
    }
    public void TransformPostion(Vector3 point)
    {
        transform.position = point;
    }
    int score = 0;[SerializeField] float ypos;
    private void OnGUI()
    {
        GUI.Label(new Rect(20, ypos, 140, 20), score.ToString());
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "corm" || coll.gameObject.tag == "rub")
        {
            score++;
        }
    }
}
