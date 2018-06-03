using Rover.Model;
using Rover.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Rover.Services
{
    public class RoverNavigationService: IRoverNavigationService
    {
        private readonly ISettingsService settingsService;
        private int GridTopX = 0;
        private int GridTopY = 0;
        public RoverNavigationService(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
        }
       /// <summary>
       /// Calls the Api endpoint to get the final coordinates
       /// </summary>
       /// <param name="RoverInputModel"></param>
       /// <returns></returns>
        public  List<Rover.Model.RoverOutputModel> GetRoverCoordinates(string RoverInputModel)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            var commands = RoverInputModel.TrimEnd().Split(stringSeparators, StringSplitOptions.None);
            List<RoverInputModel> inputModels = new List<RoverInputModel>();
            var gridDimensions = commands[0].Split(' ');  // First line sets the plateaus dimensoins

            RoverControlModel RoverControlModel = new RoverControlModel();
            RoverControlModel.GridTopX = int.Parse(gridDimensions[0]);
            RoverControlModel.GridTopY = int.Parse(gridDimensions[1]);

            for (int i = 1; i < commands.Length; i += 2)
            {
                RoverInputModel model = new RoverInputModel();
                var postion = commands[i].Split(' ');
                model.Position.PositionX = int.Parse(postion[0]);
                model.Position.PositionY = int.Parse(postion[1]);
                model.Position.Heading = postion[2];
                model.Instructions = commands[i + 1];

                RoverControlModel.RoverInputs.Add(model);
            }
            string inputJson = JsonConvert.SerializeObject(RoverControlModel);
             
            List<RoverOutputModel> FinalRoverCoordinates = new List<RoverOutputModel>();
            try
            {
                FinalRoverCoordinates = ExecuteRoverNavigation(RoverControlModel);
            }
            catch (Exception ex)
            {
                //Log Exception 
                Console.WriteLine(ex.Message);
                throw new Exception("Error occured while accessing the service API");
            }
            return FinalRoverCoordinates;

        }
        /// <summary>
        /// Mthod to move the x,y coordinates of the Rover
        /// </summary>
        /// <param name="inputLocation"></param>
        /// <returns></returns>
        private RoverInputModel Move(RoverInputModel inputLocation)
        {

            string direction = inputLocation.Position.Heading;
            string move_direction = "";
            bool can_move = true;
            var currentPositionX = inputLocation.Position.PositionX;
            var currentPositionY = inputLocation.Position.PositionY;

            if (direction == "E" || direction == "W")
                move_direction = "X";
            else if (direction == "S" || direction == "N")
                move_direction = "Y";

            if (direction == "E" || direction == "N")
            {
                if (direction == "E")
                    if ((currentPositionX < 0 || (currentPositionX) > GridTopX))
                        can_move = false;

                if (direction == "N")
                    if (currentPositionY < 0 || (currentPositionY) > GridTopY)
                        can_move = false;

                if (can_move)
                {
                    if (move_direction == "X")
                        inputLocation.Position.PositionX += 1;
                    else
                        inputLocation.Position.PositionY += 1;
                }
            }
            else if (direction == "W" || direction == "S")
            {
                if (move_direction == "X" && currentPositionX > 0)
                    inputLocation.Position.PositionX -= 1;
                else if (move_direction == "Y" && currentPositionY > 0)
                    inputLocation.Position.PositionY -= 1;
            }

            return inputLocation;
        }

        /// <summary>
        /// Processes the navigation instructions
        /// </summary>
        /// <param name="RoverInputCommand"></param>
        /// <returns></returns>
        private RoverOutputModel ExecuteCommand(RoverInputModel RoverInputCommand)
        {
            var commands = RoverInputCommand.Instructions.ToCharArray();
            RoverOutputModel finalCoodinates = new RoverOutputModel();
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'L':
                        switch (RoverInputCommand.Position.Heading)
                        {
                            case "E":
                                RoverInputCommand.Position.Heading = "N";
                                break;
                            case "N":
                                RoverInputCommand.Position.Heading = "W";
                                break;
                            case "W":
                                RoverInputCommand.Position.Heading = "S";
                                break;
                            case "S":
                                RoverInputCommand.Position.Heading = "E";
                                break;
                        }
                        break;
                    case 'R':
                        switch (RoverInputCommand.Position.Heading)
                        {
                            case "E":
                                RoverInputCommand.Position.Heading = "S";
                                break;
                            case "N":
                                RoverInputCommand.Position.Heading = "E";
                                break;
                            case "W":
                                RoverInputCommand.Position.Heading = "N";
                                break;
                            case "S":
                                RoverInputCommand.Position.Heading = "W";
                                break;
                        }
                        break;
                    case 'M':
                        RoverInputCommand = Move(RoverInputCommand);
                        break;
                }
            }

            finalCoodinates.PositionX = RoverInputCommand.Position.PositionX;
            finalCoodinates.PositionY = RoverInputCommand.Position.PositionY;
            finalCoodinates.Heading = RoverInputCommand.Position.Heading;

            return finalCoodinates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoverControlModel"></param>
        /// <returns></returns>
        public List<RoverOutputModel> ExecuteRoverNavigation(RoverControlModel RoverControlModel)
        {
            GridTopX = RoverControlModel.GridTopX;
            GridTopY = RoverControlModel.GridTopY;

            List<RoverOutputModel> FinalRoverCoordinates = new List<RoverOutputModel>();

            foreach (RoverInputModel input in RoverControlModel.RoverInputs)
            {
                FinalRoverCoordinates.Add(ExecuteCommand(input));
            }
            return FinalRoverCoordinates;
        }
    }
}
