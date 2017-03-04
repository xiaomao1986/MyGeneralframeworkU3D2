using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogdingScene : MonoBehaviour {

    float f;
    float f1=0;
    int sum = 1243;
    public GameObject BarImg;
    void Start ()
    {
         f = 1000.0f / sum;
        Debug.Log("f===" + f);

        StartCoroutine(Lod());
    }

    IEnumerator Lod()
    {
        for (int i = 0; i < sum; i++)
        {

            f1 = f1 + f;
            Debug.Log("f1===" + f1 + "---------" + i);
          
            if (f1>999.99)
            {
                f1 = 1000;
            }
            BarImg.GetComponent<RectTransform>().sizeDelta = new Vector2(f1, 100);
            yield return new WaitForSeconds(0.1f);
        }
      

    }

	void Update ()
    {
   
		
	}
}
