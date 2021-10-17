using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public List<GameObject> listOfObjectToSpawn;
    public Transform parent;
    public string Chemin;

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
        SpawnObject(PlayerManager.instance.AmountOfCardToDraw);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        Object[] listOfSO = Resources.LoadAll(Chemin, typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        print(nbElement);
        foreach (var item in listOfObjectToSpawn)
        {
            GameObject card = Instantiate(item, parent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();

            int elt = Random.Range(0, nbElement);
            cardBd.SetUpCard();

            PlayerManager.instance.HandOfVignette.Add(cardBd);
        }
    }

    public void SpawnObject(int amount)
    {
        Object[] listOfSO = Resources.LoadAll(Chemin, typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        print(nbElement);
        for (int i = 0; i < amount; i++)
        {
            int vignette = Random.Range(0, listOfObjectToSpawn.Count);

            GameObject item = listOfObjectToSpawn[vignette];

            GameObject card = Instantiate(item, parent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();

            int elt = Random.Range(0, nbElement);
            cardBd.SetUpCard();


            //Création de chaque clase de vignette
            PlayerManager.instance.HandOfVignette.Add(cardBd);
            Bd_Component.bd_instance.SetVignetteToOject(card);
            CastingManager.instance.SetCharactersToHand();
        }
    }
}
