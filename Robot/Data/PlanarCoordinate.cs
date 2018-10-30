using System;

namespace Robot.Data{

    /**Represents a 2D coordinate */
    public class PlanarCoordinate{
        public int X{get;set;}
        public int Y{get;set;}

        public PlanarCoordinate(int x, int y){
            this.X=x;
            this.Y=y;
        }

        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var objCast = obj as PlanarCoordinate;
            return (this.X==objCast.X)&&(this.Y==objCast.Y);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return (Y << 16) ^ X;
        }

        public override string ToString(){
            return String.Concat(X,",",Y);
        }
    }
}