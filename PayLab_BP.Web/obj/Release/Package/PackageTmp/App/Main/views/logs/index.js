(function () {
    angular.module('app').controller('app.views.logs.index', [
        '$scope', '$modal', 'abp.services.app.log',
        function ($scope, $modal, logService) {
            var vm = this;

            vm.logs = [];

            $scope.getLogs = function () {
                abp.ui.setBusy
                 (
                     null,
                     logService.getLogs($('#dtFrom').val(), $('#dtTo').val()).success(function (result) {
                         var table_content = [];
                         var content = '';
                         var tableEntityName = 'logs';

                         for (var i = 0; i < result.length; i++) {
                             content += '<tr>';
                             content += '<td>';
                             content += result[i].executionTime;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].serviceName;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].methodName;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].clientIpAddress;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].clientName;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].browserInfo;
                             content += '</td>';
                             content += '</tr>';
                         }

                         if ($.fn.DataTable.isDataTable('#' + tableEntityName + 'Table')) {
                             var table = $('#' + tableEntityName + 'Table').DataTable();
                             table.destroy();
                         }

                         $('#' + tableEntityName + 'TableBody').empty();
                         $('#' + tableEntityName + 'TableBody').append(content);

                         $('#' + tableEntityName + 'Table').DataTable({
                             pageLength: 25,
                             responsive: true,
                             dom: '<"html5buttons"B>lTfgitp',
                             buttons: [
                                 {
                                     extend: 'copy',
                                 },
                                 {
                                     extend: 'csv',
                                 },
                                 {
                                     extend: 'excel', title: tableEntityName + 'FileExport',
                                 },
                                 {
                                     extend: 'pdf', title: tableEntityName + 'FileExport',
                                 },

                                 {
                                     extend: 'print',
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


            $scope.getLogs();
        }
    ]);
})();