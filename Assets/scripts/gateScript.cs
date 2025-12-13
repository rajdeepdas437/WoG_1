using UnityEngine;

public class gateScript : MonoBehaviour
{
    public BoxCollider2D gate;
    public void OnTriggerExit2D(Collider2D collision)
    {
        gate.isTrigger=false;
    }
}
