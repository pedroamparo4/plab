(function () {
    angular.module('app').controller('app.views.tenants.index', [
        '$scope', '$modal', 'abp.services.app.tenant',
        function ($scope, $modal, tenantService) {
            var vm = this;

            vm.tenants = [];

            function GetSupplierType(supplierTypeCode){
                switch (supplierTypeCode)
                {
                    case 1: return abp.localization.localize("SupplierType_PhoneServices");
                    case 2: return abp.localization.localize("SupplierType_ElectricServices");
                }
            }

            $scope.Edit = function (id) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/tenants/editModal.cshtml',
                    controller: 'app.views.tenants.editModal as vm',
                    resolve: {
                        editId: function () {
                            return id;
                        }
                    },
                    backdrop: 'static'

                });
            }

            function getTenants() {
                abp.ui.setBusy
                 (
                     null,
                     tenantService.getTenants({}).success(function (result) {
                         var table_content = [];
                         var content = '';
                         var tableEntityName = 'tenants';

                         for (var i = 0; i < result.items.length; i++) {
                             content += '<tr>';
                             content += '<td>';

                             content += '<div class="btn-group">';
                             content += '<button data-toggle="dropdown" class="btn btn-primary dropdown-toggle" aria-expanded="false">Action <span class="caret"></span></button>';
                             content += '<ul class="dropdown-menu">';
                             content += '<li onclick="Edit(' + result.items[i].id + ')"><a><strong>Edit</strong></a></li>';
                             content += '</ul>';
                             content += '</div>';

                             content += '</td>';
                             content += '<td>';
                             content += result.items[i].tenancyName;
                             content += '</td>';
                             content += '<td>';
                             content += result.items[i].name;
                             content += '</td>';
                             content += '<td>';
                             content += GetSupplierType(result.items[i].tenantType);
                             content += '</td>';
                             content += '<td>';
                             content += '<input class="btswitch" name="' + result.items[i].id + '" id="' + result.items[i].id + '" type="checkbox" ' + (result.items[i].isActive ? 'checked' : '') + ' data-size="mini">';
                             content += '</td>';
                             content += '</tr>';
                         }

                         if ($.fn.DataTable.isDataTable('#' + tableEntityName + 'Table')) {
                             var table = $('#' + tableEntityName + 'Table').DataTable();
                             table.destroy();
                         }

                         $('#' + tableEntityName + 'TableBody').empty();
                         $('#' + tableEntityName + 'TableBody').append(content);
                         $(".btswitch").bootstrapSwitch();
                         $(".btswitch").on('switchChange.bootstrapSwitch', function (event, state) {
                             changeStatus($(this).attr('id'), state);
                         });

                         $('#' + tableEntityName + 'Table').DataTable({
                             pageLength: 25,
                             responsive: true,
                             dom: '<"html5buttons"B>lTfgitp',
                             buttons: [
                                 {
                                     extend: 'copy',
                                     exportOptions: {
                                        columns: [0, 1, 2]
                                     }
                                 },
                                 {
                                     extend: 'csv',
                                     exportOptions: {
                                         columns: [0, 1, 2]
                                     }
                                 },
                                 {
                                     extend: 'excel', title: tableEntityName + 'FileExport',
                                     exportOptions: {
                                         columns: [0, 1, 2]
                                     }
                                 },
                                 {
                                     extend: 'pdf', title: tableEntityName + 'FileExport',
                                     exportOptions: {
                                         columns: [0, 1, 2]
                                     }
                                 },

                                 {
                                     extend: 'print',
                                     exportOptions: {
                                         columns: [0, 1, 2]
                                     },
                                     customize: function (win) {
                                         $(win.document.body).addClass('white-bg');
                                         $(win.document.body).css('font-size', '10px');

                                         $(win.document.body).find('table')
                                                 .addClass('compact')
                                                 .css('font-size', 'inherit');
                                     }
                                 }
                             ]

                         });

                     })
                 );
            }

            function changeStatus(id, state) {
                tenantService.changeTenantStatus(id).success(function (e) {
                    abp.notify.success(abp.localization.localize((state == true ? "TenantIsActiveToggleMessage" : "TenantIsInactiveToggleMessage")));
                });
            }

            vm.openTenantCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/tenants/createModal.cshtml',
                    controller: 'app.views.tenants.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getTenants();
                });
            };

            getTenants();
        }
    ]);
})();