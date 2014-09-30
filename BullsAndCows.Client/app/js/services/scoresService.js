/**
 * Created by Виктор on 28.9.2014 г..
 */
app.factory('scoresService', ['$http','$q','baseUrl','httpRequester',function($http,q,baseUrl,httpRequester) {
    var url = baseUrl + '/api';

    return {
        scores: function () {
            return httpRequester.request({
                method: 'GET',
                url: url + '/scores',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            });
        }
    }
}]);