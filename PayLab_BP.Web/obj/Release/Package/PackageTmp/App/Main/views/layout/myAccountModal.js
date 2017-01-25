(function () {
    angular.module('app').controller('app.views.layout.myAccountModal', [
        '$scope', '$modal', '$modalInstance', 'abp.services.app.user',
        function ($scope, $modal, $modalInstance, userService) {
            var vm = this;

            //Loading account info
            vm.accountInfo =
            {
                userName: '',
                name: '',
                surName: '',
                emailAddress: ''
            };

            //VM functions-------------------------------------------------------------------------------------------
            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            vm.openChangeMyPasswordModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/layout/changeMyPasswordModal.cshtml',
                    controller: 'app.views.layout.changeMyPasswordModal as vm',
                    backdrop: 'static'
                });
            };
            //VM functions-------------------------------------------------------------------------------------------



            //General functions--------------------------------------------------------------------------------------
            function loadAccountInfo()
            {
                userService.getAccountInfo({}).success(function (result) {
                    vm.accountInfo.userName = result.userName;
                    vm.accountInfo.name = result.name;
                    vm.accountInfo.surName = result.surName;
                    vm.accountInfo.emailAddress = result.emailAddress;
                });
            }
            //General functions--------------------------------------------------------------------------------------
      

            loadAccountInfo();
        }
    ]);
})();