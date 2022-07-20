using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupModelsController : ControllerBase
    {
        private readonly RepositoryGroupModel _repositoryGroupModel;

        public GroupModelsController(RepositoryGroupModel repository)
        {
            _repositoryGroupModel = repository;
        }
        //непонятно откуда он ее берет, если это за коментить
        //GroupModel groupModel = new();

        [HttpGet("Get")]
        public ActionResult<List<GroupDTO>> GetGroups() 
        { //ок - запрос вернет статус 200
           return Ok(_repositoryGroupModel.GetGroups()
               .Select(x => new GroupDTO { Id = x.Id, Name = x.Name })
               .ToList()); 
        }

        [HttpPost("Save")]
        public GroupDTO SaveGroup(GroupDTO groupDTO)
        {
            var groupModels = ToGroupModel(groupDTO);
            var groupModels1 = _repositoryGroupModel.SaveGroup(groupModels);
            return ToGroupDTO(groupModels1);            
        }

        [HttpPost("Delete")]
        public void DeleteGroup(int groupModelId)
        {
            _repositoryGroupModel.DeleteGroup(groupModelId);
        }
       
        

        /// <summary>
        /// преобразовавет GroupDTO в GroupModel, убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private GroupModel ToGroupModel(GroupDTO groupDTO)
        {
            //присваивание значений полей через конструктор по умолчанию
            GroupModel groupModel = new GroupModel { Id = groupDTO.Id, Name = groupDTO.Name };
            return groupModel;
        }
        /// <summary>
        /// преобразовавет GroupModel в GroupDTO, убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private GroupDTO ToGroupDTO(GroupModel groupModel)
        {
            //присваивание значений полей через конструктор по умолчанию
            GroupDTO groupDTO = new GroupDTO { Id = groupModel.Id, Name = groupModel.Name };
            return groupDTO;
        }
    }
}
