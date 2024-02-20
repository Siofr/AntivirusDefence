using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public GameObject tower;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject cube;
    public GameObject menu;
    private Touch touchInput;
    public Button test1Button, test2Button, test3Button;

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
                    if (hit.transform.gameObject.tag == "Tile" && hit.transform.position == cube.transform.position)
                    {
                        
                    }

                    else if (hit.transform.gameObject.tag == "Enemy")
                    {
                        //call ui script function
                    }

                    else if (hit.transform.gameObject.tag == "Tower")
                    {
                        //call ui script function
                    }
                }
            }
        }
    }

    void test1Instantiate()
    {
        GameObject h = Instantiate(tower, cube.transform.position, Quaternion.identity);
        h.transform.localScale = new Vector3(20, 20, 20);
        
        cube.SetActive(false);
        
    }
    void test2Instantiate()
    {
        GameObject h = Instantiate(tower1, cube.transform.position, Quaternion.identity);
        h.transform.localScale = new Vector3(20, 20, 20);
        
        cube.SetActive(false);
        
    }
    void test3Instantiate()
    {
        GameObject h = Instantiate(tower2, cube.transform.position, Quaternion.identity);
        h.transform.localScale = new Vector3(20, 20, 20);
        
        cube.SetActive(false);
        
    }
}
