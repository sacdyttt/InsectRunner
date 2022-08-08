using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
using UnityEngine.UI;


public class PlayerIdleController : MonoBehaviour
{
    public Joystick Joystick;
    public float turnsmoothvelocity, turnsmoothtime;
    public GameObject FirePRT;
    public GameObject[] Destroyables;
    public Slider StatusSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroyables = GameObject.FindGameObjectsWithTag("Destoryable");
        StatusSlider.maxValue = Destroyables.Length - 15;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = SimpleInput.GetAxisRaw("Horizontal");
        float Vertical = SimpleInput.GetAxisRaw("Vertical");



        if (Joystick.joystickHeld)
        {
            print("hey");
            transform.GetComponentInChildren<Animator>().SetBool("Run", true);
            Vector3 direction = new Vector3(Horizontal, 0, Vertical).normalized;
            float targetangle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothtime);
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
        else
        {
            transform.rotation = transform.rotation;
            transform.GetComponentInChildren<Animator>().SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            other.gameObject.SetActive(false);
            this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }

        else if (other.gameObject.CompareTag("Destoryable"))
        {
            StatusSlider.value += 1;
            Vector3 Pos = new Vector3(other.transform.position.x, 2, other.transform.position.z);
            GameObject Fire = Instantiate(FirePRT, Pos, Quaternion.identity);
            Destroy(Fire, 3);
            Destroy(other.gameObject, 0);
            Destroyables = GameObject.FindGameObjectsWithTag("Destoryable");
            if(Destroyables.Length < 15)
            {
                GameMG.FindObjectOfType<GameMG>().OnGameFinishFinal();
            }
        }
    }


}