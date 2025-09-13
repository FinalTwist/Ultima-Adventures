using Server.Mobiles;
using System.Collections.Generic;

namespace Server.AnimateMove
{
    public interface IAnimateMove
    {
        bool IsRunning { get; set; }    //Turns On/Off Animation

        List<PlayerMobile> PM { get; set; }
        Shadow shadow { get; set; }

        int MoveDirection { get; set; }       //Move Direction
        int MoveDistance { get; set; }        //Move Range

        int MoveSpeed { get; set; }      //Frequency of moving
        int MoveCount { get; set; }      //Counter for Speed

        bool MoveCycle { get; set; }     //true is smooth back and forth, false is once at end it repeats from start
        bool MoveForward { get; set; }   //Sets direction of move if cycle true
        int MoveCounter { get; set; }       //Keeps count of movement
    }
}
