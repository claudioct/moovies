(function () {
    var app = angular.module("mooviesBoardApp");

    var SendIndexController = function ($scope, $log, $timeout, mooviesBoardApi, Upload) {
        
        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });
        $scope.$watch('file', function () {
            if ($scope.file !== null) {
                $scope.files = [$scope.file];
            }
        });


    };

    app.controller("SendIndexController", SendIndexController);
})();