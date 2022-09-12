using ToDoListApi.Models;

namespace ToDoListApi.Repositories
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
        public List<ToDoModel> GetToDoModel(int PersonId)
        {
            return _context.ToDoModels.Where(x => x.PersonId == PersonId).ToList();
        }

        /// <summary>
        /// Возвращает все задачи сопадающие с id группы переданной в метод
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<ToDoModel> GetToDoByGroupId(int groupId, int userId)
        {
            return _context.ToDoModels.Where(x => (groupId==0||x.GroupModelId==groupId) && x.PersonId==userId).ToList();
        }
      
        /// <summary>
        /// Удаление обьекта ToDoModel из бд
        /// </summary>
        /// <param name="groupModel"></param>
        public void DeleteToDoModel(int toDoModelId, int userID)
        {

            var todo = new ToDoModel { Id = toDoModelId, PersonId = userID };
            // Attach(todo) метод указывающий EF на наличие такой переменной
            _context.ToDoModels.Attach(todo);
            // Подготовка переменной для удаления
            _context.ToDoModels.Remove(todo);

            // Сохранение в бд
            _context.SaveChanges();
        }

        /// <summary>
        /// Сохранение/редактирование задачи в бд
        /// </summary>
        /// <param name="groupModel"></param>
        public ToDoModel SaveToDo(ToDoModel toDoModel)
        {
            if (toDoModel.Id == 0)
                // Подготовка переменной для сохранения
                _context.ToDoModels.Add(toDoModel);
            else
                _context.ToDoModels.Update(toDoModel);
                // Сохранение в бд
                _context.SaveChanges();
            return toDoModel;
        }
    }
}
