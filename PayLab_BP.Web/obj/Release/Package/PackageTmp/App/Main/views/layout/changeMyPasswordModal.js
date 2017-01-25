(function () {
    angular.module('app').controller('app.views.layout.changeMyPasswordModal', [
        '$scope', '$modalInstance', 'abp.services.app.user',
        function ($scope, $modalInstance, userService) {
            var vm = this;

            //VM Methods-------------------------------------------------------------------------------------------
            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            vm.savePassword = function () {
                userService.changePassword(vm.Credentials)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.close();
                    });
            };
            //VM Methods-------------------------------------------------------------------------------------------

      
        }
    ]);
})();