using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProjectile : MonoBehaviour
{
    [SerializeField] TextMesh textmesh;
    List<GameObject> wList2 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _Spawn();
    }
    [SerializeField] private GameObject SetObject;
    [SerializeField]
    private GameObject[] unitPrefabs;
    Vector3[] _positionsText = { new Vector3(12, -4, 51), new Vector3(15, 9, 51), new Vector3(25, 0, 51) };
    Vector3 rotateStart, rotateKoef;
    Vector3 positionStart, positionKoef;
    Vector3 myPoint = Vector3.right;
    void _Spawn()
    {
        for (int i = 0; i < unitPrefabs.Length; i++)
        {
            GameObject worm = MonoBehaviour.Instantiate(this.unitPrefabs[i], new Vector3(1, 0, 0) * (i * 2), Quaternion.identity) as GameObject;
            SetObject = worm;
        }
        for (int i = 0; i < 3; i++)
        {
            _TextMesh(_positionsText[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            positionStart = Input.mousePosition;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rotateStart = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            rotateKoef = rotateStart - Input.mousePosition;
            SetObject.GetComponent<MyObect>().Rotations(new Vector3(rotateKoef.x, rotateKoef.y, 0));
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                myPoint = new Vector3(raycastHit.point.x, raycastHit.point.y, 0);
            }
        }
        if (SetObject != null)
        {
            SetObject.transform.position = (Vector3.MoveTowards(
            SetObject.transform.position,
            myPoint, Time.deltaTime*5f));

            float[] _points = { myPoint.x, myPoint.y, myPoint.z };
            for (int i = 0; i < 3; i++)
            {
                if (wList2[i] != null) wList2[i].GetComponent<TextMesh>().text = _points[i].ToString();
            }
        }
    }
    void _TextMesh(Vector3 _position)
    {
        GameObject worm = MonoBehaviour.Instantiate(this.textmesh.gameObject, _position, Quaternion.identity) as GameObject;
        wList2.Add(worm);
        new WaitForSeconds(0.001f);
    }
}
