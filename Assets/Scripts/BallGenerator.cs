using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour {
    public GameObject[] ballTypes;
    public float waitTime;
    public int maxDelay;

    void Start() {
        StartCoroutine("GenerateBalls");
    }

    IEnumerator GenerateBalls() {
        System.Random random = new System.Random();
        yield return new WaitForSeconds(waitTime);
        for (;;) {
            var ballToInstantiate = ballTypes[Random.Range(0, ballTypes.Length)];
            Instantiate(ballToInstantiate, this.gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitTime + random.Next(0, maxDelay));
        }
    }
}