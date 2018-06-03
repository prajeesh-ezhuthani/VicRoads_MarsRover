using System.Collections.Generic;
/// <summary>
/// View model used for displaying the output to UI
/// </summary>
namespace Rover.Model
{
    public class RoverOutputViewModel
    {
        public RoverOutputViewModel()
        {
            RoverOutputs = new List<RoverOutputModel>();
        }

        public List<RoverOutputModel> RoverOutputs { get; set; }
        public string ErrorMessage { get; set; }
    }
}
