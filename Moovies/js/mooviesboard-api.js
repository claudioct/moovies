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