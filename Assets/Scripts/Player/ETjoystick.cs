using UnityEngine;
using System.Collections;

public class ETjoystick : MonoBehaviour {

    public float joyPositionX;
    public float joyPositionY;

   void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnjoyStickMove;
        EasyJoystick.On_JoystickMoveEnd += OnjoystickMoveEnd;

    }

    void OnjoystickMoveEnd(MovingJoystick move)
   {
       joyPositionX = joyPositionY = 0f;
       print("end");
   }

    void OnjoyStickMove(MovingJoystick move)
    {
        print("move");
        if (move.joystickName != "MoveJoystick")
            return;
        joyPositionX = move.joystickAxis.x;
        joyPositionY = move.joystickAxis.y;

        print(joyPositionX.ToString());
        print(joyPositionY.ToString());
        
    }
}
