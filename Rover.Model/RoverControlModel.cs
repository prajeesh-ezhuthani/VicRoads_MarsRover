using System.Collections.Generic;

namespace Rover.Model
{
    public class RoverControlModel
    {
        /// <summary>
        /// This model is holds the complete input instructions 
        /// RoverInput has the list of Rover instructions.Battle field cooridnates are stored in GridX,GridY. 
        /// </summary>
        public RoverControlModel()
        {
            RoverInputs = new List<RoverInputModel>();
        }
        public int GridTopX { get; set; }
        public int GridTopY { get; set; }
        public List<RoverInputModel> RoverInputs { get; set; }
        
    }
}
