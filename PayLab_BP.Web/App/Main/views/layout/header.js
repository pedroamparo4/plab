(function () {
    var controllerId = 'app.views.layout.header';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$modal', '$state', 'appSession',
        function ($rootScope, $modal, $state, appSession) {
            var vm = this;

            vm.languages = abp.localization.languages;
            vm.currentLanguage = abp.localization.currentLanguage;

            vm.menu = abp.nav.menus.MainMenu;
            if ($state.current.menu == 'undefined') {
                vm.currentMenuName = 'Home';
            }
            else {
                vm.currentMenuName = $state.current.menu;
            }
            //vm.currentMenuName = $state.current.menu;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                vm.currentMenuName = toState.menu;
            });

            //VM Methods-------------------------------------------------------------------------------------------
            vm.openMyAccountModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/layout/myAccountModal.cshtml',
                    controller: 'app.views.layout.myAccountModal as vm',
                    backdrop: 'static'
                });
            };

            vm.getShownUserName = function () {
                if (!abp.multiTenancy.isEnabled) {
                    return appSession.user.userName;
                } else {
                    if (appSession.tenant) {
                        return appSession.tenant.tenancyName + '\\' + appSession.user.userName;
                    } else {
                        return '.\\' + appSession.user.userName;
                    }
                }
            };
            //VM Methods-------------------------------------------------------------------------------------------

            abp.event.on('abp.notifications.received', function (userNotification) {
                abp.notifications.showUiNotifyForUserNotification(userNotification);
            });
        }
    ]);
})();