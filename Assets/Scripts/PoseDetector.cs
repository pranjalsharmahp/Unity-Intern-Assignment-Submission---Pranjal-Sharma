using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoseDetector : MonoBehaviour
{
    public Transform leftHip,leftKnee,leftAnkle;
    public Transform rightHip,rightKnee,rightAnkle;
    public Animator animator;
    private string currentPose="Standing";
    public bool isPaused=false;
    public Renderer leftSphereRenderer;
    public Renderer rightSphereRenderer;
    public TextMeshProUGUI statusText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftKneeAngle=GetKneeAngle(leftHip,leftKnee,leftAnkle);
        float rightKneeAngle=GetKneeAngle(rightHip,rightKnee,rightAnkle);
        Debug.Log("Left Knee Angle: " + leftKneeAngle);
        Debug.Log("Right Knee Angle: " + rightKneeAngle);
        if(leftKneeAngle>60 && rightKneeAngle>60){
            Debug.Log("Sqaut");
            UpdatePose("Squatting",Color.red);
            if (!isPaused &&leftKneeAngle > 130 && leftKneeAngle < 135)  
            {
                animator.speed = 0;
                
                isPaused=true;
            }
        }
        if(leftKneeAngle<10 && rightKneeAngle<10){
            Debug.Log("Stand");
            UpdatePose("Standing",Color.green);
        }
        // if((int)leftKneeAngle==132){
        //     animator.speed=0;
        //     currentPose="Sqauting";
        // }
        if(Input.GetKeyDown(KeyCode.S)){
            animator.SetBool("isSquatting",true);
            isPaused=false;
        }
        if(Input.GetKeyDown(KeyCode.W)&& isPaused){
            animator.speed=1;
            
            animator.SetBool("isSquatting",false);
        }

    }
    float GetKneeAngle(Transform Hip,Transform Knee,Transform Ankle){
        Vector3 thigh=Knee.position-Hip.position;
        Vector3 shin=Ankle.position-Knee.position;
        return Vector3.Angle(thigh,shin);
    }
    void UpdatePose(string Pose,Color color){
        currentPose=Pose;
        statusText.text=currentPose;
        leftSphereRenderer.material.color=color;
        rightSphereRenderer.material.color=color;
    }
}
