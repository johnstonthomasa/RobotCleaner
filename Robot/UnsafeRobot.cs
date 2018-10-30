using System;
using System.Collections.Generic;
using Robot.Data;

namespace Robot
{
    /**This implmentation is based on the requirements which say not to do validation or explicit runtime exceptions 
       This implementation is not optimized for commandsets that cover large areas*/
    public class UnsafeRobot : IRobot
    {
        private PlanarCoordinate currentPosition;

        private List<(CardinalDirection, int)> commandSet;

        private HashSet<PlanarCoordinate> spacesCleaned;

        public UnsafeRobot(){}
        
        public void IngestCommands(string commands)
        {
            commandSet = new List<(CardinalDirection, int)>();
            spacesCleaned = new HashSet<PlanarCoordinate>();

            var splitCommands = commands.Split(new String[]{Environment.NewLine}, 
                                               StringSplitOptions.RemoveEmptyEntries);

            var sp = splitCommands[1].Split(' ');
            currentPosition = new PlanarCoordinate(Int32.Parse(sp[0]), Int32.Parse(sp[1]));

            for(int i =2; i<splitCommands.Length;i++){
                var splitCommandEntry = splitCommands[i].Split(' ');
                var direction = (CardinalDirection)(Enum.Parse(typeof(CardinalDirection), 
                                                               splitCommandEntry[0], 
                                                               false));
                var distance = Int32.Parse(splitCommandEntry[1]);
                commandSet.Add((direction, distance));
            }
        }

        public int RunCommands()
        {
            foreach (var command in commandSet)
            {
                
                markSpacesCleaned(currentPosition, 
                                                   command.Item1, 
                                                   command.Item2);
            }
            return spacesCleaned.Count;
        }

        /**Returns the coordinates to be traversed from a starting coordinate and a move command*/
        private void markSpacesCleaned(PlanarCoordinate startingPoint, 
                                                               CardinalDirection direction, 
                                                               int distance){
            
            var numberOfSpaces=distance+1;

            Func<int, PlanarCoordinate> generateCoordinate=null;
            
            switch (direction){
                case CardinalDirection.E:
                    generateCoordinate = (n)=>{return new PlanarCoordinate(startingPoint.X+n, startingPoint.Y);};
                    break;
                case CardinalDirection.W:
                    generateCoordinate = (n)=>{return new PlanarCoordinate(startingPoint.X-n, startingPoint.Y);};
                    break;
                case CardinalDirection.N:
                    generateCoordinate = (n)=>{return new PlanarCoordinate(startingPoint.X, startingPoint.Y+n);};
                    break;
                case CardinalDirection.S:
                    generateCoordinate = (n)=>{return new PlanarCoordinate(startingPoint.X, startingPoint.Y-n);};
                    break;
            }
       
            for(int i=0; i <numberOfSpaces; i++){
                var coordinate=generateCoordinate(i);
                currentPosition=coordinate;    
                spacesCleaned.Add(coordinate);
            }
        }
    }
}
