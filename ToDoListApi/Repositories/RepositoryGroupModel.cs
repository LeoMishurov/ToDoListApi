using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApi.Models;

namespace ToDoListApi.Repositories
{
    public class RepositoryGroupModel
    {
        private MyContext context;
        public RepositoryGroupModel(MyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Возвращает все группы из GroupModels
        /// </summary>
        /// <returns></returns>
        public List<GroupModel> GetGroups()
        {
            return context.GroupModel.ToList();
        }

        /// <summary>
        /// Сохранение группы в бд
        /// </summary>
        /// <param name="groupModel"></param>
        public GroupModel SaveGroup(GroupModel groupModel)
        {
            if (groupModel.Id == 0)
                // Подготовка переменной для сохранения
                context.GroupModel.Add(groupModel);
            else
                context.GroupModel.Update(groupModel);
                // Сохранение в бд
                context.SaveChanges();

            return groupModel;
        }

        /// <summary>
        /// Удаление обьекта GroupModel из бд
        /// </summary>
        /// <param name="groupModel"></param>
        public void DeleteGroup(int groupModelId, int personId)
        {   // FirstOrDefault() возвращает первый обект совпадающий с условием
            var groupModel = context.GroupModel.FirstOrDefault(x => x.Id == groupModelId && x.PersonId == personId);
            // Подготовка переменной для удаления
            if(groupModel!=null)
            context.GroupModel.Remove(groupModel);

            // Сохранение в бд
            context.SaveChanges();
        }
    }
}
