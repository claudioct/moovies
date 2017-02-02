/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../moovies/scripts/moment.js" />
/// <reference path="../../moovies/scripts/angular.js" />
/// <reference path="../../moovies/scripts/angular-mocks.js" />
/// <reference path="../../moovies/scripts/angular-moment.js" />
/// <reference path="../../moovies/js/app.js" />
/// <reference path="../../moovies/js/mooviesboard-api.js" />
/// <reference path="../../moovies/js/home-index-controller.js" />

describe("home-index Tests->", function () {

    var data = [{ "leaderboardRecordId": 1, "userEmail": "claudioct@gmail.com", "userName": "claudioct@gmail.com", "totalTime": 60, "totalAwards": 0, "favoriteActor": "Jonne", "favoriteActress": "Johny", "favoriteGenre": "Drama", "created": "2017-01-30T19:41:06.953" }, { "leaderboardRecordId": 2, "userEmail": "claudioct@gmail.com", "userName": "claudioct@gmail.com", "totalTime": 60, "totalAwards": 0, "favoriteActor": "Jonne", "favoriteActress": "Johny", "favoriteGenre": "Drama", "created": "2017-01-30T19:41:06.953" }];
    var mooviesBoardApi = [{
        "leaderboardRecordId": 1,
        "userEmail": "claudioct@gmail.com",
        "userName": "claudioct@gmail.com",
        "totalTime": 60,
        "totalAwards": 0,
        "favoriteActor": "Jonne",
        "favoriteActress": "Johny",
        "favoriteGenre": "Drama",
        "created": "2017-01-30T19:41:06.953"
    },
                {
                    "leaderboardRecordId": 2,
                    "userEmail": "claudioct@gmail.com",
                    "userName": "claudioct@gmail.com",
                    "totalTime": 60,
                    "totalAwards": 0,
                    "favoriteActor": "Jonne",
                    "favoriteActress": "Johny",
                    "favoriteGenre": "Drama",
                    "created": "2017-01-30T19:41:06.953"
                }]
    var $httpBackend;

    beforeEach(angular.mock.module("mooviesBoardApp"));
    beforeEach(angular.mock.inject(function (_mooviesBoardApi_, _$httpBackend_) {
        mooviesBoardApi = _mooviesBoardApi_;
        $httpBackend = _$httpBackend_;
    })); 


    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe("dataService->", function () {

        it("can load LeaderboardRecords", function () {

            var response;

            $httpBackend.when("GET", "/api/v1/leaderboards")
                .respond(200, data);

            mooviesBoardApi.getLeaderboardRecords()
                .then(function (data) {
                    response = data;
                });

            $httpBackend.flush();

            expect(data).toEqual(response);
        });
    });
});