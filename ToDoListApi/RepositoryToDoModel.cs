namespace ToDoListApi
{
    public class RepositoryToDoModel
    {
        private MyContext _context;
        public RepositoryToDoModel(MyContext context) 
        { 
            _context = context;
        }

        /// <summary>
        /// Возвращает все задачи из ToDoModel
        /// </summary>
        /// <returns></returns>
        public List<ToDoModel> GetToDoModel() 
        { 
            return _context.ToDoModels.ToList();
        }

        /// <summary>
        /// Возвращает все задачи сопадающие с id группы переданной в метод
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<ToDoModel> GetToDoByGroupId(int groupId)
        {
            return _context.ToDoModels.Where(x => groupId == 0 || x.GroupModelId == groupId).ToList();
        }

        /// <summary>
        /// Удаление обьекта ToDoModel из бд
        /// </summary>
        /// <param name="groupModel"></param>
        public void DeleteToDoModel(int toDoModelId)
        {
            var todo = new ToDoModel { Id = toDoModelId };
            // Attach(todo) метод указывающий EF на наличие такой переменной
            _context.ToDoModels.Attach(todo);
            // подготовка переменной для удаления
            _context.ToDoModels.Remove(todo);

            // сохранение в бд
            _context.SaveChanges();
        }

        /// <summary>
        /// Сохранение/редактирование задачи в бд
        /// </summary>
        /// <param name="groupModel"></param>
        public ToDoModel SaveToDo(ToDoModel toDoModel)
        {
            if (toDoModel.Id == 0)
                // подготовка переменной для сохранения
                _context.ToDoModels.Add(toDoModel);
            else
                _context.ToDoModels.Update(toDoModel);
            // сохранение в бд
            _context.SaveChanges();
            return toDoModel;
        }

    }
}
