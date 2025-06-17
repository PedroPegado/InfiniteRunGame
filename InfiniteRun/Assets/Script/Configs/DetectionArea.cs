using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public string _tagTargetDetection = "Player";

    public List<Collider2D> dectectedObjs = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dectectedObjs.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dectectedObjs.Remove(collision);
        }
    }
}
