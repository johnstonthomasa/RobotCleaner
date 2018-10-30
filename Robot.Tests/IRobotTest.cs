using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Robot;

namespace Tests
{
    
    [TestFixture(Description="Tests fundamental requirements for current implementations of IRobot")]
    public class IRobotTest
    {
        /**alias for shorthand */
        private static string nl = Environment.NewLine;

        /**Returns a list of instances of all instantiable classes that implement IRobot  */
        private static IEnumerable<IRobot> GetInstances()
        {
            var type = typeof(IRobot);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && 
                       !(p.IsInterface && p.IsAbstract))
                .Select(t => (IRobot)Activator.CreateInstance(t));
        }

        #region Tests from requirements documents+
        [Test(Description="When there are no move instructions 0 spaces should be cleaned"), Sequential]
        public void NoMoveInstructions([ValueSource("GetInstances")]IRobot robotImpl){

            var commands = "0"+nl+"0 0";
            robotImpl.IngestCommands(commands);
            Assert.AreEqual(robotImpl.RunCommands(), 0);
        }

        [Test(Description="Zero move instructions should clean current space."), Sequential]
        public void ZeroMoveInstruction([ValueSource("GetInstances")]IRobot robotImpl){

            var commands = "4"+nl+"0 0"+nl+"E 0"+nl+"W 0"+nl+"S 0"+nl+"N 0";
            robotImpl.IngestCommands(commands);
            Assert.AreEqual(robotImpl.RunCommands(), 1);
        }

        [Test(Description="Baseline test.  Tests a single command.  Spaces cleaned should be distance+1")]
        public void SingleMoveInstruction([ValueSource("GetInstances")]IRobot robotImpl){

            var distance=23;
            var commands="1"+nl+"0 0"+nl+"N "+distance;
            robotImpl.IngestCommands(commands);
            Assert.AreEqual((distance+1), robotImpl.RunCommands());
        }

        [Test(Description="25 X 25 grid full coverage")]
        public void SmallTest([ValueSource("GetInstances")]IRobot robotImpl){

            var length = 25;
            var commands= RunASquareCommand(length);

            robotImpl.IngestCommands(commands);
            Assert.AreEqual(length*length, robotImpl.RunCommands());
        }

        [Test(Description="10000 X 10000 grid full coverage")]
        public void LargeTest([ValueSource("GetInstances")]IRobot robotImpl){

            var length = 10000;
            var commands= RunASquareCommand(length);

            robotImpl.IngestCommands(commands);
            Assert.AreEqual(length*length, robotImpl.RunCommands());
        }

        [Test(Description="When 2 moves cover the same spaces, they should only be counted once")]
        public void RedundantMoveInstruction([ValueSource("GetInstances")]IRobot robotImpl){
            
            var distance=30572;
            var commands="2"+nl+"0 0"+nl+"N "+distance+nl+"S "+distance;
            robotImpl.IngestCommands(commands);
            Assert.AreEqual((distance+1), robotImpl.RunCommands());
        }
        #endregion



        #region Recommended tests that have been omitted because the requirements explicitly say not to implement the functionality or they are undefined.

        [Test(Description="If the number of commands declared is not parseable, throw an exception.")]
        public void InvalidNumberOfCommands([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

        [Test(Description="If the starting coordinates given in the first line are not parseable or are out of bounds, throw an exception.")]
        public void InvalidStartingCoordinates([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

        [Test(Description="If an unparseable command is encountered on ingest, throw an exception.")]
        public void InvalidCommand([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

        [Test(Description="If it's possible to go out of bounds this should be tested for")]
        public void OutOfBounds([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

        [Test(Description="If it's possible to have a collison with a boundary, this should be defined and tested for")]
        public void Collision([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

        [Test(Description="If the number of commands given is less than the number declared, this should be handled somehow")]
        public void NumberOfGivenCommandsIsLessThanDeclaration([ValueSource("GetInstances")]IRobot robotImpl){
            Assert.Warn("Recommended test additions");
        }

                [Test(Description="Per the requirements doc.  The robot should terminate execution based on the declared number of steps")]
        public void NumberOfGivenCommandsExceedsDeclaration([ValueSource("GetInstances")]IRobot robotImpl){
           Assert.Warn("Recommended test additions");
        }
        
        #endregion

        private static string RunASquareCommand(int sidelength){
            var numberOfCommands = (sidelength*2)-1;
            var rowDistance=sidelength-1;
            var commands= new StringBuilder();
            commands.Append(numberOfCommands+nl+"0 0"+nl);
            
            for(int i=0;i<sidelength;i++){
                if(i%2==0){
                    commands.Append("W "+rowDistance+nl);
                }
                else{
                    commands.Append("E "+rowDistance+nl);
                }                
                if(i<sidelength-1){
                    commands.Append("S 1"+nl);
                }
            }
            return commands.ToString();
        }
    }
}