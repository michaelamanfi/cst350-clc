
$(function () {

    $(document).bind("contextmenu", function (e) {
        //Right click. Prevent context menu from showing.
        e.preventDefault();
    });
    $(document).on("mousedown", ".game-button", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault(); //Disable the default behavior

                var buttonNumber = $(this).val(); //Button coordinates is in the form x_y.

                // Update the button before check the status of the game.
                doButtonUpdate(buttonNumber, "/Home/ShowOneButton");

                hitBomb(buttonNumber).then(function (response) {
                    if (response.live) {
                        //Redirect to failed page
                        window.location.href = '/home/failed';
                    } else if (response.success) {
                        //Redirect to success page
                        window.location.href = '/home/success';
                    }
                    else if (response.ended) {
                        alert("Sorry, game has ended. Please click OK to reset the game.");
                        window.location.href = '/home/reset';
                    }
                }).catch(function (error) {
                    alert("There was an error in the AJAX call.");
                });

                break;
            case 3:
                event.preventDefault(); //Disable the default behavior

                var buttonNumber = $(this).val();

                doButtonUpdate(buttonNumber, "/Home/RightClickShowOneButton");
                break;
            default:
                alert('Nothing');
        }
    });
    
});

/**
 * This function updates the specified button with the output of the REST API.
 * 
 * @param {any} buttonNumber The button to update.
 * @param {any} urlString The URL to do the HttpPost to.
 */
function doButtonUpdate(buttonNumber, urlString) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: urlString,
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            $("#" + buttonNumber).html(data);
        }
    });
}

/**
 * This function uses a Promise to return the state of play.
 * @param {any} buttonNumber
 */
function hitBomb(buttonNumber) {
    // Return a new Promise
    return new Promise((resolve, reject) => {
        $.ajax({
            datatype: "json",
            method: 'POST',
            data: {
                "buttonNumber": buttonNumber
            },
            url: '/Home/GetButtonMetadata', // URL to the server-side GetButtonMetadata method
            success: function (response) {
                //Resolve the Promise with response.
                resolve(response);
            },
            error: function () {
                // If there's an error with the AJAX call, reject the Promise
                reject(false);
            }
        });
    });
}