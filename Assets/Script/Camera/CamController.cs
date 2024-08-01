using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Transform playerposition;
    public float SmoothSpeed;
    private Vector3 targetpos, newpos;
    public Vector3 MinPos, MaxPos; 
    // Start is called before the first frame update
    void Start()
    {
        playerposition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != playerposition.position){
            Vector3 _camboundarypos = new Vector3(
                Mathf.Clamp(targetpos.x , MinPos.x, MaxPos.x),
                Mathf.Clamp(targetpos.y, MinPos.y, MaxPos.y),
                Mathf.Clamp(targetpos.z, MinPos.z, MaxPos.z)
            );
            newpos = Vector3.Lerp(transform.position, _camboundarypos, SmoothSpeed);
            transform.position = newpos;
        }
    }
    public void IncreaseCameraSize(){
        float time = 0f;
        while(time <= 1f){
            Camera.main.orthographicSize = Mathf.Lerp(6.5f, 10f, time/1f);
            time += Time.deltaTime;
            //yield return new WaitForSeconds(0.5f);
        }
        //Camera.main.orthographicSize += 0.1f;
    }
}
