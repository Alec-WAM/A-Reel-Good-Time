using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Left Mouse
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                //Get clicked gameobject
                GameObject gameObject = hit.collider.gameObject;
                if(gameObject != null)
                {
                    //Check if it is a fishing zone
                    FishingZone fishZone = gameObject.GetComponent<FishingZone>();
                    if(fishZone != null)
                    {
                        fishZone.onClick();
                        return;
                    }
                }
            }
        }
    }
}
