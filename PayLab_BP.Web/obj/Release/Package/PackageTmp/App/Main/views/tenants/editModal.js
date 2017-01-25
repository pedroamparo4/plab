(function () {
    angular.module('app').controller('app.views.tenants.editModal', [
        '$scope', '$modalInstance', 'abp.services.app.tenant', 'editId',
        function ($scope, $modalInstance, tenantService, editId) {
            var vm = this;

            vm.tenant = {
                id: 0,
                tenancyName: '',
                name: '',
                adminEmailAddress: '',
                connectionString: '',
                tenantType: 0
            };

            vm.tenantType;

            vm.save = function () {
                abp.ui.setBusy();
                tenantService.saveEditTenant(vm.tenant)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.close();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            function getTenant() {
                tenantService.getTenant(editId).success(function (result) {
                    vm.tenant.tenancyName = result.tenancyName;
                    vm.tenant.name = result.name;
                    vm.tenant.connectionString = result.connectionString;
                    vm.tenant.connectionString = result.connectionString;
                    vm.tenant.tenantType = result.tenantType;
                    vm.tenant.id = result.id;
                    vm.tenantType = result.tenantType;
                });
            }

            vm.SupplierType =
            [
                { id: 1, name: abp.localization.localize("SupplierType_PhoneServices") },
                { id: 2, name: abp.localization.localize("SupplierType_ElectricServices") }
            ];
            
            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            getTenant();
        }
    ]);
})();