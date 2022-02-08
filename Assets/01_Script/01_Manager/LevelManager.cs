using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Space]
    [Header("Spawn Param")]
    public Transform vignetteSpawnParent;
    [SerializeField] private float vignetteAreaSpawnRadius = 1f;
    public List<GameObject> listOfVignettePrefabsToSpawn;

    [Space]
    [Header("pull Inventory")]
    [SerializeField] private List<UsableObject_SO> basisPullOfObject = new List<UsableObject_SO>();
    [SerializeField] private List<UsableObject_SO> healPullOfObject = new List<UsableObject_SO>();
    [SerializeField] private List<UsableObject_SO> occultsPullOfObject = new List<UsableObject_SO>();
    [SerializeField] private List<UsableObject_SO> rarePullOfObject = new List<UsableObject_SO>();

    [Space]
    [Header("Player Hand")]
    [SerializeField] private List<Vignette_Behaviours> handOfVignette;

    public List<Vignette_Behaviours> HandOfVignette { get => handOfVignette; set => handOfVignette = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : LevelManager");
        else
            instance = this;
    }

    public void SpawnObject(List<DrawVignette> inventory)
    {
        //SoundManager.instance.PlaySound_DrawVignette();
        foreach (var toDraw in inventory)
        {
            for (int i = 0; i < toDraw.AmountOfCardToDraw; i++)
            {
                int vignetteShape = Random.Range(0, listOfVignettePrefabsToSpawn.Count);
                GameObject vignette = listOfVignettePrefabsToSpawn[vignetteShape];
                GameObject card = Instantiate(vignette, vignetteSpawnParent.position + Random.insideUnitSphere * vignetteAreaSpawnRadius, Quaternion.identity, vignetteSpawnParent);

                Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
                cardBd.SetUpVignette(toDraw.CategoryToDraw);

                handOfVignette.Add(cardBd);

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
        //SoundManager.instance.PlaySound_DrawCurseVignette();
        for (int i = 0; i < amount; i++)
        {
            int vignette = Random.Range(0, listOfVignettePrefabsToSpawn.Count);

            GameObject item = listOfVignettePrefabsToSpawn[vignette];

            GameObject card = Instantiate(item, vignetteSpawnParent.position + Random.insideUnitSphere * vignetteAreaSpawnRadius, Quaternion.identity, vignetteSpawnParent);
            Vignette_Behaviours cardBd = card.GetComponent<Vignette_Behaviours>();
            cardBd.SetUpVignette(Vignette_Behaviours.VignetteCategories.CURSE/*Vignette_Behaviours.GetRandomNegatifEnum()*/);

            handOfVignette.Add(cardBd);

            card.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(104, 46, 68, 255);

            card.transform.position.Set(card.transform.position.x, card.transform.position.y, -2);
        }
    }

    public void ClearVignette()
    {
        foreach (var item in handOfVignette)
        {
            Destroy(item.gameObject);
        }

        handOfVignette.Clear();
    }

}
