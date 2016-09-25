angular.module("root", [])
.controller("myController", ["$scope", "$http", "$interval", function ($scope, $http, $interval) {
    $scope.currentEvent = '';
    $scope.menuState = [];
    $scope.showMatches = function (param) {                    
        $scope.matchesState = [];
        $scope.getMatches(param);
    }
                              
    $interval(function () {
        if ($scope.currentEvent != '') {
            $scope.getMatches($scope.currentEvent);
            console.log("Update - " + $scope.currentEvent);
        }

        sportsInitAndUpdate();
        console.log("Update Menu - Sports and Events");
        //console.log($scope.menuState);
        //console.log($scope.matchesState);
    }, 60000);

    window.onload = sportsInitAndUpdate();
                
    $scope.getMatches = function (param) {
        $scope.currentEvent = param;
        $http({
            method: "GET",
            url: "/api/events/EventMatchesFullInfo?eventId=" + $scope.currentEvent
        }).then(function succes(response) {
            $scope.matches = response.data;
        }, function error(response) {
            console.log("Update Error");
        });
    }

    function sportsInitAndUpdate() {
        $http({
            method: "GET",
            url: "http://localhost:54166/api/sports/SportsWithTheirEvents"
        }).then(function success(response) {
            $scope.sports = response.data;
        }, function error(response) {
            console.log("Error Accessing Menu Elements - " + response.data);
        })
    }
                                               
    $scope.addMenuElement = function (elementIndex) {
        if ($scope.menuState[elementIndex] != null) {
            //console.log("Menu element exist")
            return;
        }

        $scope.menuState[elementIndex] = false;
    }

    $scope.addMatchElement = function (elementIndex) {
        if ($scope.matchesState[elementIndex] != null) {
            //console.log("Match element exist")
            return;
        }

        $scope.matchesState[elementIndex] = false;
    }
}])