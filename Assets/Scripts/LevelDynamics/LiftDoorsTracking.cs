﻿using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour {

    public float doorSpeed = 7f;

    private Transform leftOuterDoor;
    private Transform rightOuterDoor;
    private Transform leftInnerDoor;
    private Transform rightInnerDoor;
    private float leftClosedPosX;
    private float rightClosePosX;

    void Awake()
    {
        leftOuterDoor = GameObject.Find("door_exit_Outer_left_001").transform;
        rightOuterDoor = GameObject.Find("door_exit_Outer_right_001").transform;
        leftInnerDoor = GameObject.Find("door_exit_Inner_left_001").transform;
        rightInnerDoor = GameObject.Find("door_exit_Inner_right_001").transform;

        //设置当前门的坐标为关闭的x坐标
        leftClosedPosX = leftInnerDoor.position.x;
        rightClosePosX = rightInnerDoor.position.x;
    }

    void MoveDoors(float newLeftXTarget,float newRightXTarget)
    {
        float newX = Mathf.Lerp(leftInnerDoor.position.x, newLeftXTarget, doorSpeed * Time.deltaTime);

        leftInnerDoor.position = new Vector3(newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

        newX = Mathf.Lerp(rightInnerDoor.position.x, newRightXTarget, doorSpeed * Time.deltaTime);

        rightInnerDoor.position = new Vector3(newX, rightInnerDoor.position.y, rightInnerDoor.position.z);

    }
    public void DoorFollowing()
    {
        MoveDoors(leftOuterDoor.position.x, rightOuterDoor.position.x);
    }

    public void CloseDoors()
    {
        MoveDoors(leftClosedPosX, rightClosePosX);
    }
}
