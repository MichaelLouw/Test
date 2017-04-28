var app = app || {};

app.UpdateProject = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },
  model: project,
  tagName: 'div',
  template: '',

  events: {
    'click #btnUpdateProject': "UpdateProject"
  },

  initialize: function(){
    this.template = _.template($("#updateproject").html());
  },

  render: function(){
    // var project = sessionStorage.getItem("project");
    //
    //     this.$el.html(UpdateProjectView);
    var string = JSON.stringify(this.model);
    var object = JSON.parse(string);
    this.$el.append(this.$el.html(this.template({title: object[0].title, description: object[0].description,startdate: object[0].start_date,enddate: object[0].end_date,isbillable: object[0].is_billable,isactive: object[0].is_active})));
    return this;
  },

  UpdateProject: function(){
    //post new task to api.
    var project = JSON.parse(sessionStorage.getItem("project"));
    $.ajax({
      method: "PATCH",
      contentType: "application/json",
      url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/" + project.pk + "/",
      headers:{
        "Authorization": "Token " + sessionStorage.getItem("Token")
      },
      data: JSON.stringify({"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),"is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}),
      processData: false,
      success: function(data){
        $.notify("Project updated", "success");
        window.location.replace("#/main");
      },
      error: function(data){
        //handle error.
        $.notify("error updating project", "error");
      }
    });
  }
});
