app.UpdateTask = Backbone.View.extend({
  // initialize: function(options){
  //   this.theproject = options;
  // },

  tagName: 'div',
  tamplate: '',

  initialize: function(){
    this.template = _.template($("#updatetask").html());
    this.listenTo(this.model, "change", this.render);
  },

  render: function(){
    var string = this.model.attributes;
    //var object = JSON.parse(string);
    this.$el.append(this.$el.html(this.template({title: string.title, duedate: string.due_date, estimatedhours: string.estimated_hours})));
    return this;
  }
});
