using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, 0f, -90f * Time.deltaTime));
    }
}
