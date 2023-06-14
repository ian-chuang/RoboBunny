using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9
public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // if collide with player set its transform's parent to our transform
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // disable parent once player exits
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
