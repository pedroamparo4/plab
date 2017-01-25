(function () {
    angular.module('app').controller('app.views.users.index', [
        '$scope', '$modal', 'abp.services.app.user',
        function ($scope, $modal, userService) {
            var vm = this;

            vm.users = [];

            function getUsers() {
                abp.ui.setBusy
                 (
                     null,
                     userService.getUsers({}).success(function (result) {
                         var table_content = [];
                         var tableEntityName = 'users';
                         var content = '';

                         for (var i = 0; i < result.length; i++) {
                             content += '<tr>';
                             content += '<td>';
                             content += result[i].userName;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].name;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].lastName;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].emailAddress;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].roleType;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].supplierName;
                             content += '</td>';
                             content += '<td>';
                             content += '<input class="btswitch" name="' + result[i].id + '" id="' + result[i].id + '" type="checkbox" ' + (result[i].isActive ? 'checked' : '') + ' data-size="mini">';
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
                userService.changeUserStatus(id).success(function (e) {
                    abp.notify.success(abp.localization.localize((state == true ? "UserIsActiveToggleMessage" : "UserIsInactiveToggleMessage")));
                });
            }

            vm.openUserCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/users/createModal.cshtml',
                    controller: 'app.views.users.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getUsers();
                });
            };

            getUsers();
        }
    ]);
})();