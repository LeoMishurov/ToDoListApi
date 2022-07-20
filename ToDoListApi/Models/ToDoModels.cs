using System.Text.Json.Serialization;
//using Newtonsoft.Json;

namespace ToDoListApi.Models
{
    public class ToDoModel
    {
        public bool IsDone { get; set; }

        public string Text { get; set; }
        public DateTime Data { set; get; } = DateTime.Now.Date;

        public int Id { get; set; }
        public int GroupModelId { get; set; }

        public int PersonId { get; set; }

        [JsonIgnore]
        //Сылка на GroupModel для enti framevork
        public GroupModel GroupModel { get; set; }

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
