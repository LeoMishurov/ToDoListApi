using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupModelsController : ControllerBase
    {
        private readonly RepositoryGroupModel _repositoryGroupModel;

        public GroupModelsController(RepositoryGroupModel repository)
        {
            _repositoryGroupModel = repository;
        }
        
        /// <summary>
        /// возвращает список всех групп
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public ActionResult<List<GroupDTO>> GetGroups() 
        { 
            // Достает id пользоввателя из токина
            var personId = User.Identity.GetId();

            // Ок - запрос вернет статус 200
            return Ok(_repositoryGroupModel.GetGroups().Where(x=>x.PersonId == personId)
               .Select(x => new GroupDTO { Id = x.Id, Name = x.Name, PersonId = x.PersonId })
               .ToList());
        }

        /// <summary>
        /// добавлене новой группы в бд
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public GroupDTO SaveGroup(GroupDTO groupDTO)
        {
            var groupModels = ToGroupModel(groupDTO);
            var groupModels1 = _repositoryGroupModel.SaveGroup(groupModels);
            return ToGroupDTO(groupModels1);            
        }

        /// <summary>
        /// удаляет группу из бд
        /// </summary>
        /// <param name="groupModelId"></param>
        [HttpPost("Delete")]
        public void DeleteGroup(int groupModelId)
        {
            // Достает id пользоввателя из токина
            var personId = User.Identity.GetId();
            _repositoryGroupModel.DeleteGroup(groupModelId, personId);
        }
          
        /// <summary>
        /// преобразовавет GroupDTO в GroupModel, убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private GroupModel ToGroupModel(GroupDTO groupDTO)
        {
            // Достает id пользоввателя из токина
            var personId = User.Identity.GetId();

            // Присваивание значений полей через конструктор по умолчанию
            GroupModel groupModel = new GroupModel 
            { 
                Id = groupDTO.Id, 
                Name = groupDTO.Name, 
                PersonId = personId 
            };
            return groupModel;
        }

        /// <summary>
        /// преобразовавет GroupModel в GroupDTO, убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private GroupDTO ToGroupDTO(GroupModel groupModel)
        {
            // Присваивание значений полей через конструктор по умолчанию
            GroupDTO groupDTO = new GroupDTO 
            { 
                Id = groupModel.Id, 
                Name = groupModel.Name, 
                PersonId = groupModel.PersonId 
            };
            return groupDTO;
        }
    }
}
