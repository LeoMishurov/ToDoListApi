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
        
        /// <summary>
        /// возвращает все задачи
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public ActionResult<List<ToDoModel>> GetToDoModel()
        { 
            // ƒостает id пользоввател€ из токина
            var userid = User.Identity.GetId();

            return Ok(_repositoryToDoModel.GetToDoModel(userid));
        }

        /// <summary>
        /// сохранение/редактирование задачи в бд
        /// </summary>
        /// <param name="toDoDTO"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public ActionResult<ToDoDTO>  SaveToDo(ToDoDTO toDoDTO)
        {         
            var toDoModel = ToToDoModel(toDoDTO);
            toDoModel = _repositoryToDoModel.SaveToDo(toDoModel);

            return ToToDoDTO(toDoModel);
        }

        /// <summary>
        /// удаление задачи из бд
        /// </summary>
        /// <param name="toDoModelId"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public ActionResult DeleteToDoModel(int toDoModelId)
        {
            // ƒостает id пользоввател€ из токина
            var userid = User.Identity.GetId();
            _repositoryToDoModel.DeleteToDoModel(toDoModelId, userid);
            return Ok();
        }

        /// <summary>
        /// возвращает все задачи с id группы переданной в метод
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetByGroupId")]
        public List<ToDoModel> GetToDoByGroupId(int Id)
        {
            // ƒостает id пользоввател€ из токина
            var userid = User.Identity.GetId();
            return _repositoryToDoModel.GetToDoByGroupId(Id, userid);
        }

        /// <summary>
        /// преобразовавет ToDoDTO в <see cref="ToDoModel"/> , убира€ лишние пол€
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoModel ToToDoModel(ToDoDTO toDoDTO)
        {
            // ѕрисваивание значений полей через конструктор по умолчанию
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
        /// преобразовавет ToDoModel в <see cref="ToDoDTO"/>, убира€ лишние пол€
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoDTO ToToDoDTO(ToDoModel toDoModel)
        {
            // ѕрисваивание значений полей через конструктор по умолчанию
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