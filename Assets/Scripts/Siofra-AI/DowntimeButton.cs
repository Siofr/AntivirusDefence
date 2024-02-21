using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DowntimeButton : MonoBehaviour
{
    public UnityEvent buttonDisabled;
    // Start is called before the first frame update
    private void OnDisable()
    {
        buttonDisabled.Invoke();
    }
}
