(function () {
    var app = angular.module("mooviesBoardApp");

    var HomeResultController = function ($scope, $log, $timeout, mooviesBoardApi, Upload, Result) {
        $scope.imdbData = Result.get();
        $log.debug("Estoy aqui!");
        $log.debug($scope.imdbData);
    };

    app.controller("HomeResultController", HomeResultController);
})();