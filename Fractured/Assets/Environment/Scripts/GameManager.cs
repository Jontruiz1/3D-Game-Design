using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject key;
    public EnemyAI  enemy;
    public GameObject cabinKey;
    public GameObject pauseScreen;

    //public Transform[] positions;
    public List<Transform> positions = new List<Transform>();
    private List<GameObject> active = new List<GameObject>();

    // list of available indecies in the positions
    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }
    void Start()
    {
        // Spawns keys in
        StartCoroutine(spawnKeys());
    }
    public bool pauseGame()
    {

        if (!pauseScreen.activeSelf) 
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseScreen.SetActive(true);
            return true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseScreen.SetActive(false);
            return false;
        }
    }

    public void startHunt()
    {
        // initiates the hunt ending sequence
        enemy.startHunt();
    }

    public void pickUp(GameObject pos)
    {
        // look for the key that we just picked up
        for(int i = 0; i < active.Count; ++i)
        {
            if(Vector3.Distance(active[i].transform.position, pos.transform.position) < 10f)
            {
                // remove the key that we just picked up from the active key list
                active.RemoveAt(i);
                Destroy(pos); 
                return;
            }
        }

    }

    public void changeArea(Collider c)
    {
        enemy.changeArea(c);
    }

    public IEnumerator spawnKeys()
    {
        for (int i = 0; i < 5; ++i)
        {
            // generate a random position within the sphere collider bounds and place key
            Bounds bound = positions[i].gameObject.GetComponent<SphereCollider>().bounds;
            Vector3 keypos = 
                new Vector3(Random.Range(bound.min.x, bound.max.x),
                40.7f, Random.Range(bound.min.z, bound.max.z));

            // store the gameobject for the key in a list to access later
            GameObject currkey = Instantiate(key, keypos, Quaternion.identity);
            active.Add(currkey);
            key.tag = "Keys";
            yield return null;
        }
    }

    public void spawnCabinKey() {
        cabinKey = active[Random.Range(0, active.Count)];
        cabinKey.tag = "Cabin Key";
    }
    
    
}
