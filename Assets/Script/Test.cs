using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    LineRenderer circlerenderer;
    [SerializeField] int steps;
    [SerializeField] float radius;
    // Start is called before the first frame update
    void Start()
    {
        circlerenderer = GetComponent<LineRenderer>();
        DrawCircle(steps, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DrawCircle(int steps,  float radius){
        circlerenderer.positionCount = steps+1;
        for(int currentstep = 0; currentstep < steps; currentstep++){
            float progress = (float) currentstep/steps;
            float angle = progress*Mathf.PI*2;
            float x = radius*Mathf.Cos(angle);
            float y = radius*Mathf.Sin(angle);
            circlerenderer.SetPosition(currentstep, new Vector3(x,y,0));
        }
        circlerenderer.SetPosition(steps, new Vector3(1,0,0) * radius);
    }
}
