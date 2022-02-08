[System.Serializable]
public class ReclameStatus
{
    public Character_Button character;
    public Object_Button.ObjectStatus status;


    public ReclameStatus(Character_Button character, Object_Button.ObjectStatus status)
    {
        this.character = character;
        this.status = status;
    }
}