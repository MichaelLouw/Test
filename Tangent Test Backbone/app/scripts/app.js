var app = app || {};

var AppRouter = Backbone.Router.extend({
  routes:{
    "": "LoadAll",
    "login": "login",
    "projectaddproject": "AddProject",
    "projectAddTask/:project": "AddTask",
    "projectDelete/:project": "DeleteProject",
    "projectUpdate/:project": "UpdateProject",
    "taskUpdate/:task": "UpdateTask",
    "taskDelete/:task": "DeleteTask"
  },

  AddProject: function(){
    var addnewproject = new app.newProject({});
    $("#mainContainer").html(addnewproject.render().el);
  },

  UpdateTask: function(task){
    sessionStorage.setItem("task", task);
    var updatetask = new app.UpdateTask({});
    $("#mainContainer").html(updatetask.render().el);
  },

  DeleteTask: function(task){
    $.ajax({
      method: "DELETE",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.substr(1) +"/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        //notify the user and reload the views.
        $.notify("Project Deleted", "success");
        window.location.replace("main.html");
      },
      error: function(data){
        //handle error.
      }
    });
  },

  AddTask: function(project){
    sessionStorage.setItem("projectid", project);
    var addtasktoproject = new app.AddTask({});
    $("#mainContainer").html(addtasktoproject.render().el);
  },

  DeleteProject: function(project){
    //delete the project
    alert(project.substr(1));
    $.ajax({
      method: "DELETE",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.substr(1) + "/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        //notify the user and reload the views.
        $.notify("Project Deleted");
        window.location.replace("main.html");
      },
      error: function(data){
        //handle error.
        console.log(data);
      }
    });
  },

  UpdateProject: function(project){
      alert(JSON.stringify(project));
      sessionStorage.setItem("project", project);
      var updateproject = new app.UpdateProject({});
      $("#mainContainer").html(updateproject.render().el);
  },

  LoadAll: function(){
    //get all data from projects and tasks.
    $.ajax({
      method: "GET",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        var projects = new app.project(data);
        sessionStorage.setItem("Projects", JSON.stringify(projects["attributes"]));
        // console.log(Object.keys(projects).length);
        // for (var i = 0; i < Object.keys(projects).length; i++){
        //   console.log(projects["attributes"][0]["pk"]);
        //   for (var name in projects){
        //     console.log(JSON.stringify(name));
        //   }
        // }
        var viewProjects = new app.ProjectView({
           collection: projects
        });
        $("#project").html(viewProjects.render().el);
        console.log(data);
      },
      error: function(data){
        //handle error.
        console.log(data);
      }
    });

    $.ajax({
      method: "GET",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        var tasks = new app.task(data);
        sessionStorage.setItem("Tasks", JSON.stringify(tasks["attributes"]));
        console.log(tasks["attributes"]);
        var viewTasks = new app.TaskView({
          collection: tasks
        });
        $("#task").html(viewTasks.render().el);
      },
      error: function(data){
        //handle error.
        console.log(data);
      }
    });
  },

  login: function(){
    window.LoginView = new LoginView({model: new login()});
  }
});

jQuery(document).ready(function(){
  var app = new AppRouter();

  $(function(){
    Backbone.history.start();
  })
});
