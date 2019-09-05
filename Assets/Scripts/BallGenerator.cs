using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour {
    public GameObject[] ballTypes;

    void Start() {
        StartCoroutine("GenerateBalls");
    }

    IEnumerator GenerateBalls() {
        yield return new WaitForSeconds(2);
        for (;;) {
            var ballToInstantiate = ballTypes[Random.Range(0, ballTypes.Length)];
            Instantiate(ballToInstantiate, this.gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}