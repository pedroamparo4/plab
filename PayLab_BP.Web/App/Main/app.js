(function () {
    'use strict';
    
    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in PayLab_BPNavigationProvider
                    });
            }

            if (abp.auth.hasPermission('Logs')) {
                $stateProvider
                    .state('logs', {
                        url: '/logs',
                        templateUrl: '/App/Main/views/logs/index.cshtml',
                        menu: 'Logs' //Matches to name of 'Logs' menu in PayLab_BPNavigationProvider
                    });
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in PayLab_BPNavigationProvider
                    });
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in PayLab_BPNavigationProvider
                })
                .state('lots', {
                    url: '/lots',
                    templateUrl: '/App/Main/views/lots/index.cshtml',
                    menu: 'Lots' //Matches to name of 'About' menu in PayLab_BPNavigationProvider
                });

           // $urlRouterProvider.otherwise('/home');

        }
    ]);
})();