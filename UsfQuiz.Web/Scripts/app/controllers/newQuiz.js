angular.module('manageQuiz', ['toggle-switch', 'ui.bootstrap', 'toaster'])
    .controller('newQuizController', ['$scope', '$controller', '$uibModal', 'toaster', '$http', '$location', newQuizController])
    /*.controller('manageQuizController', ['$scope', '$http', /*'$uibModal',#1# 'toaster', manageQuizController])*/
    .controller('AddQuestionController', ['$uibModalInstance', 'items', AddQuestionController]);

    

function newQuizController($scope, $controller, $uibModal, toaster, $http) {
        var self = this;
        var EMPTY_QUIZ = {
            name: '',
            description: '',
            termsConditions: '',
            category: '',
            questions: []
        };

        self.popups = {
            NUMBER_OF_QUESTIONS: 'You can add as many questions as you want using the button below. Here, add the number of questions to be shown per quiz. You should add at least 4 questions. Random questions will be picked in every quiz if this value is lesser than the number of questions saved.',
            ADD_QUESTIONS: 'Please add at least 4 questions!'
        };

        $http.get('/api/categories/getCategories')
            .then(function (res) {
                console.log(res.data);
                $scope.categories = res.data;
            });

        self.setQuiz = function setQuiz(quiz) {
            $scope.quiz = quiz;
        }

        self.setQuiz(angular.copy(EMPTY_QUIZ));

        self.callToaster = function callToaster(message) {
            toaster.pop('success', "Quiz Created", message, 3000, 'trustedHtml');
        }

        self.callErrorToaster = function callErrorToaster(message) {
            toaster.pop('error', "Quiz Adding Failed", message, 3000, 'trustedHtml');
        }

        self.removeQuestion = function removeQuestion(index) {
            $scope.quiz.questions.splice(index, 1);
        };

        $scope.modalIsOpen = false;

        self.openQuestionModel = function openQuestionModel(question) {
            $scope.modalIsOpen = true;
            var modalInstance = $uibModal.open({
                appendTo: $('#manage-quiz'),
                templateUrl: '/Scripts/app/angular-templates/new-question.html',
                controller: 'AddQuestionController',
                controllerAs: 'ctrl',
                resolve: {
                    items: question
                }
            });

            modalInstance.result.then(function (question) {
                if (question !== null) {
                    $scope.quiz.questions.push(question);
                }
            }, function () {
            });

            modalInstance.closed.then(function () {
                $scope.modalIsOpen = false;
            });
        };

        self.saveIsAvailable = function saveIsAvailable(quiz, form) {
            var result = form.$valid &&
                quiz.questions.length >= 3;
            return result;
        };

        

        self.addQuiz = function addQuiz(quiz, form) {
            console.log(quiz);
		    $http.post('/api/Quizzes/Create', quiz)
		        .then(function (response) {
		            self.setQuiz(angular.copy(EMPTY_QUIZ));
		            self.callToaster('Quiz added successfully');
		        }, function (error) {
		            console.log(error);
                    if (error.status == 400) {
                        self.callErrorToaster('Quiz by this name exists already!');
                    }
		        });
		};
    }

    /*function manageQuizController($scope, $http, $uibModal, toaster) {
    //var self = this;

    

    }*/

function AddQuestionController($uibModalInstance, resource) {
    var self = this, clone = '';
    self.letters = 'ABCDEF';
    if (resource) {
        self.question = resource;
        clone = JSON.stringify(resource);
    } else {
        self.question = {
            title: '',
            answers: []
        };
    }
    self.ok = function () {
        if (clone) {
            // if backup exists then this is an edit, no need to return the question
            // it is already passed by reference
            $uibModalInstance.close(null);
        } else {
            // return the question to be added to the quiz
            $uibModalInstance.close(self.question);
        }
    };

    self.cancel = function () {
        if (clone) {
            restoreClone(resource, clone);
        }
        $uibModalInstance.dismiss('cancel');
    };

    self.addChoice = function addChoice() {
        var answer = {
            text: ''
        };

        if (!self.question.answers.length) {
            answer.isCorrect = true;
        }

        self.question.answers.push(answer);
        var last = self.question.answers.length - 1;

        setTimeout(function () {
            try {
                document.getElementsByClassName('answer-field')[last].focus();
            } catch (e) { }
        }, 100);
    };

    self.removeChoice = function removeChoice(index) {
        self.question.answers.splice(index, 1);
        if (self.question.answers.length === 1) {
            self.question.answers[0].isCorrect = true;
        }
    };

    self.markCorrect = function markCorrect(index) {
        self.question.answers.forEach(function (answer, i) {
            answer.isCorrect = i === index;
        });
    };

    self.hasCorrect = function hasCorrect() {
        return self.question.answers.some(function (answer) {
            return answer.isCorrect;
        });
    };

    self.saveIsAvailable = function saveIsAvailable(questionForm) {
        var result = questionForm.$valid && self.hasCorrect() && self.question.answers.length >= 2;

        return result;
    }
}

function restoreClone(obj, backup) {
    var backupAsObject = JSON.parse(backup),
        prop;

    for (prop in backupAsObject) {
        obj[prop] = backupAsObject[prop];
    }
}