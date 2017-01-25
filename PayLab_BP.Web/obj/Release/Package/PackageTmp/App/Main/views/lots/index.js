(function() {
    angular.module('app').controller('app.views.lots.index', [
        '$scope', '$modal', 'abp.services.app.lot', '$filter',
        function ($scope, $modal, lotService, $filter) {
            var vm = this;
            vm.localize = abp.localization.getSource("PayLab");
            $scope.lotId = 0;

            function getLots() {
                abp.ui.setBusy
                 (
                     null,
                     lotService.getLots('', '').success(function (result) {
                         var table_content = [];
                         var tableEntityName = 'lots';
                         var content = '';

                         for (var i = 0; i < result.length; i++) {
                             content += '<tr>';
                             content += '<td>';
                             content += '<button id="" onclick="loadPayments(' + result[i].id + ')" class="btn btn-primary animatedModalBtn" href="#animatedModal">Ver pagos</button>';
                             content += '</td>';
                             content += '<td>';
                             content += result[i].number;
                             content += '</td>';
                             content += '<td>';
                             content += result[i].payments.length;
                             content += '</td>';
                             content += '<td>';
                             content += $filter('date')(result[i].dateFrom, 'dd/MM/yyyy');
                             content += '</td>';
                             content += '<td>';
                             content += $filter('date')(result[i].dateTo, 'dd/MM/yyyy');
                             content += '</td>';
                             content += '</tr>';
                         }


                         if ($.fn.DataTable.isDataTable('#' + tableEntityName + 'Table')) {
                             var table = $('#' + tableEntityName + 'Table').DataTable();
                             table.destroy();
                         }

                         $('#' + tableEntityName + 'TableBody').empty();
                         $('#' + tableEntityName + 'TableBody').append(content);

                         $(".animatedModalBtn").animatedModal({
                             color: '#ffffff',
                             // Callbacks
                             beforeOpen: function () {
                                 InitDatePicker();
                             },
                             afterOpen: function () {

                             },
                             beforeClose: function () {

                             },
                             afterClose: function () {
                                 var table = $('#paymentsTable').DataTable();
                                 table.clear();
                                 table.destroy();
                             }
                         });

                         $('#' + tableEntityName + 'Table').DataTable({
                             pageLength: 25,
                             responsive: true,
                             dom: '<"html5buttons"B>lTfgitp',
                             buttons: [
                                 {
                                     extend: 'copy',
                                     exportOptions: {
                                         columns: [1, 2, 3, 4]
                                     }
                                 },
                                 {
                                     extend: 'csv',
                                     exportOptions: {
                                         columns: [1, 2, 3, 4]
                                     }
                                 },
                                 {
                                     extend: 'excel', title: tableEntityName + 'FileExport',
                                     exportOptions: {
                                         columns: [1, 2, 3, 4]
                                     }
                                 },
                                 {
                                     extend: 'pdf', title: tableEntityName + 'FileExport',
                                     exportOptions: {
                                         columns: [1, 2, 3, 4]
                                     }
                                 },

                                 {
                                     extend: 'print',
                                     exportOptions: {
                                         columns: [1, 2, 3, 4]
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

            $scope.loadPayments = function (lotId) {

                abp.ui.setBusy(
                null,
                lotService.getLotPayments(lotId, $('#dt_from_payments').val(), $('#dt_to_payments').val()).success(function (result) {

                    var payments = result;
                    var totalPayments = 0;
                    var content = '';

                    for (var i = 0; i < payments.length; i++) {

                        totalPayments += payments[i].amount;
                        content += '<tr>';
                        content += '<td>';
                        content += payments[i].transactionNumber;
                        content += '</td>';
                        content += '<td>';
                        content += payments[i].tenantName;
                        content += '</td>';
                        content += '<td>';
                        content += $filter('date')(payments[i].contractNumber, 'dd/MM/yyyy');
                        content += '</td>';
                        content += '<td>';
                        content += $filter('currency')(payments[i].amount, '$');
                        content += '</td>';
                        content += '<td>';
                        content += $filter('date')(payments[i].date, 'H:mm:ss');
                        content += '</td>';
                        content += '<td>';
                        content += $filter('date')(payments[i].time, 'H:mm:ss');
                        content += '</td>';
                        content += '<td>';
                        content += payments[i].paymentType;
                        content += '</td>';
                        content += '</tr>';
                    }

                    $('#totalPayments').empty().append($filter('currency')(totalPayments, '$'));
                    $('#quantityPayments').empty().append(payments.length);
                    
                    if ($.fn.DataTable.isDataTable('#paymentsTable')) {
                        var table = $('#paymentsTable').DataTable();
                        table.clear();
                        table.destroy();
                    }

                    $('#' + 'payments' + 'TableBody').empty();
                    $('#' + 'payments' + 'TableBody').append(content);

                    $('#paymentsTable').DataTable({
                        initComplete: function (settings, json) {
                            $('#animatedModal').show();
                        },
                        pageLength: 25,
                        responsive: true,
                        dom: '<"html5buttons"B>lTfgitp',
                        buttons: [
                            {
                                extend: 'copy',
                                exportOptions: {
                                    columns: [1, 2, 3, 4, 5, 6]
                                }
                            },
                            {
                                extend: 'csv',
                                exportOptions: {
                                    columns: [1, 2, 3, 4, 5, 6]
                                }
                            },
                            {
                                extend: 'excel', title: 'payments' + 'FileExport',
                                exportOptions: {
                                    columns: [1, 2, 3, 4, 5, 6]
                                }
                            },
                            {
                                extend: 'pdf', title: 'payments' + 'FileExport',
                                exportOptions: {
                                    columns: [1, 2, 3, 4, 5, 6]
                                }
                            },

                            {
                                extend: 'print',
                                exportOptions: {
                                    columns: [1, 2, 3, 4, 5, 6]
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
                }
            ));
            }

            getLots();
        }
    ]);
})();