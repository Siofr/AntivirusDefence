using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    private Touch touchInput;
    private GameObject target;
    public UIScript UI;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchInput = Input.GetTouch(0);
            if (touchInput.phase == TouchPhase.Ended)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(raycast, out hit))
                {
                        target = hit.transform.gameObject;
                        if(target.GetComponent<TileScript>())
                        {
                            if (target.GetComponent<TileScript>().tower != null)
                            {
                                UI.target = target.GetComponent<TileScript>().tower;
                            }
                            else
                            {
                                UI.target = target;
                            }
                        }
                        else
                        {
                            UI.target = target;
                        }
                        

                }
            }
        }
    }
}
