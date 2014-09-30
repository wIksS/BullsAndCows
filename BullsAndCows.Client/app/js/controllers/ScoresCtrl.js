/**
 * Created by Виктор on 28.9.2014 г..
 */
app.controller('ScoresCtrl',['$scope','scoresService','notifier',function($scope,scoresService,notifier) {
    scoresService.scores()
        .then(function(data){
            $scope.scores = data;
            console.log($scope.scores);
        },function(err){
            notifier.error(err);
    });
}]);