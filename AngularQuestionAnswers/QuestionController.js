var myApp=angular.module('myApp');
myApp.controller('QuestionController', function($scope) {
    $scope.sortparam='-rate';
    $scope.question={
        text: 'A new?',
        author: 'OO PPet',
        date: '20/10/2013',
        answers: 
        [{
            text: 'AngularJS!',
            author: 'Mn Ju',
            date: '20/10/2013',
            rate:2
        },{
            text: 'AngularJS Ceee',
            author: 'Cs Ec',
            date: '20/10/2013',
            rate:0
        },{
            text: 'cc knockout',
            author: 'Unnown',
            date: '21/10/2013',
            rate:0
        }]
    };
     
    $scope.voteUp = function (answer){
        answer.rate++;
    };
    $scope.voteDown = function (answer){
        answer.rate--;
    };
    $scope.questColorClass= "questcolor";
    $scope.changeClass = function (e) {
         
        $scope.questColorClass = e.type == "mouseover" ? "questselectedcolor" : "questcolor";
    }
});