var app = angular.module('mwl.calendar.docs', ['mwl.calendar', 'ngAnimate', 'ui.bootstrap']);

app.filter('toDate', function () {
    return function (dateString) {
        return moment(dateString).toDate()
    };
});



app.controller('KitchenSinkCtrl', ['$scope', '$http', '$timeout', '$window', 'moment', 'alert', 'getAllFreeDaysFactory', 'insertEventFactory', 'deleteEventFactory', 'updateEventFactory', 'getUserDetailsFactory', 'getAllBlockDaysFactory', 'insertBlockEventFactory',
                function ($scope, $http, $timeout, $window, moment, alert, getAllFreeDaysFactory, insertEventFactory, deleteEventFactory, updateEventFactory, getUserDetailsFactory, getAllBlockDaysFactory, insertBlockEventFactory) {

                    var vm = this;
                    $scope.checkDates = false;
                    $scope.checkID = false;
                    $scope.checkDatesOnPopUp = false;
                    $scope.dayBlocks = false;
                    $scope.dayBlocksOnPopUp = false;
                    $scope.checkBlockDates = false;
                    $scope.events = [];
                    $scope.eventsFreeDays = [];
                    $scope.dayBlocksArray = [];
                    $scope.eventsBlock = [];
                    $scope.userDetails;
                    $scope.setManager = false;
                    $scope.render;
                    $scope.eventUpdate;
                    $scope.showUpdate = false;
                    $scope.insertMessageBool = false;
                    $scope.insertMessage;
                    $scope.startAt;
                    $scope.endAt;
                    $scope.comment;
                    $scope.fullName;



                    $scope.setManager = function (event) {
                        switch (event.manager_name) {
                            case "תומר בניטה":
                                event.type = "inverse";
                                break;
                            case "אמיר אליהו":
                                event.type = "warning";
                                break;
                            case "דניאל אבידן":
                                event.type = "info";
                                break;
                            case "איגור גולוד":
                                event.type = "success";
                                break;
                            case "בני משה":
                                event.type = "special";
                                break;
                            default:
                                event.type = null;
                                break;
                        }
                    }


                    $scope.render = function () {
                        getUserDetailsFactory.get('default.aspx/getUserDetails').then(function (data) {
                            $scope.userDetails = data.d;
                            getAllFreeDaysFactory.get('default.aspx/getAllFreeDays').then(function (data) {
                                $scope.eventsFreeDays = data.d;
                                if ($scope.userDetails.manager === 'True') {
                                    angular.forEach($scope.eventsFreeDays, function (event) {
                                        event.deletable = true;
                                        event.editable = true;
                                        event.endOpen = false;
                                        event.startOpen = false;
                                        $scope.setManager(event);
                                    });
                                }
                                else {
                                    angular.forEach($scope.eventsFreeDays, function (event) {
                                        event.deletable = false;
                                        event.editable = false;
                                        event.endOpen = false;
                                        event.startOpen = false;
                                        $scope.setManager(event);
                                    });
                                }
                                $scope.events = $scope.eventsBlock.concat($scope.eventsFreeDays);

                            });

                            getAllFreeDaysFactory.get('default.aspx/getAllBlockDays').then(function (data) {
                                $scope.eventsBlock = data.d;
                                if ($scope.userDetails.admin === 'True') {
                                    angular.forEach($scope.eventsBlock, function (event) {
                                        event.deletable = true;
                                        event.editable = true;
                                        event.endOpen = false;
                                        event.startOpen = false;
                                        event.blockDay = true;
                                        event.type = "important";
                                    });
                                }
                                else {
                                    angular.forEach($scope.eventsBlock, function (event) {
                                        event.deletable = false;
                                        event.editable = false;
                                        event.endOpen = false;
                                        event.startOpen = false;
                                        event.blockDay = true;
                                        event.type = "important";
                                    });
                                }

                                $scope.events = $scope.eventsFreeDays.concat($scope.eventsBlock);
                            });


                        });
                    };
                    $scope.render();

                    $scope.cellModifier = function (cell) {
                        angular.forEach(cell.events, function (event) {
                            if (event.blockDay) {
                                //cell.label = "חסום";
                                cell.cssClass = 'odd-cell';
                            }
                        });
                    }

                    //These variables MUST be set as a minimum for the calendar to work
                    vm.calendarView = 'month';
                    vm.viewDate = new Date();






                    $scope.newEvent = [
      {
          title: "",
          type: "",
          startsAt: "",
          endsAt: "",
          draggable: true,
          resizable: true
      }];


                    vm.isCellOpen = true;

                    vm.eventClicked = function (event) {
                        //alert.show('Clicked', event);
                    };

                    vm.eventEdited = function (event) {
                        //alert.show('Edited', event);
                        $scope.eventUpdate = event;
                        $scope.showUpdate = true;
                    };

                    vm.eventDeleted = function (event) {
                        //        alert.show('Deleted', event);
                        if (event.blockDay) {
                            if ($window.confirm("האם אתה בטוח שברצונך למחוק חסימה זו? \n" + "שם העובד שחסם: " + event.title + "\nתחילת חסימה: " + event.startsAt + "\nסוף חסימה: " + event.endsAt + "\nסיבת החסימה: " + event.comment)) {
                                $scope.showUpdate = false;
                                if (event.blockDay) {
                                    deleteEventFactory.get('default.aspx/deleteBlockDay', event.id, $scope.userDetails.employee_id).then(function (data) {
                                        $scope.reloadEvents();
                                        //$window.location.reload();
                                    });
                                }
                                else {
                                    deleteEventFactory.get('default.aspx/deleteFreeDay', event.id, $scope.userDetails.employee_id).then(function (data) {
                                        $scope.reloadEvents();
                                        //$window.location.reload();
                                    });
                                }
                            }
                        }
                        else {
                            if ($window.confirm("האם אתה בטוח שברצונך למחוק חופש זה? \n" + "שם העובד: " + event.title + "\nתחילת חופש: " + event.startsAt + "\nסוף החופש: " + event.endsAt + "\nסיבת החופש: " + event.comment)) {
                                $scope.showUpdate = false;
                                if (event.blockDay) {
                                    deleteEventFactory.get('default.aspx/deleteBlockDay', event.id, $scope.userDetails.employee_id).then(function (data) {
                                        $scope.reloadEvents();
                                        //$window.location.reload();
                                    });
                                }
                                else {
                                    deleteEventFactory.get('default.aspx/deleteFreeDay', event.id, $scope.userDetails.employee_id).then(function (data) {
                                        $scope.reloadEvents();
                                        //$window.location.reload();
                                    });
                                }
                            }
                        }
                    };

                    vm.eventTimesChanged = function (event) {
                        alert.show('Dropped or resized', event);
                    };

                    vm.toggle = function ($event, field, event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        event[field] = !event[field];
                    };

                    $scope.toggleTry = function ($event, field, event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                        event[field] = !event[field];
                    };

                    $scope.addEvent = function (titleEvent, startsAtEvent, endsAtEvent) {
                        vm.events.push({ title: titleEvent, startsAt: startsAtEvent, endsAt: endsAtEvent });
                    };


                    $scope.insertFreeDayAlsoBlocks = function () {
                        angular.element('#error_manager').css('display', 'none');
                        var dateStart = new Date($scope.startAt);
                        dateStart.setHours(0);
                        dateStart.setMinutes(0);
                        dateStart.setSeconds(0);
                        dateStart.setMilliseconds(0);
                        dateStart = dateStart.toUTCString();
                        var dateEnd = new Date($scope.endAt);
                        dateEnd.setHours(23);
                        dateEnd.setMinutes(59);
                        dateEnd.setSeconds(59);
                        dateEnd.setMilliseconds(0);
                        dateEnd = dateEnd.toUTCString();
                        insertEventFactory.get('default.aspx/insertFreeDayAlsoBlocks', $scope.fullName, dateStart, dateEnd, $scope.comment).then(function (data) {
                            if (data.d == null) {
                                $scope.reloadEvents();
                                $scope.insertMessageBool = true;
                                $scope.insertMessage = "החופש הוזן בהצלחה";

                            }
                            else if (data.d[0].error_message == "Cant Get Fixed Name") {
                                $scope.insertMessageBool = true;
                                $scope.insertMessage = "לא נמצא עובד בעל שם זה";
                            }
                            else if (data.d[0].error_message == "Get Multiple Fixed Name") {
                                $scope.insertMessageBool = true;
                                $scope.insertMessage = "נמצא יותר מעובד אחד בעל אותו השם";
                            }
                            else {
                                $scope.dayBlocks = true;
                                $scope.dayBlocksArray = data.d;
                            }
                        });

                    };

                    $scope.insertEvent = function (fullName, startAt, endAt, comment) {
                        $scope.dayBlocksArray = [];
                        $scope.dayBlocks = false;
                        $scope.insertMessageBool = false;
                        fullName = fullName.replace("'", "\\'");
                        if (!comment) {
                            comment = " ";
                        }
                        if (startAt > endAt) {
                            $scope.checkDates = true;
                        }
                        else {
                            var dateStart = new Date(startAt);
                            dateStart.setHours(0);
                            dateStart.setMinutes(0);
                            dateStart.setSeconds(0);
                            dateStart.setMilliseconds(0);
                            dateStart = dateStart.toUTCString();
                            var dateEnd = new Date(endAt);
                            dateEnd.setHours(23);
                            dateEnd.setMinutes(59);
                            dateEnd.setSeconds(59);
                            dateEnd.setMilliseconds(0);
                            dateEnd = dateEnd.toUTCString();
                            insertEventFactory.get('default.aspx/insertFreeDays', fullName, dateStart, dateEnd, comment).then(function (data) {
                                if (data.d == null) {
                                    $scope.reloadEvents();
                                    $scope.insertMessageBool = true;
                                    $scope.insertMessage = "החופש הוזן בהצלחה";
                                }
                                else if (data.d[0].error_message == "Cant Get Fixed Name") {
                                    $scope.insertMessageBool = true;
                                    $scope.insertMessage = "לא נמצא עובד בעל שם זה";
                                }
                                else if (data.d[0].error_message == "Get Multiple Fixed Name") {
                                    $scope.insertMessageBool = true;
                                    $scope.insertMessage = "נמצא יותר מעובד אחד בעל אותו השם";
                                }
                                else {
                                    if ($scope.userDetails.admin === 'True') {
                                        $scope.dayBlocksArray = data.d;
                                        $scope.startAt = startAt;
                                        $scope.endAt = endAt;
                                        $scope.comment = comment;
                                        $scope.fullName = fullName;
                                        angular.element(document).find('#error_manager').css('display', 'block');
                                    }
                                    else {
                                        $scope.dayBlocks = true;
                                        $scope.dayBlocksArray = data.d;
                                    }
                                }
                            });
                        }
                    };

                    $scope.insertBlockEvent = function (startAt, endAt, comment) {
                        if (!comment) {
                            comment = " ";
                        }
                        if (startAt > endAt) {
                            $scope.checkBlockDates = true;
                        }
                        else {
                            var dateStart = new Date(startAt);
                            dateStart.setHours(0);
                            dateStart.setMinutes(0);
                            dateStart.setSeconds(0);
                            dateStart.setMilliseconds(0);
                            dateStart = dateStart.toUTCString();
                            var dateEnd = new Date(endAt);
                            dateEnd.setHours(23);
                            dateEnd.setMinutes(59);
                            dateEnd.setSeconds(59);
                            dateEnd.setMilliseconds(0);
                            dateEnd = dateEnd.toUTCString();
                            insertBlockEventFactory.get('default.aspx/insertBlockDays', $scope.userDetails.employee_id, dateStart, dateEnd, comment).then(function (data) {
                                if (data.d == null) {
                                    $scope.reloadEvents();
                                }
                            });
                        }
                    };

                    $scope.updateEvent = function (event) {
                        $scope.dayBlocksArray = [];
                        $scope.dayBlocksOnPopUp = false;
                        if (event.startsAt > event.endsAt) {
                            $scope.checkDatesOnPopUp = true;
                        }
                        else {
                            var dateStart = new Date(event.startsAt);
                            dateStart = dateStart.toUTCString();
                            var dateEnd = new Date(event.endsAt);
                            dateEnd = dateEnd.toUTCString();
                            if (event.blockDay) {
                                updateEventFactory.get('default.aspx/updateBlockDays', event.id, dateStart, dateEnd, event.comment).then(function (data) {
                                    $scope.reloadEvents();
                                    $scope.showUpdate = false;
                                });
                            }
                            else {
                                updateEventFactory.get('default.aspx/updateFreeDays', event.id, dateStart, dateEnd, event.comment).then(function (data) {
                                    if (data.d == null) {
                                        $scope.reloadEvents();
                                        $scope.showUpdate = false;
                                    }
                                    else {
                                        $scope.dayBlocksOnPopUp = true;
                                        $scope.dayBlocksArray = data.d;
                                    }
                                });
                            }
                        }
                    };

                    $scope.reloadEvents = function () {
                        $scope.events = [];
                        $scope.render();
                    };

                    // Disable weekend selection חסימת בחירת ימים
                    $scope.disabled = function (date, mode) {
                        //return mode === 'day' && (date.getDay() === 5 || date.getDay() === 6);
                    };

                } ]);


