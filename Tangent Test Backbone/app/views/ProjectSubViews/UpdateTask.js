app.UpdateTask = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },

  tagName: 'div',
  tamplate: '',

  events: {
    "submit .updatetask-form": "onFormSubmit"
  },

  initialize: function(){
    this.template = _.template($("#updatetask").html());
    //this.listenTo(this.model, "change", this.render);
  },

  render: function(){
    var string = this.model.attributes;
    //var object = JSON.parse(string);
    this.$el.append(this.$el.html(this.template({title: string.title, due_date: string.due_date, estimatedhours: string.estimated_hours})));
    return this;
  },

  onFormSubmit: function(e){
    e.preventDefault();

    this.trigger("form:submitted", {
      title: $(".task-title-input").val(),
      due_date: $(".task-duedate-input").val(),
      estimated_hours: $(".task-estimatedhours-input").val()
    });
  }
});
