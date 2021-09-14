using System.Collections;
using UnityEngine;

public class BotColliders : MonoBehaviour
{
    public Light lumpLeft, lumpRight;


    private int Timer = 1, Timer1 = 1;
    private bool povorot = true, ezda = true;
    private float randNumb = 19f;
    private Rigidbody rb;



    private void Start()
    {
        StartCoroutine(randPos());
        lumpLeft.enabled = false;
        lumpRight.enabled = false;
    }
    
    private void Update()
    {
        //Îñòàíàâëèâàåì ìàøèíó ïğè óäàğå
        if (!ezda)
        {
            transform.Translate(0, 0, -8 * Time.deltaTime);
        }

        //ÏÎÂÎĞÎÒ ÍÀÏĞÀÂÎ
        if (randNumb <= 5)
        {
            if (povorot)
            {
                if (Timer1 > 0)
                {
                    Timer1++;
                }
                if (Timer1 % 10 == 0)
                {
                    lumpRight.enabled = !lumpRight.enabled;
                }
                transform.Translate(0.85f * Time.deltaTime, 0, 0);
            }

        }
        else
        {
            lumpRight.enabled = false;
        }


        //ÏÎÂÎĞÎÒ ÍÀËÅÂÎ

        if (randNumb >= 6 && randNumb <= 11)
        {
            if (povorot)
            {
                if (Timer > 0)
                {
                    Timer++;
                }
                if(Timer % 10 == 0)
                {
                    lumpLeft.enabled = !lumpLeft.enabled;
                }

                transform.Translate(-0.85f * Time.deltaTime, 0, 0);
            }
            
        } else
        {
            lumpLeft.enabled = false;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        //ÅÑËÈ ÑÒÎËÊÍÓËÑß Ñ ÄÎĞÎÃÎÉ ÈËÈ ÄĞÓÃÈÌ ÁÎÒÎÌ ÒÎ ÑÒÎÏ
        if (other.tag == "Bot" || other.tag == "Roads")
        {
            povorot = false;
            lumpLeft.enabled = false;
            lumpRight.enabled = false;
            
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        ezda = false;
        povorot = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        Destroy(gameObject, 1f);

    }

    private IEnumerator randPos()
    {

        while (true)
        {            
            randNumb = Random.Range(0, 20);
            yield return new WaitForSeconds(5f);
        }
    }


}
