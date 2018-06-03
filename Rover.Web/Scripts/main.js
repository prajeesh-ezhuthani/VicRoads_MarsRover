var validateInput = function () {
    var RoverInputModel = $('#RoverInputModel').val();
    var errorMessage = "";
    $("#errorMessage").text("");
    if (RoverInputModel.length > 0) {
       var inputArray = RoverInputModel.split(/\n|\r/);
        if (inputArray.length > 2) {
            var coordinates = inputArray[0];

            if (! ValidateCoordinates(coordinates)){
                $("errorMessage").text("Please enter a valid coordinate for the plateau").show();
                return false;
            }

            var RoverCount = 1;
            for (var i = 1; i < inputArray.length; i += 2) {
                var position = inputArray[i];
                var instruction = inputArray[i + 1];

                if (!ValidatePosition(position)) {
                    $("#errorMessage").text("Please enter a valid position for the Rover " + RoverCount).show();
                    return false;
                }
                //Validate the instruction 
                if (!ValidateInstruction(instruction)) {
                    $("#errorMessage").text("Please enter a valid instruction (Allowed values are L,R and M)").show();
                    return false;
                }
                RoverCount++;
            }
       }
        else {
            $("#errorMessage").text("Please enter all the requrired details").show();
            return false;
        }
    }
    else {
        $("#errorMessage").text("Please enter all the requrired details").show();
        return false;
    }
    return true;
}
var ValidateInstruction = function (instruction) {
    var exp = /^[LRM]*$/;
    if (instruction.length > 0) {
        instruction.match(exp)
        if (!instruction.match(exp)) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }
}
var ValidatePosition = function (position) {
    var exp = /^\d \d [NSEW]$/;
    position.match(exp)
    if (!position.match(exp)) {
        return false;
    }
    else {
        return true;
    }
}
var ValidateCoordinates = function (position) {
    var exp = /^\d?\d \d?\d$/;
    position.match(exp)
    if (!position.match(exp)) {
        return false;
    }
    else {
        return true;
    }
}