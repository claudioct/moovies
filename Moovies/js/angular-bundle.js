(function () {
    var app = angular.module("mooviesBoardApp", ['angularMoment']);
})();
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
(function () {
    var baseUrl = "http://localhost:43738/api/v1/";

    var mooviesBoardApi = function ($http, $log) {

        var getLeaderboardRecords = function () {
            return $http.get(baseUrl + "leaderboards")
                .then(function (response) {
                    $log.debug(response);
                    return response.data;
                });
        };

        return {
            getLeaderboardRecords: getLeaderboardRecords
        };
    };

    var app = angular.module("mooviesBoardApp");
    app.factory("mooviesBoardApi", mooviesBoardApi)
})();