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

    public void SpawnObject()
    {
        Object[] listOfSO = Resources.LoadAll("", typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        foreach (var item in listOfObjectToSpawn)
        {
            GameObject card = Instantiate(item, parent);
            Bd_Elt_Behaviours cardBd = card.GetComponent<Bd_Elt_Behaviours>();

            int elt = Random.Range(0, nbElement);
            cardBd.Value = listOfSO[elt] as Carte_SO;
            cardBd.SetUpCard();
        }
    }

    public void SpawnObject(int amount)
    {
        Object[] listOfSO = Resources.LoadAll("", typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        print(nbElement);

        for (int i = 0; i < amount; i++)
        {
            GameObject item = listOfObjectToSpawn[i];

            GameObject card = Instantiate(item, parent);
            Bd_Elt_Behaviours cardBd = card.GetComponent<Bd_Elt_Behaviours>();

            int elt = Random.Range(0, nbElement);
            print(elt);
            cardBd.Value = listOfSO[elt] as Carte_SO;
            cardBd.SetUpCard();
        }
    }
}
