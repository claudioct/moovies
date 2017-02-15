(function () {
    var app = angular.module("mooviesBoardApp", ["ngRoute", "angularMoment", "ngFileUpload"])
        .filter("numberToHuman", function () {
            return function (num, precision) {
                return Humanize.compactInteger(num, precision);
            };
        });

    app.config(function ($routeProvider) {
        $routeProvider
          .when("/main", {
              templateUrl: "ngViews/main.html",
              controller: "HomeIndexController"
          })
          .when("/result", {
              templateUrl: "ngViews/result.html",
              controller: "HomeResultController"
          })
          .otherwise({
              redirectTo: "/main"
          });
    });

    // Create the factory that share the Fact
    app.factory('Result', function () {
        var _data = {};

        return {
            get: function () {
                return _data;
            },
            set: function (data) {
                _data = data;
            }
        };
    });
})();
(function () {
    var app = angular.module("mooviesBoardApp");

    var HomeIndexController = function ($scope, $log, $location, $timeout, mooviesBoardApi, Upload, Result) {
        
        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });
        $scope.$watch('file', function () {
            if ($scope.file !== null) {
                $scope.files = [$scope.file];
            }
        });

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

        // upload on file select or drop
        $scope.upload = function (files) {
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
(function () {
    var baseUrl = "/api/v1/";
    
    var mooviesBoardApi = function ($http, $log) {



        var getLeaderboardRecords = function () {
            return $http.get(baseUrl + "leaderboards")
                .then(function (response) {
                    $log.debug(response);
                    return response.data;
                });
        };

        return {
            getLeaderboardRecords: getLeaderboardRecords,
            leaderboards: []
        };
    };

    var app = angular.module("mooviesBoardApp");
    app.factory("mooviesBoardApi", mooviesBoardApi)
})();