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
    public class ToDoModelsController : ControllerBase
    {
        private readonly RepositoryToDoModel _repositoryToDoModel;

        public ToDoModelsController(RepositoryToDoModel repository)
        {
            _repositoryToDoModel = repository;
        }       

        [HttpGet("Get")]
        public ActionResult<List<ToDoModel>> GetToDoModel()
        { 
            //достает id пользоввателя из токина
            var userid = User.Identity.GetId();

            return Ok(_repositoryToDoModel.GetToDoModel(userid));
        }

        [HttpPost("Save")]
        public ActionResult<ToDoDTO>  SaveToDo(ToDoDTO toDoDTO)
        {
            
            var toDoModel = ToToDoModel(toDoDTO);
            toDoModel = _repositoryToDoModel.SaveToDo(toDoModel);

            return ToToDoDTO(toDoModel);
        }

        [HttpPost("Delete")]
        public ActionResult DeleteToDoModel(int toDoModelId)
        {
            //достает id пользоввателя из токина
            var userid = User.Identity.GetId();
            _repositoryToDoModel.DeleteToDoModel(toDoModelId, userid);
            return Ok();
        }

        [HttpGet("GetByGroupId")]
        public List<ToDoModel> GetToDoByGroupId(int Id)
        {
            //достает id пользоввателя из токина
            var userid = User.Identity.GetId();
            return _repositoryToDoModel.GetToDoByGroupId(Id, userid);
        }

        /// <summary>
        /// преобразовавет ToDoDTO в <see cref="ToDoModel"/> , убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoModel ToToDoModel(ToDoDTO toDoDTO)
        {
            //присваивание значений полей через конструктор по умолчанию
            ToDoModel toDoModel = new ToDoModel 
            {
                Text = toDoDTO.Text, 
                Data = toDoDTO.Data, 
                GroupModelId = toDoDTO.GroupModelId,
                IsDone = toDoDTO.IsDone,
                Id = toDoDTO.Id,
                PersonId = User.Identity.GetId()
                
            };
            return toDoModel;
        }

        /// <summary>
        /// преобразовавет ToDoModel в <see cref="ToDoDTO"/>, убирая лишние поля
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoDTO ToToDoDTO(ToDoModel toDoModel)
        {
            //присваивание значений полей через конструктор по умолчанию
            ToDoDTO toDoDTO = new ToDoDTO 
            {
                Text=toDoModel.Text, 
                Data=toDoModel.Data, 
                GroupModelId=toDoModel.GroupModelId,
                IsDone = toDoModel.IsDone,
                Id = toDoModel.Id,
                PersonId = toDoModel.PersonId
                
            };
            return toDoDTO;
        }
    }
}