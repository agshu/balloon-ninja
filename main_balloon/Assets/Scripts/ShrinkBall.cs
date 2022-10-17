using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ShrinkBall : MonoBehaviour
{
    public float timer = 0f;
    public float shrinkTime = 6f;
    public float minSize = 0.5f

    public bool isMinSize = false;

    void Start()
    {
        if(isMinSize == false)
        {
            StartCoroutine(Shrink());
        }
        if(isMinSize == false)
        {
            this.Destroy()
        } 
    }

    private IEnumerator Shrink()
    {
        Vector2 startScale = transform.localScale;
        Vector2 minScale = new Vector2(minSize, minSize);

        do
        {
            transform.localScale = Vector3.Lerp(startScale, minScale, timer / shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < shrinkTime);

        isMinSize = true;
    }

}
*/