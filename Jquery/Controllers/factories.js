app.factory('getAllFreeDaysFactory', ['$http', function ($http) {
    return {
        get: function (url) {
            return $http({
                method: "GET",
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: "{}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);

app.factory('getAllBlockDaysFactory', ['$http', function ($http) {
    return {
        get: function (url) {
            return $http({
                method: "GET",
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: "{}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);


app.factory('insertEventFactory', ['$http', function ($http) {
    return {
        get: function (url, fullName, startAt, endAt, comment) {
            return $http({
                method: "POST",
                url: url,
                // + '","StartAt":"' + startAt +'","EndAt":"' + endAt + '","Comment":"' + comment +'"}'
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json',
                },
                data: "{'fullName':'" + fullName + "','StartAt':'" + startAt + "','EndAt':'" + endAt + "','Comment':'" + comment + "'}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);

app.factory('updateEventFactory', ['$http', function ($http) {
    return {
        get: function (url, id, startAt, endAt, comment) {
            return $http({
                method: "POST",
                url: url,
                // + '","StartAt":"' + startAt +'","EndAt":"' + endAt + '","Comment":"' + comment +'"}'
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json',
                },
                data: "{'ID':'" + id + "','StartAt':'" + startAt + "','EndAt':'" + endAt + "','Comment':'" + comment + "'}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);

app.factory('deleteEventFactory', ['$http', function ($http) {
    return {
        get: function (url, id, remover_id) {
            return $http({
                method: "POST",
                url: url,
                // + '","StartAt":"' + startAt +'","EndAt":"' + endAt + '","Comment":"' + comment +'"}'
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json',
                },
                data: "{'ID':'" + id + "','remover_id':'" + remover_id +"'}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);

app.factory('insertBlockEventFactory', ['$http', function ($http) {
    return {
        get: function (url, id, startAt, endAt, comment) {
            return $http({
                method: "POST",
                url: url,
                // + '","StartAt":"' + startAt +'","EndAt":"' + endAt + '","Comment":"' + comment +'"}'
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json',
                },
                data: "{'ID':'" + id + "','StartAt':'" + startAt + "','EndAt':'" + endAt + "','Comment':'" + comment + "'}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);

app.factory('getUserDetailsFactory', ['$http', function ($http) {
    return {
        get: function (url) {
            return $http({
                method: "GET",
                url: url,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'dataType': 'json'
                },
                data: "{}"
            }).then(function mySucces(response) {
                return response.data;
            });
        }
    };
} ]);