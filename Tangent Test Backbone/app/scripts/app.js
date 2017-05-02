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
    var taskupdate = new app.task({id:  task.substr(1) + "/" });

    taskupdate.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
      console.log(data);
      tskData = data;
      var updatetask = new app.UpdateTask({
        collection: tskData
      });
      $("#mainContainer").html(updatetask.render().el);
    }, function(error){
      $.notify("erro occured", "error");
    });
    // $.ajax({
    //   method: "GET",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.substr(1) + "/",
    //   headers: {
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   success: function(data){
    //       console.log(data);
    //       var task = new app.task(data);
    //       sessionStorage.setItem("task", JSON.stringify(data));
    //       console.log(task);
    //       var collectionTask = new app.Tasks([task]);
    //       var updatetask = new app.UpdateTask({
    //         model: collectionTask
    //       });
    //       $("#mainContainer").html(updatetask.render().el);
    //   },
    //   error: function(data){
    //     console.log(data);
    //   }
    // });
  },

  DeleteTask: function(task){
    var deletetask = new app.task({id: task.substr(1) + "/"});
    deletetask.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, type: "DELETE", success: function(data){
      console.log(data);
      window.location.replace("main.html");
    }, error: function(data){
      $.notify("error occured", "error");
    }});
    // $.ajax({
    //   method: "DELETE",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.substr(1) +"/",
    //   headers: {
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   success: function(data){
    //     //notify the user and reload the views.
    //     window.location.replace("main.html");
    //     $.notify("Project Deleted", "success");
    //   },
    //   error: function(data){
    //     //handle error.
    //     console.log(data);
    //     $.notify("Error deleting task");
    //   }
    // });
  },

  AddTask: function(project){
    sessionStorage.setItem("projectid", project);
    var addtasktoproject = new app.AddTask({});
    $("#mainContainer").html(addtasktoproject.render().el);
  },

  DeleteProject: function(project){
    //delete the project
    var deleteproj = new app.project({pk: project.substr(1) + "/"});
    deleteproj.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, type: "DELETE", success: function(data){
      console.log(data);
      window.location.replace("main.html");
    }, error: function(data){
      $.notify("error occured", "error");
    }});
    // $.ajax({
    //   method: "DELETE",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.substr(1) + "/",
    //   headers: {
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   success: function(data){
    //     //notify the user and reload the views.
    //     window.location.replace("main.html");
    //     $.notify("Project Deleted");
    //   },
    //   error: function(data){
    //     //handle error.
    //     console.log(data);
    //     $.notify("Error deleting project");
    //   }
    // });
  },

  UpdateProject: function(project){
      var proj = new app.project({pk:  project.substr(1) + "/" });

      proj.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
        console.log(data);
        prjData = data;
        var updateproject = new app.UpdateProject({
          collection: prjData
        });
        $("#mainContainer").html(updateproject.render().el);
      }, function(error){
        $.notify("erro occured", "error");
      });
      // $.ajax({
      //   method: "GET",
      //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.substr(1) + "/",
      //   headers: {
      //     "Authorization": "Token " + sessionStorage.getItem("Token")
      //   },
      //   success: function(data){
      //     var project = new app.project(data);
      //     sessionStorage.setItem("project", JSON.stringify(data));
      //     var collectionProject = new app.Projects([project]);
      //     var updateproject = new app.UpdateProject({
      //       model: collectionProject
      //     });
      //     $("#mainContainer").html(updateproject.render().el);
      //   },
      //   error: function(data){
      //     console.log(data);
      //   }
      // });
  },

  LoadAll: function(){
    //get all data from projects and tasks.
    // $.ajax({
    //   method: "GET",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
    //   headers: {
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   success: function(data){
    //     var projects = new app.project(data);
    //     sessionStorage.setItem("Projects", JSON.stringify(projects["attributes"]));
    //     // console.log(Object.keys(projects).length);
    //     // for (var i = 0; i < Object.keys(projects).length; i++){
    //     //   console.log(projects["attributes"][0]["pk"]);
    //     //   for (var name in projects){
    //     //     console.log(JSON.stringify(name));
    //     //   }
    //     // }
    //     var viewProjects = new app.ProjectView({
    //        collection: projects
    //     });
    //     $("#project").html(viewProjects.render().el);
    //     console.log(data);
    //   },
    //   error: function(data){
    //     //handle error.
    //     console.log(data);
    //   }
    // });
    var projectTest = new app.project();
    projectTest.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
      projectData = data;

      //create view.
      var viewProjects = new app.ProjectView({
         collection: projectData
      });
      $("#project").html(viewProjects.render().el);
    }, function(error){
      $.notify("Error downloading projects", "error");

    });

    // $.ajax({
    //   method: "GET",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/",
    //   headers: {
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   success: function(data){
    //     var tasks = new app.task(data);
    //     sessionStorage.setItem("Tasks", JSON.stringify(tasks["attributes"]));
    //     console.log(tasks["attributes"]);
    //     var viewTasks = new app.TaskView({
    //       collection: tasks
    //     });
    //     $("#task").html(viewTasks.render().el);
    //   },
    //   error: function(data){
    //     //handle error.
    //     console.log(data);
    //   }
    // });
    var taskTest = new app.task();
    taskTest.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){
      Taskdata = data;

      //create view
      var viewTasks = new app.TaskView({
        collection: Taskdata
      });
      $("#task").html(viewTasks.render().el);

    }, function(error){
      $.notify("Error downloading tasks", "error");
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
