using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    public float wait;
    public float redDelay;
    public float yellowDelay;
    public float greenDelay;
    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;
    public Collider trafficCollider;

    void Awake()
    {
        Green.SetActive(false);
        Yellow.SetActive(false);
        if (wait >= 0)
        {
            Red.SetActive(true);
        }
    }

    private IEnumerator TrafficLightCycle()
    {
        yield return new WaitForSeconds(wait);
        while (true)
        {
            Green.SetActive(true);
            Red.SetActive(false);
            Yellow.SetActive(false);
            trafficCollider.enabled = false;
            yield return new WaitForSeconds(greenDelay);

            Yellow.SetActive(true);
            Green.SetActive(false);
            Red.SetActive(false);
            trafficCollider.enabled = false;
            yield return new WaitForSeconds(yellowDelay);

            Red.SetActive(true);
            Yellow.SetActive(false);
            Green.SetActive(false);
            trafficCollider.enabled = true;
            yield return new WaitForSeconds(redDelay);
        }
    }

    void Start()
    {
        Yellow.SetActive(false);
        StartCoroutine(TrafficLightCycle());
    }
}
