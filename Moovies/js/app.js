(function () {
    var app = angular.module("mooviesBoardApp", ['angularMoment', 'ngFileUpload'])
        .filter('numberToHuman', function () {
            return function (num, precision) {
                return Humanize.compactInteger(num, precision);
            };
        });
})();