(function () {
    angular.module('app').controller('app.views.tenants.createModal', [
        '$scope', '$modalInstance', 'abp.services.app.tenant',
        function ($scope, $modalInstance, tenantService) {
            var vm = this;

            vm.tenant = {
                tenancyName: '',
                name: '',
                adminEmailAddress: '',
                connectionString: '',
                tenantType: 0
            };

            vm.save = function () {
                abp.ui.setBusy();
                tenantService.createTenant(vm.tenant)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.close();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.SupplierType =
            [
                { code: 1, name: abp.localization.localize("SupplierType_PhoneServices") },
                { code: 2, name: abp.localization.localize("SupplierType_ElectricServices") }
            ];
            
            vm.cancel = function () {
                $modalInstance.dismiss();
            };
        }
    ]);
})();