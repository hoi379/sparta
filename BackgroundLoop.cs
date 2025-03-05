using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime);

        if(this.transform.position.x <= -38f)
        {
            this.transform.position = Vector3.zero;
        }
    }
}
