<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TestWeb.Main" MasterPageFile="~/Site.Master" %>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server" ng-app="">

    <%-- Angular libraries --%>
    <script src="Scripts/Angular/angular.js"></script>
    <script src="Scripts/Angular/angular-route.js"></script>

    <%-- define app content --%>
    <asp:HiddenField runat="server" ID="ProjectsURL" />
    <asp:HiddenField runat="server" ID="TasksURL" />
    <div class="row">
        <div class="row" style="background-color: orange">
            <asp:Label runat="server" Text="Projects" Font-Bold="true" Font-Size="X-Large"></asp:Label>
        </div>

        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-3">
                    <asp:Button CssClass="form-control" runat="server" OnClientClick="RefreshTable(); return false;" Text="Refresh" />
                </div>
                <div class="col-lg-3">
                    <asp:Button CssClass="form-control" runat="server" OnClientClick="AddProduct(); return false;" Text="Add Product" />
                </div>
                <div class="col-lg-3">
                    <asp:Button CssClass="form-control" runat="server" OnClientClick="DeleteProduct(); return false;" Text="Delete" />
                </div>
                <div class="col-lg-3">
                    <asp:Button CssClass="form-control" runat="server" OnClientClick="UpdateProduct(); return false;" Text="Update" />
                </div>
            </div>
        </div>
    <%-- projects table init --%>
    <div class="row" ng-app="app" ng-controller="projectController">
        <%-- binding for devExpress tools --%>
        <div id="ProjectsTable"></div>

        <%-- binding for angular table --%>
        <table class="table table-responsive" id="projectsTable">
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Is Billable</th>
                <th>Is Active</th>
            </tr>
            <tr ng-repeat="project in projects">
                <td>{{project.title}}</td>
                <td>{{project.description}}</td>
                <td>{{project.start_date}}</td>
                <td>{{project.end_date}}</td>
                <td>{{project.is_Billable}}</td>
                <td>{{project.is_Active}}</td>
            </tr>
        </table>
    </div>
    <div class="row">
        <div class="row" style="background-color: orange">
            <asp:Label runat="server" Text="Tasks" Font-Bold="true" Font-Size="X-Large"></asp:Label>
        </div>

        <div class="col-lg-12">
            <div class="col-lg-3">
                <asp:Label runat="server" Text="Select Project First"></asp:Label>
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" Text="Add Task" OnClientClick="AddTask(); return false;"></asp:Button>
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" Text="Delete Task" OnClientClick="DeleteTask(); return false;"></asp:Button>
            </div>
            <div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" Text="Update Task" OnClientClick="UpdateTask(); return false;"></asp:Button>
            </div>
        </div>
    </div>
    <div class="row" ng-app="" ng-controller="taskController">
        <%-- binding for devExpress tools --%>
        <div id="TaskTable"></div>

        <%-- binding for angular table --%>
        <table class="table table-responsive" id="tasksTable">
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Due Date</th>
                <th>Estimated Hours</th>
            </tr>
            <tr ng-repeat="task in tasks">
                <td>{{task.title}}</td>
                <td>{{task.description}}</td>
                <td>{{task.due_date}}</td>
                <td>{{task.estimated_hours}}</td>
            </tr>
        </table>
    </div>

        <%-- create project modal --%>


        <%-- create task modal --%>


    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/respond.js"></script>
    <script src= "Scripts/dx.all.js" ></script>
    <script src="Scripts/datajs-1.1.2.min.js"></script>
        <script src="Scripts/jquery-notify.js"></script>
        <script src="Scripts/bootstrap-notify.js"></script>
    <script>
        //angular processing.
        var app = angular.module("app", []);

        app.controller("projectController", function ($scope, $http) {
            var url = $("#<%=ProjectsURL.ClientID%>").val();
            $http.get(url).success(function (response) {
                $scope.projects = response;
            }).error(function (response) {
                console.log(response);
            });
        })

        app.controller("taskController", function ($scope, $http) {
            var url = $("#<%=TasksURL.ClientID%>").val();
            $http.get(url).success(function (response) {
                $scope.tasks = response;
            }).error(function (response) {
                console.log(response);
            });
        })

        //jquery processing and devexpress tools.
        $(document).ready(function () {
            if (sessionStorage.getItem("Token") != null) {
                //token found
                LoadProjects();
                LoadTasks();
            }
            else {
                //no token login again.
                //window.location.replace("Login.aspx");
            }
        });

        function LoadProjects() {
            window.location.reload(true);
            //GET
           <%-- $.ajax({
                method: "GET",
                url: $("#<%=ProjectsURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    //get data and append table.

                    var tableinative = $("#projectsTable tbody").eq(0).clone(true)
                    $("#projectsTable tbody").eq(0).remove()
                    for (var i = 0; i < 1; i++) {
                        var row = $("<tr id=" + "" + ">");
                        $("#projectsTable").append(row);
                        var column = $("</thead>");
                        $("#projectsTable").append(column);
                        column.append("<th style=width: 20%>" + "Title" + "</th>");
                        column.append("<th style=width: 20%>" + "Description" + "</th>");
                        column.append("<th style=width: 20%>" + "Start Date" + "</th>");
                        column.append("<th style=width: 20%>" + "End Date" + "</th>");
                        column.append("<th style=width: 10%>" + "Is Billable" + "</th>");
                        column.append("<th style=width: 10%>" + "Is Active" + "</th>");
                        row.append("<td style=width: 20%>" + "" + "</td>");
                        row.append("<td style=width: 20%>" + "" + "</td>");
                        row.append("<td style=width: 20%>" + "" + "</td>");
                        row.append("<td style=width: 20%>" + "" + "</td>");
                        row.append("<td style=width: 10%>" + "" + "</td>");
                        row.append("<td style=width: 10%>" + "" + "</td>");
                    }
                },
                error: function (data) {

                }
            })   --%>       


            //Can populate table using DevExpress Tools.
            <%--$("#ProjectsTable").dxdatagrid({
                dataSource: {
                    store: {
                        type: "odata",
                        url: $("#<%=ProjectsURL.ClientID%>").val(),
                        headers: {
                            "Authorization": "Token" + sessionStorage.getItem("Token")
                        }
                    }
                },
                columns: [{
                    dataField: "pk",
                    visible: false 
                }, {
                        dataField: "title",
                        caption: "Title",
                    }, {
                        dataField: "description",
                        caption: "Description"
                }, {
                        dataField: "start_Date",
                        caption: "Start Date"
                    }, {
                        dataField: "end_date",
                        caption: "End Date"
                }, {
                        dataField: "is_Billable",
                        caption: "Billable"
                    }, {
                        dataField: "is_Active",
                        caption: "Active"
                }],
                onRowClick: function (e) {
                    //open modal to view project details.
                }
            })--%>
        }

        function LoadTasks() {
            window.location.reload(true);
            //GET
            <%--$.ajax({
                method: "GET",
                url: $("#<%=TasksURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    //get data and append table.

                    var tableinative = $("#tasksTable tbody").eq(0).clone(true)
                    $("#tasksTable tbody").eq(0).remove()
                    for (var i = 0; i < 1; i++) {
                        var row = $("<tr id=" + "" + ">");
                        $("#tasksTable").append(row);
                        var column = $("</thead>");
                        $("#tasksTable").append(column);
                        column.append("<th style=width: 25%>" + "Title" + "</th>");
                        column.append("<th style=width: 25%>" + "Description" + "</th>");
                        column.append("<th style=width: 25%>" + "Due Date" + "</th>");
                        column.append("<th style=width: 25%>" + "Estimated Hours" + "</th>");
                        row.append("<td style=width: 25%>" + "" + "</td>");
                        row.append("<td style=width: 25%>" + "" + "</td>");
                        row.append("<td style=width: 25%>" + "" + "</td>");
                        row.append("<td style=width: 25%>" + "" + "</td>");
                    }
                },
                error: function (data) {

                }
            })--%>  


            //Can populate table using DevExpress Tools.
            <%--$("#ProjectsTable").dxdatagrid({
                dataSource: {
                    store: {
                        type: "odata",
                        url: $("#<%=TasksURL.ClientID%>").val(),
                        headers: {
                            "Authorization": "Token" + sessionStorage.getItem("Token")
                        }
                    }
                },
                columns: [{
                    dataField: "id",
                    visible: false
                }, {
                    dataField: "title",
                    caption: "Title",
                }, {
                    dataField: "description",
                    caption: "Description"
                }, {
                    dataField: "due_date",
                    caption: "Due Date"
                }, {
                    dataField: "estimated_hours",
                    caption: "Estimated Hours"
                }, {
                    dataField: "project",
                    visible: false
                }],
                onRowClick: function (e) {
                    //open modal to view task details.
                }
            })--%>
        }

        function RefreshTable() {
            LoadProjects();
            LoadTasks();
        }

        //project functions

        function AddProject() {

            //post to api
            $.ajax({
                method: "POST",
                url: $("#<%=ProjectsURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": "", "description": "", "start_date": "", "end_date": "", "is_Billable": "", "is_Active": "" }),
                processData: true,
                success: function (data) {
                    $.notify(NotifyOptions.success, "Project added");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed adding project");
                }
            })
        }

        function DeleteProduct() {
            var pk = "";
            //delete to api
            $.ajax({
                method: "DELETE",
                url: $("#<%=ProjectsURL.ClientID%>").val() + pk + "/",
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    $.notify(NotifyOptions.success, "Project deleted");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed deleting project");
                }
            })
        }

        function UpdateProduct() {
            //patch to api
            $.ajax({
                method: "PATCH",
                url: $("#<%=ProjectsURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": "", "description": "", "start_date": "", "end_date": "", "is_Billable": "", "is_Active": "" }),
                processData: true,
                success: function (data) {
                    $.notify(NotifyOptions.success, "Project updated");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed updating project");
                }
            })
        }

        //task functions

        function AddTask() {
            //post to api
            $.ajax({
                method: "POST",
                url: $("#<%=TasksURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": "", "description": "", "start_date": "", "end_date": "", "is_Billable": "", "is_Active": "" }),
                processData: true,
                success: function (data) {
                    $.notify(NotifyOptions.success, "Task added");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed adding task");
                }
            })
        }

        function DeleteTask() {
            var pk = "";
            //delete to api
            $.ajax({
                method: "DELETE",
                url: $("#<%=TasksURL.ClientID%>").val() + pk + "/",
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    $.notify(NotifyOptions.success, "Task deleted");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed deleting task");
                }
            })
        }

        function UpdateTask() {
            //patch to api
            $.ajax({
                method: "PATCH",
                url: $("#<%=ProjectsURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": "", "description": "", "start_date": "", "end_date": "", "is_Billable": "", "is_Active": "" }),
                processData: true,
                success: function (data) {
                    $.notify(NotifyOptions.success, "Task updated");
                    RefreshTable();
                },
                error: function (data) {
                    $.notify(NotifyOptions.error, "Failed updating task");
                }
            })
        }

    </script>
</asp:Content>
