
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
                hitBomb(buttonNumber).then(function (response) {
                    if (response.live) {
                        //Redirect to failed page
                        window.location.href = '/home/failed';
                    } else if (response.ended) {
                        alert("Sorry, game has ended. Please click OK to reset the game.");
                        window.location.href = '/home/reset';
                    }
                }).catch(function (error) {
                    alert("There was an error in the AJAX call.");
                });

                isGameSuccess().then(function (response) {
                    if (response.success) {
                        //Redirect to success page
                        window.location.href = '/home/success';
                    }
                }).catch(function (error) {
                    alert("There was an error in the AJAX call.");
                });

                doButtonUpdate(buttonNumber, "/Home/ShowOneButton");
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
 * This function determines if the game succeeded.
 */
function isGameSuccess() {
    // Return a new Promise
    return new Promise((resolve, reject) => {
        $.ajax({
            datatype: "json",
            method: 'POST',
            url: '/Home/IsGameSuccess', // URL to the server-side IsGameSuccess method            
            success: function (response) {
                //Resolve the Promise with true.
                resolve(response);
            },
            error: function () {
                // If there's an error with the AJAX call, reject the Promise
                reject(false);
            }
        });
    });
}

/**
 * This function uses a Promise to determine if the game has ended or the user hit a bomb.
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
            url: '/Home/GetButton', // URL to the server-side GetButton method
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