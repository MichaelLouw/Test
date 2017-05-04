window.app = {
  Models: {},
  Views: {},

  start: function(){
    var router = new app.Router();
    var projects = new app.Projects();
    var tasks = new app.Tasks();
    //populate tasks and projects.

    router.on("route:home", function(){
      router.navigate('', {
        trigger: true,
        replace: true
      });
    });

    router.on("route:home", function(){
      projects.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(collection){
        var projectsView = new app.ProjectView({
          collection: collection
        });

        $("#project").html(projectsView.render().$el);
      }, function(error){
        $.notify("Error loading project data", "error");
      });

      tasks.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(collection){
        var taskView = new app.TaskView({
          collection: collection
        });

        $("#task").html(taskView.render().$el);
      }, function(error){
        $.notify("Error loading project data", "error");
      });
    });

    router.on('route:AddProject', function(){
      var newProjectView = new app.newProject({
        model: new app.project()
      });

      newProjectView.on("form:submitted", function(attr){
        attr.id = projects.isEmpty() ? 1 : (_.max(projects.pluck('id')) + 1);
        projects.add(attr);
        var prj = new app.project(attr);
        prj.save({}, { headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/"});
        //router.navigate('', true);
        window.location.replace("http://localhost:9000/main.html");
      });

      $("#mainContainer").html(newProjectView.render().$el);
    });

    router.on('route:AddTask', function(projectid){
      var project = projects.get(projectid);
      var AddTaskToProject;

      if (project){
        AddTaskToProject = new app.AddTask({
          model: project
        });

        AddTaskToProject.on('form:submitted', function(attr){
          console.log(attr);
          tasks.add(attr);
          var tsk = new app.task(attr);
          tsk.save({}, { headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/"});
          //router.navigate('showdata', true);
          window.location.replace("http://localhost:9000/main.html");
        });

        $("#mainContainer").html(AddTaskToProject.render().$el);
      }
      else{
        $.notify("No such project please refresh", "error");
      }
    });

    router.on("route:DeleteProject", function(projectid){
      var project = projects.get(projectid);
      projects.remove(project);
      project.destroy({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.attributes.pk + "/"}).then(function(data){
        window.location.replace("http://localhost:9000/main.html");
      }, function(error){
        console.log(error);
      });
      //router.navigate('', true);
    });

    router.on("route:DeleteTask", function(taskid){
      var task = tasks.get(taskid);
      console.log(task.attributes);
      tasks.remove(task);
      task.destroy({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.attributes.id + "/"}).then(function(data){
        window.location.replace("http://localhost:9000/main.html");
      }, function(error){
        console.log(error);
      });
      //router.navigate("", true);
    });

    router.on("route:UpdateProject", function(projectid){
      var project = projects.get(projectid);
      var UpdateProjectView;

      if (project){
        UpdateProjectView = new app.UpdateProject({
          model: project
        });

        UpdateProjectView.on("form:submitted", function(attr){
          project.set(attr);
          project.save({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, method: "PATCH", url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.attributes.pk + "/"});
          //router.navigate("", true);
          window.location.replace("http://localhost:9000/main.html");
        });

        $("#mainContainer").html(UpdateProjectView.render().$el);
      }
      else{
        $.notify("No such project please refresh", "error");
      }
    });

    router.on("route:UpdateTask", function(taskid){
      var task = tasks.get(taskid);

      if (task){
        var UpdateTaskView = new app.UpdateTask({
          model: task
        });

        UpdateTaskView.on("form:submitted", function(attr){
          task.set(attr);
          task.save({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}, method: "PATCH", url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/tasks/" + task.attributes.id + "/"});
          //router.navigate("", true);
          window.location.replace("http://localhost:9000/main.html");
        });

        $("#mainContainer").html(UpdateTaskView.render().$el);
      }
      else{
        $.notify("No such Task please refresh", "error");
      }
    })

    Backbone.history.start();
  }
}
