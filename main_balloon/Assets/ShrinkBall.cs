using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBall : MonoBehaviour
{
    public float timer = 0f;
    public float shrinkTime = 10f;
    public float minSize = 0.05f;

    public bool isMinSize = false;

    // Start is called before the first frame update
    void Start()
    {
        if(isMinSize == false){StartCoroutine(Shrink());}
    
    }

    private IEnumerator Shrink()
    {
        Vector3 startScale = transform.localScale;
        Vector3 minScale = new Vector3(minSize, minSize, minSize);

        do {
            transform.localScale = Vector3.Lerp(startScale, minScale, timer / shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < shrinkTime);
        isMinSize = true;
        if(isMinSize == true){Destroy(gameObject);}
    }

}
