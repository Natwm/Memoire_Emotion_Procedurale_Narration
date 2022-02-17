
public class Vignette_Behaviours_Perte_Objet : Vignette_Behaviours
{
    protected VignetteCategories initCategorie = VignetteCategories.PERTE_OBJET;
    protected string m_VignetteName = "<color=#B5935A>-1<sprite=1 color=#B5935A></color=#B5935A><br>Perte d'objet";

    public override void ApplyVignetteEffect()
    {
        print("LooseObjectEffect");
        Character character = GameManager.instance.CurrentCharacter;

        if (character.InventoryObj.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, character.InventoryObj.Count);
            character.InventoryObj.RemoveAt(index);
            CanvasManager.instance.RemoveObjInPlayerInventory(index);
            //SoundManager.instance.PlaySound_LooseObject();
        }
    }

}
