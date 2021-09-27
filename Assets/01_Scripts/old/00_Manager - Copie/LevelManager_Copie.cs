using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_Copie : MonoBehaviour
{

    public static LevelManager_Copie instance;
    public List<GameObject> listOfObjectToSpawn;
    public float radius;
    public Transform parent;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : LevelManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjectInTheLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjectInTheLevel()
    {
        print("ok");
        foreach (var item in listOfObjectToSpawn)
        {
            Vector3 position = Random.insideUnitSphere * radius;
            print(position);
            GameObject a = Instantiate(item,Vector3.zero, Quaternion.identity,parent);
            //a.transform.position = Vector3.zero;
            float posZ = Mathf.Clamp(position.z, 0, Mathf.Infinity);
            a.transform.position = new Vector3 (position.x, 1, posZ);
            print(a.transform.position);
        }
    }
}
