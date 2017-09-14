(function () {
    var app = angular.module("mooviesBoardApp");

    var HomeIndexController = function ($scope, $log, $location, $timeout, mooviesBoardApi, Upload, Result) {
        
        //$scope.$watch('files', function () {
        //    $scope.upload($scope.files);
        //});
        //$scope.$watch('file', function () {
        //    if ($scope.file !== null) {
        //        $scope.files = [$scope.file];
        //    }
        //});

        //$log.debug("Here");

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

        $scope.onFileSelect = function ($files) {
                $log.debug("Here");
        }

        // upload on file select or drop
        $scope.upload = function (files) {
            $log.debug("veio");
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (!file.$error) {
                        Upload.upload({
                            url: '/api/v1/files',
                            data: {
                                file: files
                            }
                        }).then(function (resp) {
                            $timeout(function () {
                                $scope.log = 'file: ' +
                                resp.config.data.file.name +
                                ', Response: ' + JSON.stringify(resp.data) +
                                '\n' + $scope.log;
                                Result.set(resp.data);
                                $location.path("/result");
                            });
                        }, null, function (evt) {
                            var progressPercentage = parseInt(100.0 *
                                    evt.loaded / evt.total);
                            $scope.log = 'progress: ' + progressPercentage +
                                '% ' + evt.config.data.file.name + '\n' +
                              $scope.log;
                        });
                    }
                }
            }
        };
    };

    app.controller("HomeIndexController", HomeIndexController);
})();