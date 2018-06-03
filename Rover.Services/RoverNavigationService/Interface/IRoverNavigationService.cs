using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rover.Model;
namespace Rover.Services.Interface
{
    public interface IRoverNavigationService
    {
        List<Rover.Model.RoverOutputModel> GetRoverCoordinates(string RoverControlJsonModel);
    }
}
