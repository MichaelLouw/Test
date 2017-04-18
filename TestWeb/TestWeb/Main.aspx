<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TestWeb.Main" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                    <asp:Button CssClass="form-control" runat="server" Text="Add Project" data-toggle="modal" data-target="#ModalProject"/>
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
                <td><asp:Button CssClass="form-control" runat="server" OnClientClick="DeleteProject({{project.pk}}); return false;" Text="Delete"/></td>
                <td><asp:Button CssClass="form-control" runat="server" OnClientClick="SetUpdateProject({{project.pk}}); return false;" Text="Update" data-toggle="modal" data-target="#ModalUpdateProject"/></td>
                <td><asp:Button CssClass="form-control" runat="server" Text="Add Task" data-toggle="modal" data-taregt="#ModalTask" OnClientClick="setAddTaskInfo({{project.pk}}); return false;"/></td>
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
            <%--<div class="col-lg-3">
                <asp:Button CssClass="form-control" runat="server" Text="Add Task" data-toggle="modal" data-taregt="#ModalTask"></asp:Button>
            </div>--%>
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
                <td><asp:Button CssClass="form-control" runat="server" Text="Delete Task" OnClientClick="DeleteTask({{task.id}}); return false;"></asp:Button></td>
                <td><asp:Button CssClass="form-control" runat="server" Text="Update Task" OnClientClick="setUpdateTask({{task.id}}); return false;" data-target="#ModalUpdateTask" data-toggle="modal"></asp:Button></td>
            </tr>
        </table>
    </div>

        <%-- Update project modal --%>
        <div class="modal fade" id="ModalUpdateProject" role="dialog">
        <div class="modal-dialog modal-lg" style="max-height: 100%">
            <div class="modal-content">
                        <asp:HiddenField ID="UpdateProject" runat="server" />
                        <div class="modal-header">
                            <asp:Label runat="server" ID="Label2" Text="Create new proejct"></asp:Label>
                        </div>
                        <div class="modal-body" style="padding-bottom: 50px">
                              <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Title"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtUpdateTitle"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Description"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtUpdateDescription"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Start Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtUpdateStart_Date"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="CalendarExtender4" TargetControlID="txtUpdateStart_Date" />
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="End Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtUpdateEnd_Date"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="CalendarExtender5" TargetControlID="txtUpdateEnd_Date" />
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Is Billable"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList runat="server" ID="ddlIs_Billable"></asp:DropDownList>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Is Active"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList runat="server" ID="ddlIs_Active"></asp:DropDownList>
                                </div>
                            </div>                  
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Button runat="server" OnClientClick="UpdateProject(); return false;"  Text="Save Project"/>
                                </div>
                            </div>
                        </div>
            </div>
        </div>
    </div>

        <%-- create project modal --%>
        <div class="modal fade" id="ModalProject" role="dialog">
        <div class="modal-dialog modal-lg" style="max-height: 100%">
            <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label runat="server" ID="projectHeader" Text="Create new project"></asp:Label>
                        </div>
                        <div class="modal-body" style="padding-bottom: 50px">
                              <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Title"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Description"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Start Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtStart_date"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="calEstimatedCloseDate" TargetControlID="txtStart_date" />
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="End Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox runat="server" ID="txtEnd_Date"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="CalendarExtender1" TargetControlID="txtEnd_Date" />
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Is Billable"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList runat="server" ID="txtIs_Billable"></asp:DropDownList>
                                </div>
                            </div> 
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Is Active"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList runat="server" ID="txtIs_Active"></asp:DropDownList>
                                </div>
                            </div>                  
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Button runat="server" OnClientClick="AddProject(); return false;"  Text="Save Project"/>
                                </div>
                            </div>
                        </div>
            </div>
        </div>
    </div>

        <%-- update task modal --%>
        <div class="modal fade" id="ModalUpdateTask" role="dialog">
        <div class="modal-dialog modal-lg" style="max-height: 100%">
            <div class="modal-content">
                        <asp:HiddenField runat="server" ID="UpdateTaskID" />
                        <div class="modal-header">
                            <asp:Label runat="server" ID="Label1" Text="Create Task"></asp:Label>
                        </div>
                        <div class="modal-body" style="padding-bottom: 50px">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Title"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Description"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Due Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="CalendarExtender3" TargetControlID="txtDue_Date" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Estimated Hours"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TextBox4" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Projects"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList ID="DropDownList1" runat="server" ></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Button runat="server" OnClientClick="UpdateTask(); return false;" Text="Save Task" />
                                </div>
                            </div>
                        </div>
            </div>
        </div>
    </div>

        <%-- create task modal --%>
        <div class="modal fade" id="ModalTask" role="dialog">
        <div class="modal-dialog modal-lg" style="max-height: 100%">
            <div class="modal-content">
                        <asp:HiddenField runat="server" ID="TaskProjectID" />
                        <div class="modal-header">
                            <asp:Label runat="server" ID="taskHeader" Text="Create Task"></asp:Label>
                        </div>
                        <div class="modal-body" style="padding-bottom: 50px">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Title"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtTaskTitle" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Description"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtTaskDescription" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Due Date"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtDue_Date" runat="server" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" runat="server" ID="CalendarExtender2" TargetControlID="txtDue_Date" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Estimated Hours"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtEstimated_Hours" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Text="Projects"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:DropDownList ID="txtProject" runat="server" ></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Button runat="server" OnClientClick="AddTask(); return false;" Text="Save Task" />
                                </div>
                            </div>
                        </div>
            </div>
        </div>
    </div>

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

            $.ajax({
                method: "GET",
                url: url,
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    $scope.projects = data;
                },
                error: function (data) {
                    alert("Erro get projects");
                }
            })
        })

        app.controller("taskController", function ($scope, $http) {
            var url = $("#<%=TasksURL.ClientID%>").val();

            $.ajax({
                method: "GET",
                url: url,
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                success: function (data) {
                    $scope.tasks = data;
                },
                error: function (data) {
                    alert("Erro get tasks");
                }
            })
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
                window.location.replace("Login.aspx");
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
        function SetUpdateProject(pk) {
            $("#<%=UpdateProject.ClientID%>").val(pk);

            //open and populate modal for project.

        }

        function AddProject() {

            //post to api
            $.ajax({
                method: "POST",
                url: $("#<%=ProjectsURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": $("#<%=txtTitle.ClientID%>").val(), "description": $("#<%=txtDescription.ClientID%>").val(), "start_date": $("#<%=txtStart_date.ClientID%>").val(), "end_date": $("#<%=txtEnd_Date.ClientID%>").val(), "is_Billable": $("#<%=txtIs_Billable.ClientID%> option:selected").val(), "is_Active": $("#<%=txtIs_Active.ClientID%> option:selected").val() }),
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

        function DeleteProject(pk) {
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

        function UpdateProject() {
            //patch to api
            $.ajax({
                method: "PATCH",
                url: $("#<%=ProjectsURL.ClientID%>").val() + $("#<%=UpdateProject.ClientID%>").val() + "/",
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": $("#<%=txtTitle.ClientID%>").val(), "description": $("#<%=txtDescription.ClientID%>").val(), "start_date": $("#<%=txtStart_date.ClientID%>").val(), "end_date": $("#<%=txtEnd_Date.ClientID%>").val(), "is_Billable": $("#<%=txtIs_Billable.ClientID%> option:selected").val(), "is_Active": $("#<%=txtIs_Active.ClientID%> option:selected").val() }),
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
        function setAddTaskInfo(project) {
            $("#<%=TaskProjectID.ClientID%>").val(project);
        }

        function setUpdateTask(task) {
            $("#<%=UpdateTaskID.ClientID%>").val(task);

            //open and populate modal for task.
        }

        function AddTask() {
            //post to api
            $.ajax({
                method: "POST",
                url: $("#<%=TasksURL.ClientID%>").val(),
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "title": $("#<%=txtTaskTitle.ClientID%>").val(), "description": $("#<%=txtTaskDescription.ClientID%>").val(), "due_date": $("#<%=txtDue_Date.ClientID%>").val(), "estimated_hours": $("#<%=txtEstimated_Hours.ClientID%>").val(), "project": $("#<%=TaskProjectID.ClientID%>").val() }),
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

        function DeleteTask(id) {
            //delete to api
            $.ajax({
                method: "DELETE",
                url: $("#<%=TasksURL.ClientID%>").val() + id + "/",
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
                url: $("#<%=ProjectsURL.ClientID%>").val() + id + "/",
                headers: {
                    "Authorization": sessionStorage.getItem("Token")
                },
                contentType: "application/json",
                data: JSON.stringify({ "id": $("#<%=UpdateTaskID.ClientID%>").val(), "title": $("#<%=txtTaskTitle.ClientID%>").val(), "description": $("#<%=txtTaskDescription.ClientID%>").val(), "due_date": $("#<%=txtDue_Date.ClientID%>").val(), "estimated_hours": $("#<%=txtEstimated_Hours.ClientID%>").val()}),
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
