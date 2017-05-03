app.UpdateProject = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },
  tagName: 'div',
  template: '',

  events: {
    'click #btnUpdateProject': "UpdateProject"
  },

  initialize: function(){
    this.template = _.template($("#updateproject").html());
    this.ListenTo(this.model, "change", this.render);
  },

  render: function(){
    this.$el.append(this.$el.html(this.template(this.model.attributes)));
    return this;
  },

  UpdateProject: function(){
    //post new task to api.
    //var project = JSON.parse(sessionStorage.getItem("project"));
  //   this.model.UpdateProject({"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),
  //   "is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}, {"Authorization": "Token " + sessionStorage.getItem("Token")});
  //   console.log(this.collection.pk);
  //   var proj = new app.project({pk: this.collection.pk + "/"});
  //   proj.fetch({data: {"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),
  //   "is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}, type: "PATCH", headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
  //   success: function(data){
  //     console.log(data);
  //   },
  //   error: function(data){
  //     $.notify("error occured", "error");
  //   }
  // });
  this.model.UpdateProject({ headers: {"Authorization": "Token " + sessionStorage.getItem("Token")}});
  window.history.back();
  }
});
