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
    $.ajax({
      method: "GET",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.substr(1) + "/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        jQuery(document).ready(function(){
          console.log(data);
          var task = new app.task(data);
          sessionStorage.setItem("task", JSON.stringify(data));
          console.log(task);
          var collectionTask = new app.Tasks([task]);
          var updatetask = new app.UpdateTask({
            model: collectionTask
          });
          $("#mainContainer").html(updatetask.render().el);
        });
      },
      error: function(data){
        console.log(data);
      }
    });
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
        window.location.replace("main.html");
        $.notify("Project Deleted", "success");
      },
      error: function(data){
        //handle error.
        console.log(data);
        $.notify("Error deleting task");
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
    $.ajax({
      method: "DELETE",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.substr(1) + "/",
      headers: {
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      success: function(data){
        //notify the user and reload the views.
        window.location.replace("main.html");
        $.notify("Project Deleted");
      },
      error: function(data){
        //handle error.
        console.log(data);
        $.notify("Error deleting project");
      }
    });
  },

  UpdateProject: function(project){
      sessionStorage.setItem("project", project.substr(1));

      $.ajax({
        method: "GET",
        url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.substr(1) + "/",
        headers: {
          "Authorization": "Token " + sessionStorage.getItem("Token")
        },
        success: function(data){
          var updateproject = new app.UpdateProject({
            collection: data
          });
          $("#mainContainer").html(updateproject.render().el);
        },
        error: function(data){
          console.log(data);
        }
      });
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
