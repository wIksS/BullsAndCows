/**
 * Created by Виктор on 27.9.2014 г..
 */

app.controller('LoginCtrl',['$scope','auth','identity','notifier','$location',function($scope,auth,identity,notifier,$location){
    $scope.isLogged = identity.isLogged();

    $scope.login = function(user){
        auth.login(user)
            .then(function(data){
                identity.loginUser(data);
                $scope.isLogged = identity.isLogged();

                var user = identity.getUser();
                $scope.currentUser = user.username;
                notifier.success('Successful login !');
                $location.path('/');
            },
            function(error){
                notifier.error(error.error_description);
            });
        };

    $scope.logout = function(){
        identity.logoutUser();
        $scope.isLogged = identity.isLogged();
        $scope.user.username = '';
        $scope.user.password = '';
        notifier.success('Successful logout ! ');
        $location.path('/');
    }
}]);