(function() {
    var controllerId = 'app.views.home';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.payment', '$filter', function ($scope, paymentService, $filter) {
            var vm = this; 
        
            function loadPayments() {

                abp.ui.setBusy(
                null,
                paymentService.getTodayPayments({}).success(function (result) {

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

                    //$('#totalPayments').empty().append($filter('currency')(totalPayments, '$'));
                   // $('#quantityPayments').empty().append(payments.length);

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
                        responsive: true

                    });
                }
            ));
            }

            loadPayments();
        }
    ]);
})();