using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public List<GameObject> listOfObjectToSpawn;
    public Transform parent;
    public string Chemin;

    [Space]
    private int amountOfpageDone = 0;
    [SerializeField] private float radius = 1;

    [Space]
    [Header("BranchingCondition")]
    [SerializeField] private List<BranchingCondition> nextCondition;
    [Space]
    [SerializeField] private int nextCheck = 0;
    [SerializeField] private BranchingCondition currentBranching;

    [Space]
    [Header("Pull Page")]
    [SerializeField] private List<Object_SO> unlockableObject;

    [Space]
    [Header("Page Inventory")]
    [SerializeField] private List<UsableObject> pageInventory = new List<UsableObject> ();
    [SerializeField] private List<Object_SO> basisPullOfObject = new List<Object_SO>();
    [SerializeField] private List<Object_SO> healPullOfObject = new List<Object_SO>();
    [SerializeField] private List<Object_SO> occultsPullOfObject = new List<Object_SO>();
    [SerializeField] private List<Object_SO> rarePullOfObject = new List<Object_SO>();

    [Space]
    [Header("PARAM")]
    [SerializeField] private int amountOfLevelInventory = 0;

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
        //SpawnObject(PlayerManager.instance.AmountOfCardToDraw);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnObject()
    {
        Object[] listOfSO = Resources.LoadAll(Chemin, typeof(Carte_SO));
        int nbElement = listOfSO.Length;
        foreach (var item in listOfObjectToSpawn)
        {
            GameObject card = Instantiate(item, parent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();

            int elt = Random.Range(0, nbElement);
            cardBd.SetUpCard();

            PlayerManager.instance.HandOfVignette.Add(cardBd);

            card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -2);
        }
    }

    public void SpawnObject(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            int vignette = Random.Range(0, listOfObjectToSpawn.Count);

            GameObject item = listOfObjectToSpawn[vignette];

            GameObject card = Instantiate(item, parent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
            cardBd.SetUpVignette(Vignette_Behaviours.GetRandomEnum());
            cardBd.SetUpCard();


            //Création de chaque clase de vignette
            PlayerManager.instance.HandOfVignette.Add(cardBd);

            card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -2);
        }
    }

    public void SpawnObject(List<Object_SO> inventory)
    {
        foreach (var item in inventory)
        {
            foreach (var toDraw in item.DrawParam)
            {
                for (int i = 0; i < toDraw.AmountOfCardToDraw; i++)
                {
                    int vignetteShape = Random.Range(0, listOfObjectToSpawn.Count);
                    GameObject vignette = listOfObjectToSpawn[vignetteShape];
                    GameObject card = Instantiate(vignette, parent);

                    Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
                    cardBd.SetUpVignette(toDraw.CategoryToDraw, item);

                    PlayerManager.instance.HandOfVignette.Add(cardBd);

                    card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -2);
                }
            }
        }
    }

    public void SpawnObject(Object_SO obj)
    {
            foreach (var toDraw in obj.DrawParam)
            {
                for (int i = 0; i < toDraw.AmountOfCardToDraw; i++)
                {
                    int vignetteShape = Random.Range(0, listOfObjectToSpawn.Count);
                    GameObject vignette = listOfObjectToSpawn[vignetteShape];
                    GameObject card = Instantiate(vignette, parent);

                    Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
                    cardBd.SetUpVignette(toDraw.CategoryToDraw, obj);

                    PlayerManager.instance.HandOfVignette.Add(cardBd);

                    card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -2);
                }
            }
    }

    public void SpawnObject(List<UsableObject> inventory)
    {
        foreach (var item in inventory)
        {
            foreach (var toDraw in item.Data.DrawParam)
            {
                for (int i = 0; i < toDraw.AmountOfCardToDraw; i++)
                {
                    int vignetteShape = Random.Range(0, listOfObjectToSpawn.Count);
                    GameObject vignette = listOfObjectToSpawn[vignetteShape];

                    GameObject card = Instantiate(vignette, parent.position + Random.insideUnitSphere * radius, Quaternion.identity, parent);
                    
                    Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
                    cardBd.SetUpVignette(toDraw.CategoryToDraw, item);

                    PlayerManager.instance.HandOfVignette.Add(cardBd);

                    card.transform.position = new Vector3(card.transform.position.x, card.transform.position.y, -2);
                }
            }
        }
    }

    public void SpawnObject(List<DrawVignette> inventory)
    {
        SoundManager.instance.PlaySound_DrawVignette();
            foreach (var toDraw in inventory)
            {
                for (int i = 0; i < toDraw.AmountOfCardToDraw; i++)
                {
                    int vignetteShape = Random.Range(0, listOfObjectToSpawn.Count);
                    GameObject vignette = listOfObjectToSpawn[vignetteShape];
                GameObject card = Instantiate(vignette, parent.position + Random.insideUnitSphere * radius, Quaternion.identity, parent);

                Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
                    cardBd.SetUpVignette(toDraw.CategoryToDraw);

                    PlayerManager.instance.HandOfVignette.Add(cardBd);

                    card.transform.position.Set(card.transform.position.x, card.transform.position.y, -2);
                }
            }
       /* if (inventory.Count > 0)
        {
            SpawnObject(PlayerManager.instance.InventoryObj);
        }*/
    }

    public void SpawnNegatifObject(int amount = 1)
    {
        SoundManager.instance.PlaySound_DrawCurseVignette();
        for (int i = 0; i < amount; i++)
        {
            int vignette = Random.Range(0, listOfObjectToSpawn.Count);

            GameObject item = listOfObjectToSpawn[vignette];

            GameObject card = Instantiate(item, parent.position + Random.insideUnitSphere * radius, Quaternion.identity, parent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
            cardBd.SetUpVignette(Vignette_Behaviours.GetRandomNegatifEnum());
            //cardBd.SetUpCard();

            card.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(104, 46, 68, 255);

            //Création de chaque clase de vignette
            PlayerManager.instance.HandOfVignette.Add(cardBd);

            card.transform.position.Set(card.transform.position.x, card.transform.position.y, -2);
        }
    }

    public void NewPage()
    {
        if (ChangeNarrativeBranch()) 
        {
            GridManager.instance.ClearScene();
        }
        else
        {
            GridManager.instance.ClearScene();
        }
    }

    #region Branching

    /// <summary>
    /// La méthode permets de vérifier par rapport a la possition actuelle de la branche si 
    /// un character valide les condition nécéssaire afin de passer a une nouvelle branche
    /// </summary>
    /// <returns> retourne un booleen permettant d'utiliser une méthode lier a la nouvelle branche</returns>
    public bool ChangeNarrativeBranch()
    {
        nextCheck--;
        if(nextCheck <= 0)
        {
            // vérifie si un des persos valide la condition
            foreach (Character item in CastingManager.instance.AllCharacters)
            {

                // check dans toutes les branches possible si il en existe au moins une de valide
                foreach (var stepCondition in nextCondition)
                {
                    if (item.currentRole != stepCondition.RoleCondition)
                        return false;

                    switch (stepCondition.EmotionCondition)
                    {
                        // ^ = XOR condition ( ou bien :  true xor false = true && true xor true = False)
                        case EmotionJauge.Jauge_PeurColere:
                            if (item.currentJauge != EmotionJauge.Jauge_PeurColere ^ item.jaugeNumber != stepCondition.JaugeValueCondition)
                                return false;

                            else if (item.currentJauge == EmotionJauge.Jauge_PeurColere && item.jaugeNumber == stepCondition.JaugeValueCondition)
                            {
                                nextCheck = stepCondition.NextCheck; 
                                nextCondition = stepCondition.NextMove;
                                currentBranching = stepCondition;
                                return true;
                            }

                            break;

                        case EmotionJauge.Jauge_TristesseJoie:
                            if (item.currentJauge != EmotionJauge.Jauge_TristesseJoie ^ item.jaugeNumber != stepCondition.JaugeValueCondition)
                                return false;

                            else if (item.currentJauge == EmotionJauge.Jauge_TristesseJoie && item.jaugeNumber == stepCondition.JaugeValueCondition)
                            {
                                nextCheck = stepCondition.NextCheck;
                                nextCondition = stepCondition.NextMove;
                                currentBranching = stepCondition;
                                return true;
                            }
 
                            break;

                        default:
                            break;
                    }

                }
            }
        }
        return false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parent.transform.position , radius);
    }

    #region Getter && Setter

    public int AmountOfpageDone { get => amountOfpageDone; set => amountOfpageDone = value; }
    public BranchingCondition CurrentBranching { get => currentBranching; set => currentBranching = value; }
    public List<UsableObject> PageInventory { get => pageInventory; set => pageInventory = value; }
    public List<Object_SO> UnlockableObject { get => unlockableObject; set => unlockableObject = value; }
    public int AmountOfLevelInventory { get => amountOfLevelInventory; set => amountOfLevelInventory = value; }
    public List<Object_SO> BasisPullOfObject { get => basisPullOfObject; set => basisPullOfObject = value; }
    public List<Object_SO> HealPullOfObject { get => healPullOfObject; set => healPullOfObject = value; }
    public List<Object_SO> OccultsPullOfObject { get => occultsPullOfObject; set => occultsPullOfObject = value; }
    public List<Object_SO> RarePullOfObject { get => rarePullOfObject; set => rarePullOfObject = value; }

    #endregion

}
