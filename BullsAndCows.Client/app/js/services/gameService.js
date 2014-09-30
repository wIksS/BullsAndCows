/**
 * Created by Виктор on 28.9.2014 г..
 */
app.factory('gameService', ['$http','$q','baseUrl','httpRequester',function($http,q,baseUrl,httpRequester) {
    var url = baseUrl + '/api/games';

    return {
        quess:function(input){
            var number = {
                number:input.number
            };

            return httpRequester.request({
                method: 'POST',
                url: url + '/' + input.Id + '/quess',
                data:ObjectToQueryString(number),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization':'Bearer ' +input.identity
                }
            });
        },
        login:function(game){
            var number = {
                number:game.number
            }

            return httpRequester.request({
                method: 'PUT',
                url: url + '/' + game.Id,
                data: ObjectToQueryString(number),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization': 'Bearer ' + game.identity
                }
            });
        },
        getGames:function(user){
            var headers = {
                'Content-Type': 'application/x-www-form-urlencoded'
            };
            if(user){
                headers.Authorization = 'Bearer ' + user.token;

            }

            return httpRequester.request({
                method: 'GET',
                url: url,
                headers: headers
            });
        },
        create: function (game) {
            return httpRequester.request({
                method: 'POST',
                url: url,
                data: ObjectToQueryString(game),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization':'Bearer ' +game.identity
                }
            });
        }
    }
}]);