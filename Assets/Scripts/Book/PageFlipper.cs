using UnityEngine;

public class PageFlipper : MonoBehaviour
{
    //public Transform hinge; // das ist der Punkt, an dem die Seite „gebunden“ ist
    private float flipDuration = 1f;

    private bool isFlipping = false;

    public void FlipForward()
    {
        if (!isFlipping)
            StartCoroutine(FlipPage(0, 180));
    }

    public void FlipBackward()
    {
        if (!isFlipping)
            StartCoroutine(FlipPage(180, 0));
    }

    private System.Collections.IEnumerator FlipPage(float startAngle, float endAngle)
    {
        isFlipping = true;
        float t = 0;

        while (t < flipDuration)
        {
           
            t += Time.deltaTime;
            float normalized = t / flipDuration;

            float angle = Mathf.Lerp(startAngle, endAngle, normalized);
            
            this.transform.localRotation = Quaternion.Euler(0, 0, angle);
            
            yield return null;
        }

        this.transform.localRotation = Quaternion.Euler(0, 0, endAngle);
        isFlipping = false;
    }
}