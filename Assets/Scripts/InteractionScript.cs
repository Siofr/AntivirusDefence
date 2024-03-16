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
                                if(UI.target != null)
                                {
                                    if(UI.target.GetComponent<TileScript>())
                                    {
                                        UI.target.GetComponent<TileScript>().tileUI.SetActive(false);
                                    }
                                }
                                //UI.target.GetComponent<DefenceStats>().upgradeUI.SetActive(false);
                                UI.target = target.GetComponent<TileScript>().tower;
                                UI.target.GetComponent<DefenceStats>().upgradeUI.SetActive(true);
                            }
                            else
                            {
                                if(UI.target != null)
                                {
                                    if(UI.target.GetComponent<TileScript>())
                                    {
                                        UI.target.GetComponent<TileScript>().tileUI.SetActive(false);
                                    }
                                }
                                UI.target = target;
                            }
                        }
                        else
                        {
                            if(target.GetComponentInParent<DefenceStats>())
                            {
                                //UI.target.GetComponentInParent<DefenceStats>().upgradeUI.SetActive(false);
                                target.GetComponentInParent<DefenceStats>().upgradeUI.SetActive(true);
                            }
                            UI.target = target;
                        }
                        

                }
            }
        }
    }
}
