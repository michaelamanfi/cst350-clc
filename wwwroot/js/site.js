
$(function () {

    $(document).bind("contextmenu", function (e) {
        //Right click. Prevent context menu from showing.
        e.preventDefault();
    });
    $(document).on("mousedown", ".game-button", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();

                var buttonNumber = $(this).val();
                hitBomb(buttonNumber).then(function (hit) {
                    if (hit == 1) {
                        //Redirect to failed page
                        window.location.href = '/home/failed';
                    } else if (hit == 2) {
                        alert("Sorry, game has ended. Please click OK to reset the game.");
                        window.location.href = '/home/reset';
                    }
                }).catch(function (error) {
                    alert("There was an error in the AJAX call.");
                });

                isGameSuccess().then(function (isSuccess) {
                    if (isSuccess) {
                        //Redirect to success page
                        window.location.href = '/home/success';
                    }
                }).catch(function (error) {
                    alert("There was an error in the AJAX call.");
                });

                doButtonUpdate(buttonNumber, "/Home/ShowOneButton");
                break;
            case 3:
                event.preventDefault();
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
                if (response.success) {
                    //Resolve the Promise with true.
                    resolve(true);
                } else {
                    //Resolve the Promise with false.
                    resolve(false);
                }
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
                if (response.live) {
                    //Resolve the Promise with 1.
                    resolve(1);
                } else if (response.ended) {
                    //Resolve the Promise with 2.
                    resolve(2);
                } else {
                    //Resolve the Promise with 3.
                    resolve(3);
                }
            },
            error: function () {
                // If there's an error with the AJAX call, reject the Promise
                reject(false);
            }
        });
    });
}