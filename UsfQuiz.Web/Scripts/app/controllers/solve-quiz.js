 angular.module('solveQuiz', ['ui.bootstrap', 'toaster'])
        .controller('solveQuizController', ['$http', '$timeout', '$window', 'toaster', solveQuizController]);

    function solveQuizController($http, $timeout, $window,toaster) {

        var self = this;
        /**
         * Mark the selected answer of quiz a question
         * @param {int} questionIndex
         * @param {int} selectedAnswerIndex 
         * @returns {void} 
         */
        self.select = function (questionIndex, selectedAnswerIndex) {
            var question = self.quiz.Questions[questionIndex];
            question.selected = selectedAnswerIndex;
        };
           
        self.submit = function submit() {
            var flag = false;
            var data = {
                forQuizId: quiz.Id,
                selectedAnswerIds: quiz.Questions.map(function (question) {
                    if (question.Answers[question.selected] != undefined) {
                        var id = question.Answers[question.selected].Id;
                        return id;
                    }
                })
            };
           
            for (var i = 0; i < data.selectedAnswerIds.length; i++) {
                if (data.selectedAnswerIds[i] == undefined) {
                    toaster.pop({
                        type: 'error',
                        title: 'Question not solved',
                        body: 'You haven\'t selected an option for question ' + (i + 1),
                        timeout: 8000,
                        showCloseButton: true
                    });
                    flag = true;
                }
            }
            if (flag === false) {
                $http.post('/SolveQuiz/solve', data).then(function (response) {
                        document.write(response.data);
                });
            }
        };
        this.init();
    }

    /**
     * Initializes the properties of the SolveQuizController
     * @private
     */
 solveQuizController.prototype.init = function init() {

        var self = this;

        self.questionTemplate = '/Scripts/app/angular-templates/solve.html';
        self.quiz = quiz;
        self.questionsCount = quiz.Questions.length;
    };