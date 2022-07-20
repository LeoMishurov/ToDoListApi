using System.Text.Json.Serialization;
//using Newtonsoft.Json;

namespace ToDoListApi
{
    public class ToDoModel
    {
        public bool IsDone { get; set; }

        public string Text { get; set; }
        public DateTime Data { set; get; } = DateTime.Now.Date;

        public int Id { get; set; }
        public int GroupModelId { get; set; }

        [JsonIgnore]
        //Сылка на GroupModel для enti framevork
        public GroupModel GroupModel { get; set; }

    }

    public class GroupModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        //Сылка на ToDoModel для enti framevork
        public List<ToDoModel> ToDoModels { get; set; }

    }

    public class GroupDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }

    }
    public class ToDoDTO 
    {
        public bool IsDone { get; set; }

        public string Text { get; set; }
        public DateTime Data { set; get; } = DateTime.Now.Date;

        public int Id { get; set; }
        public int GroupModelId { get; set; }
    }
}
