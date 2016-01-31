using UnityEngine;
using System.Collections;

public class kill : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        var plyr = other.GetComponent<PlayerController>();
        if (plyr)
        {
            print(plyr);
        }
    }
}
