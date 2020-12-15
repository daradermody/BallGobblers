using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Color _backgroundColor;
    private Camera _camera;

    private void Start() {
        _camera = GetComponent<Camera>();
        _backgroundColor = GetComponent<Camera>().backgroundColor;
    }

    public void ColourShiftBackground(Color color) {
        StartCoroutine("ColourShiftBackgroundCoroutine", color);
    }

    IEnumerator ColourShiftBackgroundCoroutine(Color startColor) {
        for (float t = 0f; t <= 1.0; t += 0.1f) {
            _camera.backgroundColor = Color.Lerp(startColor, _backgroundColor, t);
            yield return new WaitForSeconds(0.05f);
        }

        _camera.backgroundColor = _backgroundColor;
    }
}
