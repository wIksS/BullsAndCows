'use strict';
function ObjectToQueryString(obj) {
    var p = [];
    for (var key in obj) {
        p.push(key + '=' + obj[key]);
    }
    return p.join('&');
};

var app = angular.module('bullsAndCowsApp',['ngRoute'])
    .config(['$routeProvider',function($routeProvider){
        $routeProvider.
            when('/', {
                templateUrl: '../views/partials/home.html'
            }).
            when('/register', {
                templateUrl: '../views/partials/register.html',
                controller:'RegisterCtrl'
            }).
            when('/games', {
                templateUrl: '../views/partials/list-games.html',
                controller:'GameCtrl'
            }).
            when('/scores', {
                templateUrl: '../views/partials/scores.html',
                controller:'ScoresCtrl'
            }).
            when('/games/create', {
                templateUrl: '../views/partials/create-game.html',
                controller:'GameCtrl'
            }).
            when('/games/play/:Id', {
                templateUrl: '../views/partials/play.html',
                controller:'PlayCtrl'
            })

}])
    .value('toastr',toastr)
    .constant('baseUrl','http://localhost:10497');