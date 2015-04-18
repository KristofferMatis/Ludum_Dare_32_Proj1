using UnityEngine;
using System.Collections;
using System.Linq;


public class InputManager : Singleton<InputManager>
{
    public struct ReturnInfo
    {
        public bool IsPressed;

        public float AxisValue;
    }

    [System.Serializable]
    public class ButtonControls
    {
        #region Enums
        public enum GamePadButtons
        {
            None = 0,
            A_BUTTON,
            B_BUTTON,
            X_BUTTON,
            Y_BUTTON,
            RB_BUTTON,
            RT_BUTTON,
            RT_AXIS,
            LB_BUTTON,
            LT_BUTTON,
            LT_AXIS,
            START_BUTTON,
            PAUSE_BUTTON,
            DPAD_UP,
            DPAD_DOWN,
            DPAD_LEFT,
            DPAD_RIGHT,
            L_TMBSTK_PRESS,
            R_TMBSTK_PRESS,
            L_TMBSTK_LEFT,
            L_TMBSTK_RIGHT,
            L_TMBSTK_UP,
            L_TMBSTK_DOWN,
            R_TMBSTK_LEFT,
            R_TMBSTK_RIGHT,
            R_TMBSTK_UP,
            R_TMBSTK_DOWN
        }

        public enum PlayerAction
        {
            MoveLeft = 0,
            MoveRight,
            MoveForward,
            MoveBackward,
            LookRight,
            LookLeft,
            LookUp,
            LookDown,
            Jump,
            Attack1
        }
        #endregion

        #region Variables
        public PlayerAction ActionName;

        public KeyCode[] KeyMap;

        public GamePadButtons[] ButtonMap;
        #endregion

        #region Helper Funtions
        /// <summary>
        /// helperfuntions that help out with front faceing button checks
        /// </summary>
        /// <returns></returns>
        #region Face Buttons

        bool IsAPressed()
        {
            return Input.GetButton("A");
        }

        bool IsBPressed()
        {
            return Input.GetButton("B");
        }

        bool IsXPressed()
        {
            return Input.GetButton("X");
        }

        bool IsYPressed()
        {
            return Input.GetButton("Y");
        }

        bool IsStartPressed()
        {
            return Input.GetButton("Start");
        }

        bool IsBackPressed()
        {
            return Input.GetButton("Back");
        }

        bool IsLTHMBSTKPressed()
        {
            return Input.GetButton("LAP");
        }

        bool IsRTHMSTKPressed()
        {
            return Input.GetButton("RAP");
        }

        #endregion

        /// <summary>
        /// helper funtions that help out with face axis buttons
        /// </summary>
        /// <returns></returns>
        #region Face Axis

        float IsLTMBSKTRight()
        {
            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                return Input.GetAxis("Horizontal");
            }

            return 0.0f;
        }

        float IsLTMBSKTLeft()
        {
            if (Input.GetAxis("Horizontal") < 0.0f)
            {
                return Input.GetAxis("Horizontal");
            }

            return 0.0f;
        }

        float IsLTMBSKTUp()
        {
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                return Input.GetAxis("Vertical");
            }

            return 0.0f;
        }

        float IsLTMBSKTDown()
        {
            if (Input.GetAxis("Vertical") < 0.0f)
            {
                return Input.GetAxis("Vertical");
            }

            return 0.0f;
        }

        float IsRTMBSKTRight()
        {
            if (Input.GetAxis("RSTKHor") > 0.0f)
            {
                return Input.GetAxis("RSTKHor");
            }

            return 0.0f;
        }

        float IsRTMBSKTLeft()
        {
            if (Input.GetAxis("RSTKHor") < 0.0f)
            {
                return Input.GetAxis("RSTKHor");
            }

            return 0.0f;
        }

        float IsRTMBSKTUp()
        {
            if (Input.GetAxis("RSTKVer") > 0.0f)
            {
                return Input.GetAxis("RSTKVer");
            }

            return 0.0f;
        }

        float IsRTMBSKTDown()
        {
            if (Input.GetAxis("RSTKVer") < 0.0f)
            {
                return Input.GetAxis("RSTKVer");
            }

            return 0.0f;
        }



        #endregion

        /// <summary>
        /// helper funtions that help with back buttons
        /// </summary>
        /// <returns></returns>
        #region Back Buttons

        bool IsRBPressed()
        {
            return Input.GetButton("RB");
        }

        bool IsLBPressed()
        {
            return Input.GetButton("LB");
        }

        #endregion


        /// <summary>
        /// helper functions that help with back axis buttons
        /// </summary>
        /// <returns></returns>
        #region Back Axis

        float IsRTPressed()
        {
            return Input.GetAxis("RT");
        }

        float IsLTPressed()
        {
            return Input.GetAxis("LT");
        }

        #endregion

        public ReturnInfo HandleButtons(GamePadButtons AButton = 0)
        {
            ReturnInfo returnInfo = new ReturnInfo();

            if (AButton != 0)
            {
                switch (AButton)
                {
                    #region Front Face Buttons
                    case GamePadButtons.A_BUTTON:
                        {
                            returnInfo.IsPressed = IsAPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.B_BUTTON:
                        {
                            returnInfo.IsPressed = IsBPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.X_BUTTON:
                        {
                            returnInfo.IsPressed = IsXPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.Y_BUTTON:
                        {
                            returnInfo.IsPressed = IsYPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.START_BUTTON:
                        {
                            returnInfo.IsPressed = IsStartPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.PAUSE_BUTTON:
                        {
                            returnInfo.IsPressed = IsBackPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.R_TMBSTK_PRESS:
                        {
                            returnInfo.IsPressed = IsRTHMSTKPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.L_TMBSTK_PRESS:
                        {
                            returnInfo.IsPressed = IsLTHMBSTKPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;
                    #endregion

                    #region Back Face Buttons
                    case GamePadButtons.RB_BUTTON:
                        {
                            returnInfo.IsPressed = IsRBPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;

                    case GamePadButtons.LB_BUTTON:
                        {
                            returnInfo.IsPressed = IsLBPressed();
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;
                    #endregion

                    #region Front Face Axis
                    case GamePadButtons.L_TMBSTK_RIGHT:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsLTMBSKTRight();
                        }
                        break;

                    case GamePadButtons.L_TMBSTK_LEFT:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsLTMBSKTLeft();
                        }
                        break;

                    case GamePadButtons.L_TMBSTK_UP:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsLTMBSKTUp();
                        }
                        break;

                    case GamePadButtons.L_TMBSTK_DOWN:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsLTMBSKTDown();
                        }
                        break;

                    case GamePadButtons.R_TMBSTK_RIGHT:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsRTMBSKTRight();
                        }
                        break;

                    case GamePadButtons.R_TMBSTK_LEFT:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsRTMBSKTLeft();
                        }
                        break;

                    case GamePadButtons.R_TMBSTK_UP:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsRTMBSKTUp();
                        }
                        break;

                    case GamePadButtons.R_TMBSTK_DOWN:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsRTMBSKTDown();
                        }
                        break;
                    #endregion

                    #region Back Face Axis

                    case GamePadButtons.RT_AXIS:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsRTPressed();
                        }
                        break;

                    case GamePadButtons.LT_AXIS:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = IsLTPressed();
                        }
                        break;
                    #endregion
                    default:
                        {
                            returnInfo.IsPressed = false;
                            returnInfo.AxisValue = 0.0f;
                        }
                        break;
                }
            }
            return returnInfo;
        }

        #endregion

    }

    public ButtonControls[] Buttons;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PlayerMoveRight()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.MoveRight)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerMoveForward()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.MoveForward)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerMoveBackward()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.MoveBackward)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerMoveLeft()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks if the player action is not move left
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.MoveLeft)
            {
                continue;
            }

            //goes through the buttons and returns the axis keyboard and contorl are not fully set up
            //TODO.....
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        return 0.0f;
    }

    public float PlayerLookRight()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.LookRight)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerLookLeft()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.LookLeft)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerLookUp()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.LookUp)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public float PlayerLookDown()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Move Right
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.LookDown)
            {
                continue;
            }

            //goes through all the button maps and returns the axis not set up to work seperately from keyboard and mouse
            //TODO.......
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                return Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).AxisValue;
            }
        }

        //if it gets here that means nothing is pressed
        return 0.0f;
    }

    public bool PlayerJump()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not jump
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.Jump)
            {
                continue;
            }

            //creating containers for the button and key presses(for multi press)
            bool[] MultiButtonCheck = new bool[Buttons[i].ButtonMap.Length];
            bool[] MultiKeyCheck = new bool[Buttons[i].KeyMap.Length];

            //goes through the buttons sets the value to the the containers we created
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                MultiButtonCheck[j] = Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).IsPressed;
            }

            //goes through the keys and sets the value to the container we created
            for (int k = 0; k < Buttons[i].KeyMap.Length; k++)
            {
                MultiKeyCheck[k] = Input.GetKey(Buttons[i].KeyMap[k]);
            }

            //checks if the button container contains all trues if so it returns true
            if (!MultiButtonCheck.Contains(false))
            {
                return true;
            }

            //checks if the key container contains all trues if so it returns true
            if (!MultiKeyCheck.Contains(false))
            {
                return true;
            }
        }

        //if it gets here that means nothing was pressed
        return false;
    }

    public bool PlayerAttack1()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            //checks to see if the player action is not Attack1
            if (Buttons[i].ActionName != ButtonControls.PlayerAction.Attack1)
            {
                continue;
            }

            //creating containers for the button and key presses(for multi press)
            bool[] MultiButtonCheck = new bool[Buttons[i].ButtonMap.Length];
            bool[] MultiKeyCheck = new bool[Buttons[i].KeyMap.Length];

            //goes through the buttons sets the value to the the containers we created
            for (int j = 0; j < Buttons[i].ButtonMap.Length; j++)
            {
                MultiButtonCheck[j] = Buttons[i].HandleButtons(Buttons[i].ButtonMap[j]).IsPressed;
            }

            //goes through the keys sets the value to the the containers we created
            for (int k = 0; k < Buttons[i].KeyMap.Length; k++)
            {
                MultiKeyCheck[k] = Input.GetKey(Buttons[i].KeyMap[k]);
            }

            //checks if the button container contains all trues if so it returns true
            if (!MultiButtonCheck.Contains(false))
            {
                return true;
            }

            //checks if the key container contains all trues if so it returns true
            if (!MultiKeyCheck.Contains(false))
            {
                return true;
            }
        }

        //if it gets here that means nothing was pressed
        return false;
    }
}