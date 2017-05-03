window.app = {
  Models: {},
  Views: {},

  start: function(){
    var router = new app.Router();
    var projects = new app.Projects();
    var tasks = new app.Tasks();
    projects.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){

    }, function(error){
      $.notify("Error loading project data", "error");
    });
    tasks.fetch({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}}).then(function(data){

    }, function(error){
      $.notify("Error loading project data", "error");
    });
    console.log(projects);
    console.log(tasks);
    //populate tasks and projects.

    router.on("route:home", function(){
      router.navigate('', {
        trigger: true,
        replace: true
      });
    });

    router.on('route:AddProject', function(){
      var newProjectView = new app.newProject({
        model: new app.project()
      });

      newProjectView.on("form:submitted", function(attr){
        attr.id = projects.isEmpty() ? 1 : (_.max(projects.pluck('id')) + 1);
        projects.add(attr);
        router.navigate('', true);
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
          tasks.add(attr);
          router.navigate('', true);
        });

        $("#mainContainer").html(AddTaskToProject.render().$el);
      }
      else{
        $.notify("No such project please refresh", "error");
      }
    });

    router.on("route:DeleteProject", function(projectid){
      var project = projects.get(projectid);
      projects.destroy(project);
      router.navigate('', true);
    });

    router.on("route:UpdateProject", function(projectid){
      var project = projects.get(projectid);
      var UpdateProjectView;

      if (project){

      }
      else{
        $.notify("No such project please refresh", "error");
      }
    })

    Backbone.history.start();
  }
}
