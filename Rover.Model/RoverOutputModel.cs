/// <summary>
/// Model has the final coordinates and of the Rover after the exploration
/// instructions are applied.
/// </summary>
namespace Rover.Model
{
    public class RoverOutputModel
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string Heading { get; set; }
    }
}
