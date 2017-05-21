angular.module('manageCategories', ['ngFileUpload', 'ngImgCrop', 'ui.bootstrap'])
    .controller('CategoriesController', ['$http', '$uibModal', '$scope', '$timeout', 'Upload', CategoriesController])
    .controller('ImagesModalController', ['$uibModalInstance', '$http', ImagesModalController])
    .directive('editCategory', [editCategory]);

var CATEGORY_API_PATH = '/api/categories/';

function addCategory(categories, category, id) {
    var copy = angular.copy(category);
    copy.id = id;
    copy.quizzesCount = 0;
    categories.push(copy);
    category.name = null;
    category.imageUrl = null;
}

function initCategories(ctrl, $http) {
    $http.get(CATEGORY_API_PATH + 'getAll')
        .then(function (res) {
            console.log(res);
            ctrl.categories = res.data;
        });

}

function CategoriesController($http, $uibModal, $scope, $timeout, Upload) {
        var self = this;

        self.toggleMode = function toggleMode(category) {
            console.log('edit');
            category.editMode = !category.editMode;
        };

        self.save = function save(category) {
            $http.post(CATEGORY_API_PATH + 'edit', category)
                .then(function () {
                    self.toggleMode(category);

                });
        };

        self.addNew = function addNew(category) {
            $http.post(CATEGORY_API_PATH + 'addNew', category)
                .then(function (res) {
                    console.log(res);
                    addCategory(self.categories, category, res.data.id);
                });
        }

        self.openImagesMenu = function openImagesMenu(category,size) {
            var modalInstance = $uibModal.open({
                animation: true,
                size: size,
                appendTo: $('#categories-menu'),
                templateUrl: '/Scripts/app/angular-templates/image-modal.html',
                controller: 'ImagesModalController',
                controllerAs: 'ctrl'
            });

            modalInstance.result.then(function (imageSrc) {
                if (imageSrc !== null) {
                    category.imageUrl = imageSrc;
                }

            }, function () {
            });

            modalInstance.closed.then(function () {
            });
        };

        initCategories(self, $http);

        self.upload = function (dataUrl, name) {
            Upload.upload({
                url: '/api/categories/uploadImage',
                data: {
                    file: Upload.dataUrltoBlob(dataUrl, name)
                }
            }).then(function (response) {
                $scope.result = response.data;
                console.log($scope.result);
            },function (data) {
                console.log(data);
            });
        }
    }

    /**
     * Controller
    */
    function ImagesModalController($uibModalInstance, $http) {
        var self = this;

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        self.select = function select(imgSrc) {
            console.log(imgSrc);
            $uibModalInstance.close(imgSrc);
        }

        self.deleteImage = function deleteImage(src, index) {
            $http.delete(CATEGORY_API_PATH + 'deleteImage?name=' + src)
                .then(function() {
                    self.categoryImages.splice(index, 1);
                });
        }

        $http.get(CATEGORY_API_PATH + 'getAvailableImages')
            .then(function (res) {
                console.log(res);
                self.categoryImages = [];

                res.data.forEach(function (name) {
                    self.categoryImages.push('/Content/images/categories/' + name);
                });
            });
    }

 
    function editCategory() {
        return {
            templateUrl: '/Scripts/app/angular-templates/category-view.html',
            restrict: 'AE',
            replace: true
        }
    }

    

    function addCategory(categories, category, id) {
        var copy = angular.copy(category);
        copy.id = id;
        copy.quizzesCount = 0;
        categories.push(copy);
        category.name = null;
        category.imageUrl = null;
    }