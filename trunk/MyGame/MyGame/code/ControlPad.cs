using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public class ControlPad
    {
        #region fields & constants
        public PlayerIndex index;
        GamePadState currentState;
        GamePadState lastState;
        public Vector2 RS, LS;
        public float menuDelay = 0;

        public float timeToRumble;
        public bool rumbling = false;

        public const float THRESHOLD = 0.5f;
        public const float MENU_DELAY = 0.1f;

#if DEBUG
        public KeyboardState kbCurrentState;
        public KeyboardState kbLastState;
#endif
        #endregion

        public void update(PlayerIndex playerIndex)
        {
            lastState = currentState;
            currentState = GamePad.GetState(playerIndex);

#if DEBUG
            kbLastState = kbCurrentState;
            kbCurrentState = Keyboard.GetState();
#endif

            RS = currentState.ThumbSticks.Right;
            LS = currentState.ThumbSticks.Left;

#if DEBUG
            if (LS == Vector2.Zero)
            {
                if (kbCurrentState.IsKeyDown(Keys.Down))
                    LS.Y = -1;
                if (kbCurrentState.IsKeyDown(Keys.Up))
                    LS.Y = 1;
                if (kbCurrentState.IsKeyDown(Keys.Left))
                    LS.X = -1;
                if (kbCurrentState.IsKeyDown(Keys.Right))
                    LS.X = 1;
                if (LS != Vector2.Zero)
                {
                    LS.Normalize();
                }
            }
#endif

            menuDelay += SB.dt;

            #region Rumble
            timeToRumble -= SB.dt;
            if (rumbling && timeToRumble < 0)
                stopRumbling();
            #endregion
        }

        #region Sticks
        public bool LSfirstLeft()
        {
            return (currentState.ThumbSticks.Left.X < -THRESHOLD && lastState.ThumbSticks.Left.X > -THRESHOLD);
        }
        public bool LSfirstRight()
        {
            return (currentState.ThumbSticks.Left.X > THRESHOLD && lastState.ThumbSticks.Left.X < THRESHOLD);
        }
        public bool LSfirstUp()
        {
            return (currentState.ThumbSticks.Left.Y > THRESHOLD && lastState.ThumbSticks.Left.Y < THRESHOLD);
        }
        public bool LSfirstDown()
        {
            return (currentState.ThumbSticks.Left.Y < -THRESHOLD && lastState.ThumbSticks.Left.Y > -THRESHOLD);
        }
        public bool RSfirstLeft()
        {
            return (currentState.ThumbSticks.Right.X < -THRESHOLD && lastState.ThumbSticks.Right.X > -THRESHOLD);
        }
        public bool RSfirstRight()
        {
            return (currentState.ThumbSticks.Right.X > THRESHOLD && lastState.ThumbSticks.Right.X < THRESHOLD);
        }
        public bool RSfirstUp()
        {
            return (currentState.ThumbSticks.Right.Y > THRESHOLD && lastState.ThumbSticks.Right.Y < THRESHOLD);
        }
        public bool RSfirstDown()
        {
            return (currentState.ThumbSticks.Right.Y < -THRESHOLD && lastState.ThumbSticks.Right.Y > -THRESHOLD);
        }
        public bool LSleft()
        {
            return (currentState.ThumbSticks.Left.X < -THRESHOLD);
        }
        public bool LSright()
        {
            return (currentState.ThumbSticks.Left.X > THRESHOLD);
        }
        public bool LSup()
        {
            return (currentState.ThumbSticks.Left.Y > THRESHOLD);
        }
        public bool LSdown()
        {
            return (currentState.ThumbSticks.Left.Y < -THRESHOLD);
        }
        public bool RSleft()
        {
            return (currentState.ThumbSticks.Right.X < -THRESHOLD);
        }
        public bool RSright()
        {
            return (currentState.ThumbSticks.Right.X > THRESHOLD);
        }
        public bool RSup()
        {
            return (currentState.ThumbSticks.Right.Y > THRESHOLD);
        }
        public bool RSdown()
        {
            return (currentState.ThumbSticks.Right.Y < -THRESHOLD);
        }
        public bool RSClick_pressed()
        {
            return (currentState.Buttons.RightStick == ButtonState.Pressed);
        }
        public bool RSClick_firstPressed()
        {
            return (currentState.Buttons.RightStick == ButtonState.Pressed && lastState.Buttons.RightStick == ButtonState.Released);
        }
        public bool RSClick_firstReleased()
        {
            return (currentState.Buttons.RightStick == ButtonState.Released && lastState.Buttons.RightStick == ButtonState.Pressed);
        }
        public bool RSClick_released()
        {
            return (currentState.Buttons.RightStick == ButtonState.Released);
        }
        public bool LSClick_pressed()
        {
            return (currentState.Buttons.LeftStick == ButtonState.Pressed);
        }
        public bool LSClick_firstPressed()
        {
            return (currentState.Buttons.LeftStick == ButtonState.Pressed && lastState.Buttons.LeftStick == ButtonState.Released);
        }
        public bool LSClick_firstReleased()
        {
            return (currentState.Buttons.LeftStick == ButtonState.Released && lastState.Buttons.LeftStick == ButtonState.Pressed);
        }
        public bool LSClick_released()
        {
            return (currentState.Buttons.LeftStick == ButtonState.Released);
        }
        #endregion
        #region Colored Buttons
        public bool A_pressed()
        {
            return (currentState.Buttons.A == ButtonState.Pressed)
#if DEBUG
 || kbCurrentState.IsKeyDown(Keys.Z)
#endif
                ;
        }
        public bool A_firstPressed()
        {
            return (currentState.Buttons.A == ButtonState.Pressed && lastState.Buttons.A == ButtonState.Released)
#if DEBUG
 || (kbCurrentState.IsKeyDown(Keys.Z) && kbLastState.IsKeyUp(Keys.Z) )
#endif
                ;
        }
        public bool A_firstReleased()
        {
            return (currentState.Buttons.A == ButtonState.Released && lastState.Buttons.A == ButtonState.Pressed);
        }
        public bool A_released()
        {
            return (currentState.Buttons.A == ButtonState.Released);
        }
        public bool X_pressed()
        {
            return (currentState.Buttons.X == ButtonState.Pressed)
#if DEBUG
 || kbCurrentState.IsKeyDown(Keys.A)
#endif
            ;
        }
        public bool X_firstPressed()
        {
            return (currentState.Buttons.X == ButtonState.Pressed && lastState.Buttons.X == ButtonState.Released)
#if DEBUG
 || (kbCurrentState.IsKeyDown(Keys.A) && kbLastState.IsKeyUp(Keys.A))
#endif
                ;
        }
        public bool X_firstReleased()
        {
            return (currentState.Buttons.X == ButtonState.Released && lastState.Buttons.X == ButtonState.Pressed);
        }
        public bool X_released()
        {
            return (currentState.Buttons.X == ButtonState.Released);
        }
        public bool Y_pressed()
        {
            return (currentState.Buttons.Y == ButtonState.Pressed)
#if DEBUG
 || kbCurrentState.IsKeyDown(Keys.S)
#endif
                ;
        }
        public bool Y_firstPressed()
        {
            return (currentState.Buttons.Y == ButtonState.Pressed && lastState.Buttons.Y == ButtonState.Released)
#if DEBUG
 || (kbCurrentState.IsKeyDown(Keys.S) && kbLastState.IsKeyUp(Keys.S))
#endif
                ;
        }
        public bool Y_firstReleased()
        {
            return (currentState.Buttons.Y == ButtonState.Released && lastState.Buttons.Y == ButtonState.Pressed);
        }
        public bool Y_released()
        {
            return (currentState.Buttons.Y == ButtonState.Released);
        }
        public bool B_pressed()
        {
            return (currentState.Buttons.B == ButtonState.Pressed)
#if DEBUG
 || kbCurrentState.IsKeyDown(Keys.X)
#endif
                ;
        }
        public bool B_firstPressed()
        {
            return (currentState.Buttons.B == ButtonState.Pressed && lastState.Buttons.B == ButtonState.Released)
#if DEBUG
 || (kbCurrentState.IsKeyDown(Keys.X) && kbLastState.IsKeyUp(Keys.X))
#endif
                ;
        }
        public bool B_firstReleased()
        {
            return (currentState.Buttons.B == ButtonState.Released && lastState.Buttons.B == ButtonState.Pressed);
        }
        public bool B_released()
        {
            return (currentState.Buttons.B == ButtonState.Released);
        }
        #endregion
        #region Start & Back & Guide
        public bool Start_pressed()
        {
            return (currentState.Buttons.Start == ButtonState.Pressed);
        }
        public bool Start_firstPressed()
        {
            return (currentState.Buttons.Start == ButtonState.Pressed && lastState.Buttons.Start == ButtonState.Released);
        }
        public bool Start_firstReleased()
        {
            return (currentState.Buttons.Start == ButtonState.Released && lastState.Buttons.Start == ButtonState.Pressed);
        }
        public bool Start_released()
        {
            return (currentState.Buttons.Start == ButtonState.Released);
        }
        public bool Back_pressed()
        {
            return (currentState.Buttons.Back == ButtonState.Pressed);
        }
        public bool Back_firstPressed()
        {
            return (currentState.Buttons.Back == ButtonState.Pressed && lastState.Buttons.Back == ButtonState.Released);
        }
        public bool Back_firstReleased()
        {
            return (currentState.Buttons.Back == ButtonState.Released && lastState.Buttons.Back == ButtonState.Pressed);
        }
        public bool Back_released()
        {
            return (currentState.Buttons.Back == ButtonState.Released);
        }
        public bool Guide_firstPressed()
        {
            return (currentState.Buttons.BigButton == ButtonState.Pressed);
        }
        #endregion
        #region DPAD
        public bool DLeft_firstPressed()
        {
            return (currentState.DPad.Left == ButtonState.Pressed && lastState.DPad.Left == ButtonState.Released);
        }
        public bool DRight_firstPressed()
        {
            return (currentState.DPad.Right == ButtonState.Pressed && lastState.DPad.Right == ButtonState.Released);
        }
        public bool DUp_firstPressed()
        {
            return (currentState.DPad.Up == ButtonState.Pressed && lastState.DPad.Up == ButtonState.Released);
        }
        public bool DDown_firstPressed()
        {
            return (currentState.DPad.Down == ButtonState.Pressed && lastState.DPad.Down == ButtonState.Released);
        }
        public bool DLeft_pressed()
        {
            return (currentState.DPad.Left == ButtonState.Pressed);
        }
        public bool DRight_pressed()
        {
            return (currentState.DPad.Right == ButtonState.Pressed);
        }
        public bool DUp_pressed()
        {
            return (currentState.DPad.Up == ButtonState.Pressed);
        }
        public bool DDown_pressed()
        {
            return (currentState.DPad.Down == ButtonState.Pressed);
        }
        #endregion
        #region Directions
        public bool Left_firstPressed()
        {
            if (menuDelay > MENU_DELAY)
            {
                if (LSfirstLeft() || DLeft_firstPressed())
                {
                    menuDelay = 0;
                    return true;
                }
            }
            return false;
        }
        public bool Right_firstPressed()
        {
            if (menuDelay > MENU_DELAY)
            {
                if (LSfirstRight() || DRight_firstPressed())
                {
                    menuDelay = 0;
                    return true;
                }
            }
            return false;
        }
        public bool Down_firstPressed()
        {
            if (menuDelay > MENU_DELAY)
            {
                if (LSfirstDown() || DDown_firstPressed())
                {
                    menuDelay = 0;
                    return true;
                }
            }
            return false;
        }
        public bool Up_firstPressed()
        {
            if (menuDelay > MENU_DELAY)
            {
                if (LSfirstUp() || DUp_firstPressed())
                {
                    menuDelay = 0;
                    return true;
                }
            }
            return false;
        }
        public bool Left_pressed()
        {
            return (LSleft() || DLeft_pressed());
        }
        public bool Right_pressed()
        {
            return (RSleft() || DRight_pressed());
        }
        public bool Down_pressed()
        {
            return (LSdown() || DDown_pressed());
        }
        public bool Up_pressed()
        {
            return (LSup() || DUp_pressed());
        }
        #endregion
        #region Bumpers
        public bool RB_pressed()
        {
            return (currentState.Buttons.RightShoulder == ButtonState.Pressed);
        }
        public bool RB_firstPressed()
        {
            return (currentState.Buttons.RightShoulder == ButtonState.Pressed && lastState.Buttons.RightShoulder == ButtonState.Released);
        }
        public bool RB_firstReleased()
        {
            return (currentState.Buttons.RightShoulder == ButtonState.Released && lastState.Buttons.RightShoulder == ButtonState.Pressed);
        }
        public bool RB_released()
        {
            return (currentState.Buttons.RightShoulder == ButtonState.Released);
        }
        public bool LB_pressed()
        {
            return (currentState.Buttons.LeftShoulder == ButtonState.Pressed);
        }
        public bool LB_firstPressed()
        {
            return (currentState.Buttons.LeftShoulder == ButtonState.Pressed && lastState.Buttons.LeftShoulder == ButtonState.Released);
        }
        public bool LB_firstReleased()
        {
            return (currentState.Buttons.LeftShoulder == ButtonState.Released && lastState.Buttons.LeftShoulder == ButtonState.Pressed);
        }
        public bool LB_released()
        {
            return (currentState.Buttons.LeftShoulder == ButtonState.Released);
        }
        #endregion
        #region Triggers
        public bool LT_pressed()
        {
            return (currentState.Triggers.Left > THRESHOLD);
        }
        public bool LT_firstPressed()
        {
            return (currentState.Triggers.Left > THRESHOLD && lastState.Triggers.Left < THRESHOLD);
        }
        public bool LT_firstReleased()
        {
            return (currentState.Triggers.Left < THRESHOLD && lastState.Triggers.Left > THRESHOLD);
        }
        public bool RT_pressed()
        {
            return (currentState.Triggers.Right > THRESHOLD);
        }
        public bool RT_firstPressed()
        {
            return (currentState.Triggers.Right > THRESHOLD && lastState.Triggers.Right < THRESHOLD);
        }
        public bool RT_firstReleased()
        {
            return (currentState.Triggers.Right < THRESHOLD && lastState.Triggers.Right > THRESHOLD);
        }
        #endregion

        public bool disconnected()
        {
            return (lastState.IsConnected && !currentState.IsConnected);
        }
        #region Rumble functions
        public void rumble(int time, float left, float right)
        {
            rumbling = true;
            try { GamePad.SetVibration(index, left, right); }
            catch (Exception ) { }
            timeToRumble = time;
        }
        public void rumble(float left, float right)
        {
            rumbling = true;
            try { GamePad.SetVibration(index, left, right); }
            catch (Exception ) { }
            timeToRumble = 99999999;
        }
        public void stopRumbling()
        {
            rumbling = false;
            try { GamePad.SetVibration(index, 0, 0); }
            catch (Exception ) { }
        }
        #endregion
    }
}