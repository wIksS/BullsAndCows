/**
 * Created by Виктор on 28.9.2014 г..
 */

app.controller('PlayCtrl',['$scope','$routeParams','identity','gameService','notifier',function($scope,$routeParams,identity,gameService,notifier){
    $scope.play = function(game){
        $scope.game = game;
    }

    $scope.cows = 4;
    $scope.bulls = 4;
    $scope.bullsAndCowsLength = [1,2,3,4];

    $scope.quess = function(number){
        var input = {
            number:number
        };

        var user = identity.getUser();
        input.identity = user.token;
        input.Id = $routeParams.Id;
debugger;
        gameService.quess(input)
            .then(function(data){
                $scope.cows = data.CowsCount;
                $scope.bulls = data.BullsCount;
                console.log(data);
            },function(err){
                notifier.error(err.Message);
            });
    }
}])