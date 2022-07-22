namespace ToDoListApi.Models
{
   
        public class GroupModel
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public int PersonId { get; set; }

            //Сылка на ToDoModel для enti framevork
            public List<ToDoModel> ToDoModels { get; set; }

        }

        public class GroupDTO
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public int PersonId { get; set; }

    }
    
}
