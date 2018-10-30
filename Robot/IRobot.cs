using System;

namespace Robot
{
    public interface IRobot
    {
        /**Parses and loads commands 
           Commands are expected to be formatted as a linebreak separated string as follows:
           int: numberOfExpectedCommands (will be in the range (0 <= n <= 10,000))
           int int: startingCoordinates (both will be in the range (-100,000 ,+ n <= 100,000))
           char int: direction N,S,E or W and distance (in range (0 < n < 100,000)) (there will be potentially be many of these)
           ...
           */
        void IngestCommands(string commands);

        /**Executes the loaded commands and returns the number of unique spaces "cleaned" by the robot
           The robot cleans all spaces it touches, not just where it stops*/
        int RunCommands();
    }
}
