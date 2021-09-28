using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public List<GameObject> listOfObjectToSpawn;
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
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObject()
    {
        Object[] listOfSO = Resources.LoadAll("", typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        print(nbElement);
        foreach (var item in listOfObjectToSpawn)
        {
            GameObject card = Instantiate(item, parent);
            Bd_Elt_Behaviours cardBd = card.GetComponent<Bd_Elt_Behaviours>();

            int elt = Random.Range(0, nbElement);
            print(elt);
            cardBd.Value = listOfSO[elt] as Carte_SO;
        }
    }
}
