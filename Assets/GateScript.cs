using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(openGate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator openGate()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
