using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArenaSpawner : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField] GameObject spawnableObject;

    public Camera arCam;
    GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (raycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(hits[0].pose.position);
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null) 
            {
                spawnedObject.transform.position = hits[0].pose.position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnableObject, spawnPosition, Quaternion.identity);
    }
}
