using UnityEngine;

public class plateTriggerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("WORKS!");
        }

    }
}
