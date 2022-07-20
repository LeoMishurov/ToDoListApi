using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Controllers
{
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
            //������� id ������������� �� ������
            var userid = User.Identity.GetId();

            return Ok(_repositoryToDoModel.GetToDoModel());
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
            _repositoryToDoModel.DeleteToDoModel(toDoModelId);
            return Ok();
        }

        [HttpGet("GetByGroupId")]
        public List<ToDoModel> GetToDoByGroupId(int Id)
        {
            return _repositoryToDoModel.GetToDoByGroupId(Id);
        }

        /// <summary>
        /// �������������� ToDoDTO � <see cref="ToDoModel"/> , ������ ������ ����
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoModel ToToDoModel(ToDoDTO toDoDTO)
        {
            //������������ �������� ����� ����� ����������� �� ���������
            ToDoModel toDoModel = new ToDoModel 
            {
                Text = toDoDTO.Text, 
                Data = toDoDTO.Data, 
                GroupModelId = toDoDTO.GroupModelId,
                IsDone = toDoDTO.IsDone,
                Id = toDoDTO.Id
            };
            return toDoModel;
        }

        /// <summary>
        /// �������������� ToDoModel � <see cref="ToDoDTO"/>, ������ ������ ����
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        private ToDoDTO ToToDoDTO(ToDoModel toDoModel)
        {
            //������������ �������� ����� ����� ����������� �� ���������
            ToDoDTO toDoDTO = new ToDoDTO 
            {
                Text=toDoModel.Text, 
                Data=toDoModel.Data, 
                GroupModelId=toDoModel.GroupModelId,
                IsDone = toDoModel.IsDone,
                Id = toDoModel.Id
            };
            return toDoDTO;
        }
    }
}