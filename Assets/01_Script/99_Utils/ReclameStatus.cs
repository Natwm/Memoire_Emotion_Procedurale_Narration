[System.Serializable]
public class ReclameStatus
{
    public Character character;
    public Object_Button.ObjectStatus status;


    public ReclameStatus(Character character, Object_Button.ObjectStatus status)
    {
        this.character = character;
        this.status = status;
    }
}