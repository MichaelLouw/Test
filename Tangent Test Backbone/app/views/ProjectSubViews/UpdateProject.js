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
    var string = JSON.stringify(this.collection);
    var object = JSON.parse(string);
    console.log(string);
    this.$el.append(this.$el.html(this.template({title: object.title, description: object.description,startdate: object.start_date,enddate: object.end_date,isbillable: object.is_billable,isactive: object.is_active})));
    return this;
  },

  UpdateProject: function(){
    //post new task to api.
    //var project = JSON.parse(sessionStorage.getItem("project"));
    console.log(this.collection.pk);
    var proj = new app.project({pk: this.collection.pk + "/"});
    proj.fetch({data: {"title": $("#title").val(), "description": $("#description").val(),"start_date": $("#start_date").val(),"end_date": $("#end_date").val(),
    "is_billable": $("#is_billable option:selected").val(),"is_active": $("#is_active option:selected").val()}, type: "PATCH", headers: {"Authorization": "Token " + sessionStorage.getItem("Token")},
    success: function(data){
      console.log(data);
    },
    error: function(data){
      $.notify("error occured", "error");
    }
  });
  }
});
