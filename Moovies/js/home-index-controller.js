(function () {
    var app = angular.module("mooviesBoardApp");

    var HomeIndexController = function ($scope, $log, mooviesBoardApi) {
        
        $scope.leaderboards = [];
        $scope.isBusy = true;

        mooviesBoardApi.getLeaderboardRecords()
            .then(function (response) {
                $log.debug(response);
                //SUCCESS
                angular.copy(response, $scope.leaderboards);
            },
            function () {
                //ERROR
                alert("sorry");
            })
            .then(function () {
                $scope.isBusy = false;
            });
        ;
    };

    app.controller("HomeIndexController", HomeIndexController);
})();