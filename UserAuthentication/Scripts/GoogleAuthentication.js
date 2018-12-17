/// <reference path="jquery-3.3.1.min.js" />
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var access_token = location.hash.split('access_token=')[1].split('&')[0];
            if (access_token) {
                isUserRegistered(access_token);
            }
        }
    }
}

function isUserRegistered(access_token) {
    $.ajax({
        url: 'api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + access_token
        },
        success: function (response) {
            if (response.HasRegistered) {
                localStorage.setItem('accessToken', access_token);
                localStorage.setItem('userName', response.Email);
                window.location.href = "Data.html";
            }
            else {
                signupExternalUser(access_token, response.LoginProvider);
            }
        }
    });
}

function signupExternalUser(access_token, provider) {
    $.ajax({
        url: 'api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + access_token
        },
        success: function () {
            window.location.href = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A64047%2FLogin.html&state=aDOw7M3f17c-oQZCgK6f6hvM8WQqbklq2MHPjOE2GIU1";
        }
    });
}