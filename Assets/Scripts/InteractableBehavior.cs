using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{

    public float hitSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BreakSpell"))
        {
            if(gameObject.GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
            if (gameObject.CompareTag("Interactable"))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * hitSpeed, ForceMode.Impulse);
            }
        }
    }
}
