var app = app || {};

app.newProject = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },
  tagName: 'div',
  className: 'AddProject',
  template: $("#addproject").html(),

  events: {
    'click #btnAddProject': "NewProject"
  },

  initialize: function(){
    this.render();
  },

  render: function(){
    // var project = sessionStorage.getItem("project");
    //
    //     this.$el.html(UpdateProjectView);
    this.$el.html($("#addproject").html());
    return this;
  },

  NewProject: function(){
    //post new task to api.
    var proj = new app.project();
    proj.fetch({data: {"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),
    "is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}, type: "POST", headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
    success: function(data){
      console.log(data);
      $.notify("New Project Saved", "success");
      window.location.replace("");
    },
    error: function(data){
      $.notify("error occured", "error");
    }
  });
    // alert( + " " +$("#description").val() + " "+$("#start_date").val() + " "+$("#end_date").val() + " "+$("#is_billable option:selected").val() + " "+$("#is_active option:selected").val() + " ");
    // $.ajax({
    //   method: "POST",
    //   contentType: "application/json",
    //   url: "http://projectservice.staging.tangentmicroservices.com:80/api/v1/projects/",
    //   headers:{
    //     "Authorization": "Token " + sessionStorage.getItem("Token")
    //   },
    //   data: JSON.stringify({"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),"is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}),
    //   success: function(data){
    //     $.notify("New Project Saved", "success");
    //     window.location.replace("");
    //   },
    //   error: function(data){
    //     //handle error.
    //     $.notify("error saving project", "error");
    //   }
    // });
  }
});
