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
              templateUrl: "/ngViews/main.html",
              controller: "HomeIndexController"
          })
          .when("/result", {
              templateUrl: "/ngViews/result.html",
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