namespace Rover.Model
{
    /// <summary>
    /// Model holds input information pertaining to the Rover that has been
    /// deployed.This includes the position and series of instructions.
    /// </summary>
    public class RoverInputModel
    {
        public RoverInputModel()
        {
            Position = new RoverOutputModel();
        }

       public RoverOutputModel Position { get; set; }
       public string Instructions { get; set; }

    }
}
