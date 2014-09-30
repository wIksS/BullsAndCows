/**
 * Created by Виктор on 28.9.2014 г..
 */

app.controller('GameCtrl',['$scope','$location','identity','notifier','gameService',function($scope,$location,identity,notifier,gameService){
    $scope.isLogged = identity.isLogged();
    $scope.user = identity.getUser();

    $scope.login = function(game){
        var user = identity.getUser();
        game.identity = user.token;
        gameService.login(game)
            .then(function(data){
                console.log(data);
                $location.path('/games/play/' + game.Id);
            },function(err){
                notifier.error(err);
                console.log(err);
            })
    }

    $scope.create = function(game){
        if(!identity.isLogged()){
            notifier.error('You must be logged to create a game !');
            return;
        }
        var user = identity.getUser();
        game.identity = user.token;
        gameService.create(game)
            .then(function(data){
                console.log(data);
            },function(err){
                notifier.error(err);
                console.log(err);
            })
    }

    $scope.$on("$routeChangeSuccess", function($currentRoute, $previousRoute) {
        if(identity.isLogged()){
            $scope.isLogged = identity.isLogged();
            $scope.user = identity.getUser();
            gameService.getGames(identity.getUser())
                .then(function (data) {
                    $scope.games = data;
                });
        }
        else {
            gameService.getGames()
                .then(function (data) {
                    $scope.games = data;
                    console.log(data);
                });
        }
    });
}])