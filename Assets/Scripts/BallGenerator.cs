using System.Collections;
using UnityEngine;
using Random = System.Random;

public class BallGenerator : MonoBehaviour {
    public GameObject ballHolder;
    public GameObject[] ballTypes;
    public float waitTime;
    public int maxDelay;

    public void StartGeneration() {
        StartCoroutine("GenerateBalls");
    }

    IEnumerator GenerateBalls() {
        Random random = new Random();
        yield return new WaitForSeconds(waitTime);
        for (;;) {
            var ballToInstantiate = ballTypes[UnityEngine.Random.Range(0, ballTypes.Length)];
            Instantiate(ballToInstantiate, this.gameObject.transform.position, Quaternion.identity, ballHolder.transform);
            yield return new WaitForSeconds(waitTime + random.Next(0, maxDelay));
        }
    }

    public void StopGeneration() {
        StopCoroutine("GenerateBalls");
        foreach (Transform child in ballHolder.transform) {
            Destroy(child.gameObject);
        }
    }
}