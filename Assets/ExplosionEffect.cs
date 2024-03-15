using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private Vector3 endScale = new Vector3(0.05f, 0.05f, 0.05f);

    // Start is called before the first frame update

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, endScale, 0.175f);

        if (Vector3.Distance(transform.localScale, endScale) <= 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
