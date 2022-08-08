using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obst"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayerController.FindObjectOfType<PlayerController>().OnCheckIfPlayerDead();
            Vector3 pos = new Vector3(transform.position.x, 0.5f, transform.position.z);
            PlayerController.FindObjectOfType<PlayerController>().KillChara(pos);
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            PlayerController.FindObjectOfType<PlayerController>().OnDiamondHit(other.transform.position);
        }
    }
}
