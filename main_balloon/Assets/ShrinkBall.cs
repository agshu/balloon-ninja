using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBall : MonoBehaviour
{
    public float timer = 0f;
    public float shrinkTime = 6f;
    public float minSize = 0.5f;

    public bool isMinSize = false;

    // Start is called before the first frame update
    void Start()
    {
        if(isMinSize == false){StartCoroutine(Shrink());}
    
    }

    private IEnumerator Shrink()
    {
        Vector2 startScale = transform.localScale;
        Vector3 minScale = new Vector3(minSize, minSize, minSize);

        do {
            transform.localScale = Vector3.Lerp(startScale, minScale, timer / shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < shrinkTime);
        isMinSize = true;
    }

}
