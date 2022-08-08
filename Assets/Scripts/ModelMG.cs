using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelMG : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] MyMaterial;
    public Texture[] textures;
    public float changeInterval;
    public float speed = 3;
    public TextMeshPro Txt;
    public int MyValue;

    private void Awake()
    {
        Txt = transform.GetComponentInChildren<TextMeshPro>();
        MyValue = Random.Range(5, 30);
        Txt.text = MyValue.ToString();
    }
    private void Start()
    {

    }
    
    private void Update()
    {
        UpdateTexture();
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.back, out hit, 3.0f))
        {
            if(hit.collider.gameObject.CompareTag("MGate") || hit.collider.gameObject.CompareTag("Gate"))
            {
                if(hit.distance < 3f)
                {
                    transform.GetComponent<Animator>().SetBool("Jump", true);
                    StartCoroutine(JumpStop());
                }
            }
        }
    }

    IEnumerator JumpStop()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<Animator>().SetBool("Jump", false);
    }

    public void UpdateTexture()
    {
        /* if (textures.Length == 0)
             return;

         int index = Mathf.FloorToInt(Time.time / changeInterval);
         index = index % textures.Length;
         MyMaterial[0].mainTexture = textures[index];*/
        transform.Translate(Vector3.forward * speed * Time.deltaTime); 
    }

    
}
