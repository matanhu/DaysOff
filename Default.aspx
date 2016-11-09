<%@ Page Language="C#" MasterPageFile="~/master_top.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div dir="rtl" ng-app="mwl.calendar.docs" style="margin:20px;">
    <div id="header">
 <img src="images/top_freeDays.gif" style="display: block; margin-left:auto; margin-right:auto;"/>
</div>
        <div ng-controller="KitchenSinkCtrl as vm" id="container">
            <div class="row">
                <div class="col-md-8 text-center">
                    <div class="header">
                        <h2 class="text-center">
                            {{ vm.calendarTitle }}</h2>
                        <div class="row">
                            <div class="col-md-6 text-center">
                                <div class="btn-group">
                                    <button class="btn btn-primary" mwl-date-modifier date="vm.viewDate" decrement="vm.calendarView">
                                        קודם
                                    </button>
                                    <button class="btn btn-default" mwl-date-modifier date="vm.viewDate" set-to-today>
                                        היום
                                    </button>
                                    <button class="btn btn-primary" mwl-date-modifier date="vm.viewDate" increment="vm.calendarView">
                                        הבא
                                    </button>
                                </div>
                            </div>
                            <br class="visible-xs visible-sm">
                            <div class="col-md-6 text-center">
                                <div class="btn-group">
                                    <label class="btn btn-primary" ng-model="vm.calendarView" uib-btn-radio="'year'">
                                        שנה</label>
                                    <label class="btn btn-primary" ng-model="vm.calendarView" uib-btn-radio="'month'">
                                        חודש</label>
                                    <label class="btn btn-primary" ng-model="vm.calendarView" uib-btn-radio="'week'">
                                        שבוע</label>
                                    <label class="btn btn-primary" ng-model="vm.calendarView" uib-btn-radio="'day'">
                                        יום</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <mwl-calendar events="events" view="vm.calendarView" view-title="vm.calendarTitle"
                        view-date="vm.viewDate" on-event-click="vm.eventClicked(calendarEvent)" on-event-times-changed="vm.eventTimesChanged(calendarEvent); calendarEvent.startsAt = calendarNewEventStart; calendarEvent.endsAt = calendarNewEventEnd"
                        edit-event-html="'<i class=\'glyphicon glyphicon-pencil\'></i>'" delete-event-html="'<i class=\'glyphicon glyphicon-remove\'></i>'"
                        on-edit-event-click="vm.eventEdited(calendarEvent)" on-delete-event-click="vm.eventDeleted(calendarEvent)"
                        cell-is-open="vm.isCellOpen" day-view-start="00:00" day-view-end="23:59" day-view-split="30"
                        cell-modifier="cellModifier(calendarCell)">
  </mwl-calendar>
                    <div ng-show="showUpdate" style="margin-top: 10px;">
                        <table class="table table-bordered">
                            <thead style="direction: rtl; text-align: right;">
                                <tr style="direction: rtl; text-align: right;">
                                    <th style="direction: rtl; text-align: right;">
                                        ת.ז. עובד
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        תחילת חופש
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        סוף חופש
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        הערות
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="event in newEvent track by $index">
                                    <td>
                                        <input type="text" class="form-control" placeholder="ת.ז." readonly ng-model="eventUpdate.title">
                                        <input type="text" class="form-control" placeholder="ת.ז." readonly ng-model="eventUpdate.employee_id">
                                    </td>
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="eventUpdate.startsAt" is-open="eventUpdate.startOpen" max-date="eventUpdate.endsAt"
                                                placeholder="תאריך התחלה" date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'startOpen', eventUpdate)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                 <%--       <p style="direction: ltr;">
                                            <uib-timepicker ng-model="eventUpdate.startsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="eventUpdate.endsAt" is-open="eventUpdate.endOpen" min-date="eventUpdate.startsAt"
                                                placeholder="תאריך סוף" date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'endOpen', eventUpdate)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                        <%--<p style="direction: ltr;">
                                            <uib-timepicker ng-model="eventUpdate.endsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <textarea type="text" class="form-control" placeholder="הערות" ng-model="eventUpdate.comment"></textarea>
                                        <button class="btn btn-danger" ng-click="updateEvent(eventUpdate)" style="margin-top: 10px;">
                                            שמירה
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="checkDatesOnPopUp">
                            * הוכנסו תאריכים לא תקינים
                        </div>
                        <div style="color: #ff0000; font-weight: bold; margin-bottom: 20px; float: right;
                            margin-right: 20px;" ng-show="dayBlocksOnPopUp">
                            קיימים תאריכים חסומים
                            <div ng-repeat="dayBlock in dayBlocksArray">
                                בין התאריכים {{dayBlock.startsAt | toDate | date}} ל {{dayBlock.endsAt | toDate
                                | date}} נחסם ע"י {{dayBlock.employee_name}}, סיבת החסימה: {{dayBlock.comment}}.
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-md-4 text-center">
                    <%--<h3 id="event-editor">
    <button
      class="btn btn-primary pull-right"
      ng-click="newEvent.push({title: 'בקשת חופשה חדשה', type: 'important', draggable: true, resizable: true})">
      הוספת בקשה
    </button>
    <div class="clearfix"></div>
  </h3>--%>
                    <div class="add-days" ng-show="userDetails.manager == 'True'">
                        <table class="table table-bordered" style="margin-top: 10px;">
                            <thead style="direction: rtl; text-align: right;">
                                <tr style="direction: rtl; text-align: right;">
                                    <th style="direction: rtl; text-align: right;">
                                        שם העובד
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        תחילת חופש
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        סוף חופש
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        הערות
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="event in newEvent track by $index">
                                    <td>
                                        <input type="text" class="form-control" placeholder="שם העובד" ng-model="event.fullName">
                                    </td>
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="event.startsAt" is-open="event.startOpen" max-date="event.endsAt" placeholder="תאריך התחלה"
                                                date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'startOpen', event)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                        <%--<p style="direction: ltr;">
                                            <uib-timepicker ng-model="event.startsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="event.endsAt" is-open="event.endOpen" min-date="event.startsAt" placeholder="תאריך סוף"
                                                date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'endOpen', event)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                       <%-- <p style="direction: ltr;">
                                            <uib-timepicker ng-model="event.endsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <textarea type="text" class="form-control" placeholder="הערות" ng-model="event.comment"  rows="4"></textarea>
                                        <button class="btn btn-danger" ng-click="insertEvent(event.fullName, event.startsAt, event.endsAt, event.comment)"
                                            style="margin-top: 10px;">
                                            שמירה
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="checkDates">
                            * הוכנסו תאריכים לא תקינים
                        </div>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="checkID">
                            * ת.ז. לא תקינה
                        </div>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="dayBlocks">
                            קיימים תאריכים חסומים
                            <div ng-repeat="dayBlock in dayBlocksArray">
                                בין התאריכים {{dayBlock.startsAt | toDate | date}} ל {{dayBlock.endsAt | toDate
                                | date}} נחסם ע"י {{dayBlock.employee_name}}, סיבת החסימה: {{dayBlock.comment}}.
                            </div>
                        </div>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="insertMessageBool">
                            <div>
                                {{insertMessage}}
                            </div>
                        </div>
                    </div>
                    <div class="add-days" ng-show="userDetails.admin == 'True'">
                        <table class="table table-bordered" style="margin-top: 10px;">
                            <thead style="direction: rtl; text-align: right;">
                                <tr style="direction: rtl; text-align: right;">
                                    <th style="direction: rtl; text-align: right;">
                                        תחילת חסימה
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        סוף חסימה
                                    </th>
                                    <th style="direction: rtl; text-align: right;">
                                        הערות
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="event in newEvent track by $index">
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="event.blockStartsAt" is-open="event.blockStartOpen" max-date="event.blockEndsAt"
                                                placeholder="תאריך התחלה" date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'blockStartOpen', event)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                        <%--<p style="direction: ltr;">
                                            <uib-timepicker ng-model="event.blockStartsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <p class="input-group" style="max-width: 250px">
                                            <input type="text" class="form-control" readonly uib-datepicker-popup="dd MMMM yyyy"
                                                ng-model="event.blockEndsAt" is-open="event.blockEndOpen" min-date="event.blockStartsAt"
                                                placeholder="תאריך סוף" date-disabled="disabled(date, mode)" ng-click="vm.toggle($event, 'blockEndOpen', event)"
                                                close-text="Close">
                                        </p>
<%--לטובת הוספת שעות ודקות--%>
                                        <%--<p style="direction: ltr;">
                                            <uib-timepicker ng-model="event.blockEndsAt" hour-step="1" minute-step="15" show-meridian="false">
          </uib-timepicker>
                                        </p>--%>
                                    </td>
                                    <td>
                                        <textarea type="text" class="form-control" placeholder="הערות" ng-model="event.blockComment"></textarea>
                                        <button class="btn btn-danger" ng-click="insertBlockEvent(event.blockStartsAt, event.blockEndsAt, event.blockComment)"
                                            style="margin-top: 10px;">
                                            שמירה
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="color: #ff0000; font-weight: bold;" ng-show="checkBlockDates">
                            * הוכנסו תאריכים לא תקינים
                        </div>
                    </div>
                </div>
            </div>
        



        

<div id="error_manager" class="w3-modal" style="display:none">
  <div class="w3-modal-content w3-card-8 w3-animate-zoom" style="max-width:600px">
  
    <div class="w3-center" style="z-index: 999"><br>
      <span onclick="document.getElementById('error_manager').style.display='none'" class="w3-closebtn w3-hover-red w3-container w3-padding-8 w3-display-topright" title="Close Modal">&times;</span>
      
      <div style="color: #ff0000; font-weight: bold;">
                            קיימים תאריכים חסומים
                            <div ng-repeat="dayBlock in dayBlocksArray">
                                בין התאריכים {{dayBlock.startsAt | toDate | date}} ל {{dayBlock.endsAt | toDate
                                | date}} נחסם ע"י {{dayBlock.employee_name}}, סיבת החסימה: {{dayBlock.comment}}.
                            </div>
                        </div>
                        <img src="images/seo-mistakes.jpg" alt="Avatar" style="width:30%" class="w3-circle w3-margin-top">
                        <div style="color: #ff0000; font-weight: bold;">
                            האם לאשר את החופש?
                        </div>
    </div>

    

    <div class="w3-container w3-border-top w3-padding-16 w3-light-grey">
      <button ng-click="insertFreeDayAlsoBlocks()" type="button" class="w3-btn w3-red" style="float:right">אישור</button>
      <button onclick="document.getElementById('error_manager').style.display='none'" type="button" class="w3-btn w3-red" style="float:left">ביטול</button>
    </div>
    </div>
    



    </div>
</div>
</asp:Content>
