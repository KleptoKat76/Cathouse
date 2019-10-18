using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{
    private string horizontalAxis;
    private string verticalAxis;
    private string jumpAxis;
    private string shootAxis;
    private string reflectAxis;
    private string gunAimXAxis;
    private string gunAimYAxis;

    public string HorizontalAxis { get => horizontalAxis; set => horizontalAxis = value; }
    public string VerticalAxis { get => verticalAxis; set => verticalAxis = value; }
    public string JumpAxis { get => jumpAxis; set => jumpAxis = value; }
    public string ReflectAxis { get => reflectAxis; set => reflectAxis = value; }
    public string GunAimXAxis { get => gunAimXAxis; set => gunAimXAxis = value; }
    public string GunAimYAxis { get => gunAimYAxis; set => gunAimYAxis = value; }
    public string ShootAxis { get => shootAxis; set => shootAxis = value; }

    public ControlScheme(PlayerController.Controller controller){
        string controllerType = "";
        if(controller != PlayerController.Controller.keyboard)
        {
            controllerType = "joy_" + controller.ToString().Substring(controller.ToString().Length - 1);
            GunAimXAxis = controllerType + "_axis_3";
            GunAimYAxis = controllerType + "_axis_4";
        }
        else
        {
            controllerType = "keyboard";
            GunAimXAxis = "mouseXMovement";
            GunAimYAxis = "mouseYMovement";
        }
        HorizontalAxis = controllerType + "_axis_0";
        VerticalAxis = controllerType + "_axis_1";
        JumpAxis = controllerType + "_axis_5";
        shootAxis = controllerType + "_axis_6";
        print(HorizontalAxis);
    }
}
