using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale;
    Vector3 _prevMousePosition = Vector3.zero;

    private Vector2 smoothPos;
    private float smoothScale;

    private void UpdateShader() {
        smoothPos = Vector2.Lerp(smoothPos, pos, .03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, .03f);

        float aspect = (float) Screen.width / (float) Screen.height;

        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if(aspect > 1) {
            scaleY /= aspect;
        } else {
            scaleX *= aspect;
        }


        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));

        // if(Input.GetMouseButton(0)) {
        //     mat.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY));
        // } else {
        //     mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        // }
    }

    private void HandleInputs() {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f) {
            scale *= .7f;
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f) {
            scale *= 1.3f;
        }

        Vector3 mousePosition = Input.mousePosition;
        if( Input.GetMouseButton( 0 ) )
        {
            pos.x -= (mousePosition.x-_prevMousePosition.x)/(Screen.width/scale);
            pos.y -= (mousePosition.y-_prevMousePosition.y)/(Screen.height/scale);
        }
        _prevMousePosition = mousePosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }
}
