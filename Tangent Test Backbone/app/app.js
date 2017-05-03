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

    Backbone.history.start();
  }
}
