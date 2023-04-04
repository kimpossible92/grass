using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNaturals : MonoBehaviour
{
    [SerializeField] Gameplay.ShipName.PlayerShip playerShip;
    [SerializeField] PlayerC _playerC;
    [SerializeField] GameObject oldparent;
    Vector3 oldPos = Vector3.zero;
    Vector3 oldpos2 = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = _playerC.transform.localPosition;
        oldpos2 = playerShip.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerC>()!=null)
        {
            print("coll");
            var pl = collider.gameObject;
            pl.gameObject.transform.SetParent(playerShip.transform);
            pl.transform.localPosition = new Vector3(0, -4.2f, -2.6f); 
            playerShip.Set_activated();
        }
        if (collider.gameObject.GetComponent<CollisionParam>() != null)
        {
            print("coll2"); playerShip.Unset_Activated();
            _playerC.transform.SetParent(oldparent.transform);
            _playerC.transform.localPosition = oldPos;
            _playerC.transform.rotation = Quaternion.identity;
            playerShip.transform.position = oldpos2;
        }
    }
}
