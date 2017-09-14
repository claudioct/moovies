(function () {
    var app = angular.module("mooviesBoardApp");

    var HomeResultController = function ($scope, $log, $timeout, mooviesBoardApi, Upload, Result) {
        $scope.imdbData = Result.get();        
    };

    app.controller("HomeResultController", HomeResultController);
})();