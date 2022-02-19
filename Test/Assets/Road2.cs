using System.Collections;
using UnityEngine;

namespace Assets
{
    public class Road2 : MonoBehaviour
    {
        [SerializeField]
        private float posX;
        [SerializeField]
        private float posY;
        [SerializeField] Material[] mts;
        public int delcount = 0;[SerializeField] bool ui = false;
        public Material GetMaterial() { return mts[pcolor]; }
        private int pcolor;
        [SerializeField] private offcorm[] Offcorms;
        [SerializeField] offcorm newcorm;
        [SerializeField] GameObject Quad;
        private float[] positions = { 0.16f, 0.0f, 0.234f };
        private float[] positionX = { -0.3f, -0.159f, 0, 0.145f, 0.3f };
        public bool getpause()
        {
            return gamePaused;
        }
        bool gamePaused;
        public void Paused()
        {
            gamePaused = true;
            Time.timeScale = 0f;
        }
        public void newStart2()
        {
            int layerlevel = Offcorms.Length;
            if (FindObjectOfType<CollShip>().viewLevel() > Offcorms.Length) { layerlevel = Offcorms.Length; }
            else { layerlevel = FindObjectOfType<CollShip>().viewLevel(); }
            GameObject worm = Instantiate(Offcorms[Random.Range(0,layerlevel)].gameObject);
            worm.transform.SetParent(Quad.transform);
            worm.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            worm.transform.localPosition = new Vector3(positionX[Random.Range(0, positionX.Length)], positions[Random.Range(0, positions.Length)],-2);
            GameObject worm2 = Instantiate(Offcorms[Random.Range(0, layerlevel)].gameObject);
            worm2.transform.SetParent(Quad.transform);
            worm2.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f); 
            worm2.transform.localPosition = new Vector3(positionX[Random.Range(0, positionX.Length)], positions[Random.Range(0, positions.Length)],-2);
            GameObject worm3 = Instantiate(Offcorms[Random.Range(0, layerlevel)].gameObject);
            worm3.transform.SetParent(Quad.transform);
            worm3.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            worm3.transform.localPosition = new Vector3(positionX[Random.Range(0, positionX.Length)], positions[Random.Range(0, positions.Length)],-2);        
        }
        public void newStart()
        {
            GameObject worm = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position + new Vector3(posX, -2.5f, positions[1]), Quaternion.identity);
            GameObject worm2 = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position - new Vector3(posX, posY, 0), Quaternion.identity);
            GameObject worm3 = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position + new Vector3(posX - 1.5f, -2.5f, 0), Quaternion.identity);
            GameObject worm4 = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position + new Vector3(posX + 1.5f, -2.5f, 0), Quaternion.identity);
            GameObject worm5 = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position - new Vector3(posX + 1.5f, posY, 0), Quaternion.identity);
            GameObject worm6 = (GameObject)MonoBehaviour.Instantiate(Offcorms[pcolor].gameObject, transform.position - new Vector3(posX - 1.5f, posY, 0), Quaternion.identity);
            GameObject worm7 = (GameObject)MonoBehaviour.Instantiate(newcorm.gameObject, transform.position + new Vector3(-posX - 1.5f, -2.5f, 0), Quaternion.identity);
            GameObject worm8 = (GameObject)MonoBehaviour.Instantiate(newcorm.gameObject, transform.position - new Vector3(-posX, posY, 0), Quaternion.identity);
            GameObject worm9 = (GameObject)MonoBehaviour.Instantiate(newcorm.gameObject, transform.position + new Vector3(-posX + 1.5f, -2.5f, 0), Quaternion.identity);
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}