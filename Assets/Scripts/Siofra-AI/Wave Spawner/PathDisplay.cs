using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplay : MonoBehaviour
{
    [SerializeField] private Transform leftPath;
    [SerializeField] private Transform rightPath;

    [SerializeField] private LineRenderer leftPathRenderer;
    [SerializeField] private LineRenderer rightPathRenderer;

    private int index = 0;

    void Awake()
    {
        index = 0;
        foreach (Transform position in leftPath)
        {
            leftPathRenderer.SetPosition(index, position.position);
            index++;
        }

        index = 0;
        foreach (Transform position in rightPath)
        {
            rightPathRenderer.SetPosition(index, position.position);
            index++;
        }
    }
}
